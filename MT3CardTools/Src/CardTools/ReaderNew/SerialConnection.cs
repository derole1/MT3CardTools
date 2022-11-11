using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.IO.Pipes;

using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.CardTools.ReaderNew
{
    class SerialConnection : IDisposable
    {
        const int CONNECT_TIMEOUT = 1000;
        const int READ_TIMEOUT = 1000;
        const int WRITE_TIMEOUT = 1000;

        public enum EPortType
        {
            COM,
            Pipe
        }

        public string PortName { get; }
        public EPortType PortType { get; }
        public bool IsOpen { get; protected set; }
        private SerialPort Port { get; }
        private NamedPipeClientStream Pipe { get; }

        public SerialConnection(string port, EPortType type)
        {
            PortType = type;
            switch (PortType)
            {
                case EPortType.COM:
                    Port = new SerialPort(port);
                    Port.ReadTimeout = READ_TIMEOUT;
                    Port.WriteTimeout = WRITE_TIMEOUT;
                    break;
                case EPortType.Pipe:
                    Pipe = new NamedPipeClientStream(".", port, PipeDirection.InOut, PipeOptions.Asynchronous);
                    break;
            }
        }

        public void SetCOMParameters(int baudRate, Parity parity)
        {
            if (PortType == EPortType.COM)
            {
                Port.BaudRate = baudRate;
                Port.Parity = parity;
                Port.DataBits = 8;
                Port.StopBits = StopBits.One;
                Port.Handshake = Handshake.None;
                Log.Info($"SetCOMParameters: Updated to Baud:{Port.BaudRate},Parity:{Port.Parity},DataBits:{Port.DataBits},StopBits:{Port.StopBits},Flow:{Port.Handshake}");
            }
        }

        public async Task OpenAsync()
        {
            try
            {
                switch (PortType)
                {
                    case EPortType.COM:
                        Port.Open();
                        break;
                    case EPortType.Pipe:
                        await Pipe.ConnectAsync(CONNECT_TIMEOUT);
                        break;
                }
                Log.Info($"Open: Port opened!");
                IsOpen = true;
            }
            catch
            {
                Log.Error($"Open: Timeout");
            }
        }
        public void Open() => OpenAsync().GetAwaiter().GetResult();

        public async Task ReadAckAsync()
        {
            if (!IsOpen)
                return;
            var mark = await ReadByteAsync();
            if (mark == 0xFF)
                return;
            else if (mark != ReaderConstants.ACK)
                await ReadAckAsync();   //Could this cause a stack overflow?
            Log.Info($"ReadAck: Got!");
        }
        public void ReadAck() => ReadAckAsync().GetAwaiter().GetResult();

        public async Task<byte[]> ReadDataAsync()
        {
            if (!IsOpen)
                return null;
            var mark = await ReadByteAsync();
            if (mark == 0xFF)
                return null;
            else if (mark != ReaderConstants.STX)
                return await ReadDataAsync();   //Could this cause a stack overflow?

            var payloadSize = await ReadByteAsync();
            Log.Debug($"ReadData: Got STX! Payload size: {payloadSize}");
            using (var ms = new MemoryStream())
            {
                for (int i=0; i<payloadSize; i++)
                {
                    var b = await ReadByteAsync();
                    if (i == payloadSize - 2 && b == ReaderConstants.ETX)
                    {
                        var checksum = await ReadByteAsync();
                        ms.Flush();
                        var data = ms.ToArray();
                        var calcSum = ReaderConstants.Checksum(data.Concat(new byte[] { b }).ToArray(), payloadSize);
                        if (checksum != calcSum)
                        {
                            Log.Error($"ReadData: Checksum error! Expected:{calcSum},Got:{checksum}");
                            return null;
                        }
                        Log.Info($"ReadData: Read {payloadSize + 2} bytes!");
                        return data;
                    }
                    ms.WriteByte(b);
                }
                Log.Error($"ReadData: Reached end of payload but have not encountered an ETX?");
                return null;
            }
        }
        public byte[] ReadData() => ReadDataAsync().GetAwaiter().GetResult();

        public async Task WriteEnqAsync()
        {
            if (!IsOpen)
                return;
            await WriteByteAsync(ReaderConstants.ENQ);
            Log.Info($"WriteEnq: Done!");
        }
        public void WriteEnq() => WriteEnqAsync().GetAwaiter().GetResult();

        public async Task WriteDataAsync(byte[] data)
        {
            data = data.Concat(new byte[] { ReaderConstants.ETX }).ToArray();
            var len = data.Length + 1;
            await WriteByteAsync(ReaderConstants.STX);
            await WriteByteAsync((byte)len);
            await WriteAsync(data);
            await WriteByteAsync(ReaderConstants.Checksum(data, (byte)len));
            Log.Info($"WriteData: Wrote {len + 2} bytes!");
        }
        public void WriteData(byte[] data) => WriteDataAsync(data).GetAwaiter().GetResult();

        private async Task<byte> ReadByteAsync() =>
            PortType == EPortType.COM ? await Port.ReadByteAsync() : await Pipe.ReadByteAsync();
        private async Task WriteAsync(byte[] b)
        {
            if (PortType == EPortType.COM)
                await Port.WriteAsync(b);
            else
                await Pipe.WriteAsync(b);
        }
        private async Task WriteByteAsync(byte b) => await WriteAsync(new byte[] { b });

        public void Close()
        {
            switch (PortType)
            {
                case EPortType.COM:
                    Port.Close();
                    break;
                case EPortType.Pipe:
                    Pipe.Close();
                    break;
            }
            Log.Info($"Close: Closed connection.");
            IsOpen = false;
        }

        public void Dispose()
        {
            switch (PortType)
            {
                case EPortType.COM:
                    Port.Dispose();
                    break;
                case EPortType.Pipe:
                    Pipe.Dispose();
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.IO.Pipes;

//using MT3CardTools.Src.CardTools.Reader;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Helpers
{
    static class SerialExtensions
    {
        //public async static Task<int> ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count)
        //{
        //    int bytesRead = 0;
        //    var buf = new byte[1];
        //    while (true)
        //    {
        //        await Task.Run(() => serialPort.Read(buf, 0, buf.Length));
        //        switch (buf[0])
        //        {
        //            case ReaderConstants.ETX:
        //                buffer[bytesRead] = buf[0];
        //                await Task.Run(() => serialPort.Read(buf, 0, buf.Length));
        //                buffer[bytesRead+1] = buf[0];
        //                return bytesRead + 2;
        //            default:
        //                if (bytesRead == 0 &&
        //                    buf[0] != ReaderConstants.STX)
        //                {
        //                    if (buf[0] == ReaderConstants.ACK || buf[0] == ReaderConstants.ENQ)
        //                    {
        //                        buffer[bytesRead] = buf[0];
        //                        return bytesRead + 1;
        //                    }
        //                    continue;
        //                }
        //                else
        //                    buffer[bytesRead] = buf[0];
        //                bytesRead++;
        //                break;
        //        }
        //    }
        //}

        public async static Task<byte> ReadByteAsync(this SerialPort serialPort)
        {
            try
            {
                var buf = new byte[1];
                await serialPort.BaseStream.ReadAsync(buf, 0, buf.Length);
                return buf[0];
            }
            catch
            {
                Log.Error("ReadByte: Timeout");
                return 0xFF;
            }
        }

        public async static Task<byte> ReadByteAsync(this NamedPipeClientStream serialPort)
        {
            try
            {
                var buf = new byte[1];
                await serialPort.ReadAsync(buf, 0, buf.Length);
                return buf[0];
            }
            catch
            {
                Log.Error("ReadByte: Timeout");
                return 0xFF;
            }
        }

        public async static Task<int> ReadAsync(this SerialPort serialPort, byte[] buffer, int offset, int count)
        {
            return await serialPort.BaseStream.ReadAsync(buffer, offset, count);
        }

        public async static Task<byte[]> ReadAsync(this SerialPort serialPort, int count)
        {
            try
            {
                var buffer = new byte[count];
                await serialPort.ReadAsync(buffer, 0, count);
                Log.Debug($"RDATA: {BitConverter.ToString(buffer).Replace("-", " ")}");
                return buffer;
            }
            catch
            {
                Log.Error("Read: Timeout");
                return null;
            }
        }

        public async static Task<byte[]> ReadAsync(this NamedPipeClientStream serialPort, int count)
        {
            var buffer = new byte[count];
            await serialPort.ReadAsync(buffer, 0, count);
            Log.Debug($"RDATA: {BitConverter.ToString(buffer).Replace("-", " ")}");
            return buffer;
        }

        public async static Task WriteAsync(this SerialPort serialPort, byte[] buffer, int offset, int count)
        {
            try
            {
                Log.Debug($"WDATA: {BitConverter.ToString(buffer).Replace("-", " ")}");

                await Task.Run(() => serialPort.Write(buffer, offset, count));
            }
            catch
            {
                Log.Error("Write: Timeout");
            }
        }

        public async static Task WriteAsync(this SerialPort serialPort, byte[] buffer)
        {
            await serialPort.WriteAsync(buffer, 0, buffer.Length);
        }

        public async static Task WriteAsync(this NamedPipeClientStream serialPort, byte[] buffer)
        {
            try
            {
                Log.Debug($"WDATA: {BitConverter.ToString(buffer).Replace("-", " ")}");

                await serialPort.WriteAsync(buffer, 0, buffer.Length);
            }
            catch
            {
                Log.Error("Write: Timeout");
            }
        }
    }
}
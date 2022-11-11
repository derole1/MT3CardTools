using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.IO.Pipes;

using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.CardTools.Reader
{
    class ReaderConnection : IDisposable
    {
        public enum EWriteCardType
        {
            FromDispenser,
            FromInsert
        }

        public class ReaderStatus
        {
            public byte R { get; set; }
            public ReaderConstants.EP P { get; set; }
            public ReaderConstants.ES S { get; set; }
        }

        public SerialPort Port { get; }
        public NamedPipeClientStream Pipe { get; }
        public string PortName { get; }
        public bool IsOpen { get; protected set; }

        public ReaderConnection(string port, bool isSerialPort = true, int baud = 38400, Parity parity = Parity.Even)
        {
            try
            {
                PortName = port;
                if (isSerialPort)
                {
                    Port = new SerialPort(port, baud, parity, 8, StopBits.One);
                    Port.Handshake = Handshake.None;
                    Port.Open();
                }
                else
                {
                    Pipe = new NamedPipeClientStream(".", port, PipeDirection.InOut, PipeOptions.Asynchronous);
                }
                IsOpen = true;
            } catch (Exception e) { System.Diagnostics.Debug.WriteLine(e); }
        }

        public async Task<bool> Initalize(bool a1, bool a2)
        {
            if (Pipe != null && !Pipe.IsConnected)
                await Pipe.ConnectAsync();
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.Init);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write((byte)(a1 ? 49 : 48));
                rw.Write((byte)(a2 ? 49 : 48));
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.Init &&
                                (rr.ReadByte() == 0x30 || true) && //Hack
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<ReaderStatus> GetStatus()
        {
            if (Pipe != null && !Pipe.IsConnected)
                await Pipe.ConnectAsync();
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.GetStatus);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                await rw.SerializeAndSend(this);
            }
            using (var rr = await GetRequestReader(true))
            {
                if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                {
                    if (rr.ReadByte() != (byte)ReaderConstants.ERequestType.GetStatus)
                        return null;
                    return new ReaderStatus
                    {
                        R = rr.ReadByte(),
                        P = (ReaderConstants.EP)rr.ReadByte(),
                        S = (ReaderConstants.ES)rr.ReadByte()
                    };
                }
            }
            return null;
        }

        public async Task<string> GetVersion()
        {
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.GetVersion);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                await rw.SerializeAndSend(this);
            }
            using (var rr = await GetRequestReader(true))
            {
                if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                {
                    if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.GetVersion &&
                        rr.ReadByte() == 0x30 &&
                        rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                        rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                    {
                        return rr.ReadString(Encoding.ASCII);
                    }
                }
            }
            return null;
        }

        public async Task AwaitCardInsert()
        {
            bool readMode = false;
            bool bitMode = false;
            byte track = 6;
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.ReadData);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write((byte)(readMode ? 0x30 : 0x32));
                rw.Write((byte)(bitMode ? 0x30 : 0x31));
                rw.Write((byte)(track + 0x30));
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.ReadData &&
                                rr.ReadByte() == 0x31 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        public async Task Cancel()
        {
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.CancelJob);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.CancelJob &&
                                rr.ReadByte() == 0x31 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        public async Task<bool> CheckCardDispenser()
        {
            byte dispenseType = 0x32;
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.DispenseCard);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write(dispenseType);
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.DispenseCard &&
                                rr.ReadByte() == 0x30 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK)
                            {
                                var sStatus = rr.ReadByte();
                                if (sStatus == (byte)ReaderConstants.ES.RunningCommand)
                                    continue;
                                return sStatus == (byte)ReaderConstants.ES.DispenserFull ||
                                    sStatus == (byte)ReaderConstants.ES.Idle;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> DispenseCard()
        {
            byte dispenseType = 0x31;
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.DispenseCard);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write(dispenseType);
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.DispenseCard &&
                                   rr.ReadByte() == 0x31 &&
                                   rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                   rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<byte[]> ReadCard()
        {
            bool readMode = true;
            bool bitMode = false;
            byte track = 6;
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.ReadData);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write((byte)(readMode ? 0x30 : 0x32));
                rw.Write((byte)(bitMode ? 0x30 : 0x31));
                rw.Write((byte)(track + 0x30));
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                var parity = Parity.Even;
                while (true)
                {
                    using (var rr = await GetRequestReader(false, parity))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.ReadData &&
                                rr.ReadByte() == 0x31 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return rr.ReadBytes(0xCF);
                            }
                        }
                        else if (rr.ResponseStatus == RequestReader.EResponseStatus.IncorrectSTX)
                            parity = Parity.None;
                    }
                }
            }
            return null;
        }

        public async Task<bool> WriteCard(byte[] data)
        {
            bool writeMode = true;
            bool bitMode = false;
            byte track = 6;
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.WriteData);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                rw.Write((byte)(writeMode ? 0x30 : 0x31));
                rw.Write((byte)(bitMode ? 0x30 : 0x31));
                rw.Write((byte)(track + 0x30));
                rw.Write(data);
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.WriteData &&
                                rr.ReadByte() == 0x31 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> EjectCard()
        {
            using (var rw = new RequestWriter())
            {
                rw.Write((byte)ReaderConstants.ERequestType.EjectCard);
                rw.Write(0);
                rw.Write(0);
                rw.Write(0);
                await rw.SerializeAndSend(this);
            }
            if (await RecieveAck())
            {
                while (true)
                {
                    using (var rr = await GetRequestReader(false))
                    {
                        if (rr.ResponseStatus == RequestReader.EResponseStatus.OK)
                        {
                            if (rr.ReadByte() == (byte)ReaderConstants.ERequestType.EjectCard &&
                                rr.ReadByte() == 0x30 &&
                                rr.ReadByte() == (byte)ReaderConstants.EP.OK &&
                                rr.ReadByte() == (byte)ReaderConstants.ES.Idle)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        async Task SendAck()
        {
            if (Port == null)
                await Pipe.WriteAsync(new byte[] { ReaderConstants.ACK });
            else
                await Port.WriteAsync(new byte[] { ReaderConstants.ACK });
            //await Task.Delay(10);   //Give reader some time
        }

        async Task SendEnq()
        {
            if (Port == null)
                await Pipe.WriteAsync(new byte[] { ReaderConstants.ENQ });
            else
                await Port.WriteAsync(new byte[] { ReaderConstants.ENQ });
            //await Task.Delay(10);   //Give reader some time
        }

        async Task<bool> RecieveAck()
        {
            byte[] buf;
            if (Port == null)
                buf = await Pipe.ReadAsync(255);
            else
                buf = await Port.ReadAsync(255);
            return buf[0] == ReaderConstants.ACK;
        }

        async Task<RequestReader> GetRequestReader(bool sendAck, Parity parity = Parity.Even)
        {
            if (!sendAck || await RecieveAck())
            {
                await SendEnq();
                //await Task.Delay(500);   //Give reader some time
                //Port.Parity = parity;
                var reqReader = new RequestReader(Port == null ? await Pipe.ReadAsync(255) : await Port.ReadAsync(255));
                //Port.Parity = Parity.Even;
                await Task.Delay(10);   //Give reader some time
                return reqReader;
            }
            return null;
        }

        public void Dispose()
        {
            if (Port != null)
                Port.Close();
            if (Pipe != null)
                Pipe.Close();
            IsOpen = false;
        }
    }
}

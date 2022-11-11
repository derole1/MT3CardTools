using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

using MT3CardTools.Src.CardTools.ReaderNew.Models;

namespace MT3CardTools.Src.CardTools.ReaderNew
{
    class ReaderConnection : IDisposable
    {
        public SerialConnection Port { get; }

        public bool IsOpen => Port.IsOpen;
        public IResponse LastResponse { get; protected set; }

        public ReaderConnection(string port, SerialConnection.EPortType portType, int baudRate = 38400, Parity parity = Parity.Even)
        {
            Port = new SerialConnection(port, portType);
            Port.SetCOMParameters(baudRate, parity);
        }

        public async Task Open() => await Port.OpenAsync();

        public async Task<Init.Response> Init(bool arg0, bool arg1)
        {
            var req = new Init.Request
            {
                Arg0 = arg0,
                Arg1 = arg1
            };
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<Init.Response>(await Port.ReadDataAsync());
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<GetStatus.Response> GetStatus()
        {
            var req = new GetStatus.Request();
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<GetStatus.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<GetVersion.Response> GetVersion()
        {
            var req = new GetVersion.Request();
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<GetVersion.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<Read.Response> Read(Read.EMode mode, bool parity, Read.ETrack track)
        {
            var req = new Read.Request
            {
                Mode = mode,
                Parity = parity,
                Track = track
            };
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<Read.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<Write.Response> Write(Write.EMode mode, bool parity, Write.ETrack track, byte[] data)
        {
            var req = new Write.Request
            {
                Mode = mode,
                Parity = parity,
                Track = track,
                Data = data
            };
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<Write.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<PrintImage.Response> PrintImage(ushort nXw, ushort nYw, ushort nXp, ushort nYp, byte[] data)
        {
            for (int i=0; i<data.Length; i += 237)
            {
                var req = new PrintImage.Request
                {
                    Mode = 0x30,
                    BuffClr = 0x30,
                    Rect = new System.Drawing.Rectangle(nXp, nYp, nXw, nYw),
                    SequenceNum = (byte)(i / 237),
                    SequenceCount = (byte)(data.Length / 237),
                    Data = data.Skip(i).Take(Math.Min(data.Length - i, 237)).ToArray()
                };
                await Port.WriteDataAsync(req.Serialize());
                await Port.ReadAckAsync();
            }
            await Port.WriteEnqAsync();
            while (true)
            {
                var res = GetObject<PrintImage.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<GetCard.Response> GetCard(bool dispense)
        {
            var req = new GetCard.Request
            {
                Dispense = dispense
            };
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<GetCard.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<Eject.Response> Eject()
        {
            var req = new Eject.Request();
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<Eject.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<Cancel.Response> Cancel()
        {
            var req = new Cancel.Request();
            await Port.WriteDataAsync(req.Serialize());
            await ReadAckWriteEnq();
            while (true)
            {
                var res = GetObject<Cancel.Response>(await Port.ReadDataAsync());
                if (res == null)
                    return null;
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return res;
                await Port.WriteEnqAsync();
            }
        }

        public async Task<T> GenericEnq<T>() where T : new()
        {
            await Port.WriteEnqAsync();
            while (true)
            {
                var res = (IResponse)GetObject<T>(await Port.ReadDataAsync());
                if (res == null)
                    return default(T);
                if (res.S != ReaderConstants.ES.ExecutingCommand)
                    return (T)res;
                await Port.WriteEnqAsync();
            }
        }

        private async Task ReadAckWriteEnq()
        {
            await Port.ReadAckAsync();
            await Port.WriteEnqAsync();
        }

        private T GetObject<T>(byte[] data) where T : new()
        {
            if (data == null)
                return default(T);
            if (!typeof(IResponse).IsAssignableFrom(typeof(T)))
                throw new Exception($"Attempted to get object that does not inherit IResponse!");
            var c = (IResponse)new T();
            c.FromData(data);
            LastResponse = c;
            return (T)c;
        }

        public void Close() => Port.Close();
        public void Dispose() => Port.Dispose();
    }
}

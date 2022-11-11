using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.CardTools.Reader
{
    class RequestWriter : IDisposable
    {
        private MemoryStream _ms;
        private BinaryWriter _bw;

        public RequestWriter()
        {
            _ms = new MemoryStream();
            _bw = new BinaryWriter(_ms);
        }

        public BinaryWriter GetWriter() => _bw;

        public async Task SerializeAndSend(ReaderConnection conn)
        {
            if (conn.Port == null)
                await conn.Pipe.WriteAsync(Serialize());
            else
                await conn.Port.WriteAsync(Serialize());
            //await Task.Delay(10);   //Give reader some time
        }

        public void Write(byte value) => _bw.Write(value);
        public void Write(byte[] value) => _bw.Write(value);

        public byte[] Serialize()
        {
            _bw.Write(ReaderConstants.ETX);
            _bw.Flush();
            var data = _ms.ToArray();
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                byte payloadSize = (byte)(data.Length + 1);
                bw.Write(ReaderConstants.STX);
                bw.Write(payloadSize);
                bw.Write(data);
                bw.Write(ReaderConstants.Checksum(data, payloadSize));
                bw.Flush();
                return ms.ToArray();
            }
        }

        public void Dispose()
        {
            _ms.Dispose();
            _bw.Dispose();
        }
    }
}

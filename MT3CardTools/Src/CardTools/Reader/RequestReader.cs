using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MT3CardTools.Src.CardTools.Reader
{
    class RequestReader : IDisposable
    {
        public enum EResponseStatus : byte
        {
            OK,
            IncorrectSTX,
            IncorrectETX,
            IncorrectChecksum
        }

        private MemoryStream _ms;
        private BinaryReader _br;

        private MemoryStream ms;
        private BinaryReader br;

        public EResponseStatus ResponseStatus { get; }
        public long StreamPosition { get { return ms.Position; } set { ms.Position = value; } }
        public long StreamLength => ms.Length;

        public RequestReader(byte[] data)
        {
            _ms = new MemoryStream(data);
            _br = new BinaryReader(_ms);
            ResponseStatus = Process();
        }

        public EResponseStatus Process()
        {
            if (_br.ReadByte() == ReaderConstants.STX)
            {
                var toRead = _br.ReadByte();
                var data = _br.ReadBytes(toRead - 1);
                if (data[data.Length - 1] == ReaderConstants.ETX)
                {
                    if (_br.ReadByte() == ReaderConstants.Checksum(data, toRead))
                    {
                        ms = new MemoryStream(data);
                        br = new BinaryReader(ms);
                        return EResponseStatus.OK;
                    }
                    return EResponseStatus.IncorrectChecksum;
                }
                return EResponseStatus.IncorrectETX;
            }
            return EResponseStatus.IncorrectSTX;
        }

        public byte ReadByte() => br.ReadByte();
        public byte[] ReadBytes(int count) => br.ReadBytes(count);
        public string ReadString(Encoding enc) => enc.GetString(br.ReadBytes((int)(StreamLength - StreamPosition - 1)));

        public void Dispose()
        {
            _ms.Dispose();
            _br.Dispose();
        }
    }
}

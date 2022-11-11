using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MT3CardTools.Src.Helpers
{
    class BitReader : IDisposable
    {
        private BinaryReader _br;
        private byte _current;
        private int _index = 8;

        public bool CanRead { get; protected set; }
        public bool ReverseReadOrder { get; set; }

        public bool ReadBool() => BitConverter.ToBoolean(Read(1), 0);
        public byte ReadByte(int bitLength = 8) => Read(bitLength)[0];
        public short ReadInt16(int bitLength = 16) => BitConverter.ToInt16(Read(bitLength), 0);
        public ushort ReadUInt16(int bitLength = 16) => BitConverter.ToUInt16(Read(bitLength), 0);
        public int ReadInt32(int bitLength = 32) => BitConverter.ToInt32(Read(bitLength, 4), 0);
        public uint ReadUInt32(int bitLength = 32) => BitConverter.ToUInt32(Read(bitLength, 4), 0);
        public string ReadString(Encoding enc, int bitLength = 8) => enc.GetString(Read(bitLength)).Trim('\0');

        public BitReader(MemoryStream ms) => _br = new BinaryReader(ms);

        public byte[] Read(int bitLength, int arrayLength = -1)
        {
            var bits = new BitArray(bitLength);
            for (int i = 0; i < bitLength; i++)
                bits[i] = ReadBit();
            byte[] bytes = new byte[arrayLength == -1 ? (int)Math.Ceiling((decimal)bitLength / 8) : arrayLength];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        private bool ReadBit()
        {
            if (_index >= 8)
            {
                CanRead = _br.BaseStream.Position < _br.BaseStream.Length;
                if (CanRead)
                {
                    _current = _br.ReadByte();
                    _index = 0;
                }
            }
            return ((_current >> (ReverseReadOrder ? 7 - _index++ : _index++)) & 0x1) > 0;
        }

        public void Dispose() => _br.Close();
    }
}

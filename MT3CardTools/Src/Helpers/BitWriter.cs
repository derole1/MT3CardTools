using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.Helpers.Nam;

namespace MT3CardTools.Src.Helpers
{
    class BitWriter : IDisposable
    {
        private BinaryWriter _bw;
        private byte _current;
        private int _index = 0;

        public bool ReverseWriteOrder { get; set; }

        public void WriteBool(bool value) => Write(BitConverter.GetBytes(value), 1);
        public void WriteByte(byte value, int bitLength = 8) => Write(BitConverter.GetBytes(value), bitLength);
        public void WriteInt16(short value, int bitLength = 16) => Write(BitConverter.GetBytes(value), bitLength);
        public void WriteUInt16(ushort value, int bitLength = 16) => Write(BitConverter.GetBytes(value), bitLength);
        public void WriteInt32(int value, int bitLength = 32) => Write(BitConverter.GetBytes(value), bitLength);
        public void WriteUInt32(uint value, int bitLength = 32) => Write(BitConverter.GetBytes(value), bitLength);
        public void WriteString(string value, Encoding enc, int bitLength = 8) => Write(enc.GetBytes((value != null ? value : "")
            .PadRight(bitLength / 8, enc.EncodingName == Encoding.ASCII.EncodingName ? '\0' : '　')), bitLength);
        //public void WritePad(byte[] iv, int ivOffset, int bitLength) => Write(NamPad.GeneratePadding(iv[ivOffset], bitLength), bitLength);

        public BitWriter(MemoryStream ms) => _bw = new BinaryWriter(ms);

        public void Write(byte[] data, int bitLength)
        {
            if (data == null)
                data = new byte[(int)Math.Ceiling((decimal)bitLength / 8)];
            var bits = new BitArray(data);
            for (int i = 0; i < bitLength; i++)
                WriteBit(i < bits.Count ? bits[i] : false);
        }

        public void Flush()
        {
            if (_index > 0)
                _bw.Write(_current);
            _index = 0;
            _bw.Flush();
        }

        private void WriteBit(bool bit)
        {
            if (_index >= 8)
            {
                _bw.Write(_current);
                _index = 0;
            }
            if (bit)
                _current |= (byte)(1 << (ReverseWriteOrder ? 7 - _index++ : _index++));
            else
                _current &= (byte)~(1 << (ReverseWriteOrder ? 7 - _index++ : _index++));
        }

        public void Dispose() => _bw.Close();
    }
}

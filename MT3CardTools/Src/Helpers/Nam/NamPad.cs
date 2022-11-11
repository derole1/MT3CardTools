using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.CardTools;

namespace MT3CardTools.Src.Helpers.Nam
{
    class NamPad
    {
        public static byte[] PadKeyTable { get; set; } = Convert.FromBase64String(Properties.Settings.Default.PadKeyTable);

        public static byte[] GeneratePaddingData1(Card card, int bitLength, int arrayLength = -1)
        {
            using (var ms = new MemoryStream(GeneratePadding(card.Iv1[0], bitLength + 24, arrayLength)))
            using (var br = new BitReader(ms))
            {
                var pad = br.Read(bitLength);
                card.Unk_v322 = br.ReadByte();
                card.Sum_v322 = br.ReadByte();
                card.Status_v322 = br.ReadByte();
                return pad;
            }
        }

        public static byte[] GeneratePaddingData2(Card card, int bitLength, int arrayLength = -1)
            => GeneratePadding(card.Iv1[1], bitLength, arrayLength);

        private static byte[] GeneratePadding(byte ivByte, int bitLength, int arrayLength = -1)
        {
            var len = arrayLength == -1 ? (int)Math.Ceiling((decimal)bitLength / 8) : arrayLength;
            if (PadKeyTable.Length != 4096) //Wrong table
                return new byte[len];
            using (var ms = new MemoryStream())
            {
                for (int i = 0; i < len; i++)
                    ms.WriteByte(PadKeyTable[16 * ivByte + (i & 0xF)]);
                ms.Flush();
                return ms.ToArray();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace MT3CardTools.Src.Helpers
{
    static class BinaryExtensions
    {
        public static byte[] GetData(this byte[] buf, int offset, int length) => buf.Skip(offset).Take(length).ToArray();
        public static byte[] GetData(this BinaryReader br, int offset, int length)
        {
            var oldPos = br.BaseStream.Position;
            br.BaseStream.Position = offset;
            var data = br.ReadBytes(length);
            br.BaseStream.Position = oldPos;
            return data;
        }

        public static byte[] FromHex(this string hex)
        {
            hex = Regex.Replace(hex, "[^A-Fa-f0-9]", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ToHex(this byte[] data, int len = 32)
        {
            return Regex.Replace(BitConverter.ToString(data).Replace("-", ""), "(.{" + len + "})", "$1\r\n");
        }

        public static string GetTime(this uint value)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(value);
            return string.Format("{0:D2}'{1:D2}\"{2:D3}",
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
        }

        public static string GetTenFightRank(this byte value)
        {
            switch (value)
            {
                case 1:
                    return "B";
                case 2:
                    return "A";
                case 3:
                    return "S";
                default:
                    return "-";
            }
        }

        public static byte[] Hash(this byte[] data) => MD5.Create().ComputeHash(data);
    }
}

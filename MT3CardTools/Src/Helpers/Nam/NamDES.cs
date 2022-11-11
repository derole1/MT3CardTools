using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace MT3CardTools.Src.Helpers.Nam
{
    class NamDES
    {
        public static byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
        {
            using (var des = DES.Create())
            {
                des.KeySize = 64;
                des.BlockSize = 64;
                des.Padding = PaddingMode.None;

                des.Key = ExpandKey(key);
                des.IV = iv;

                using (var encryptor = des.CreateEncryptor(des.Key, des.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        public static byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
        {
            using (var des = DES.Create())
            {
                des.KeySize = 64;
                des.BlockSize = 64;
                des.Padding = PaddingMode.None;

                des.Key = ExpandKey(key);
                des.IV = iv;

                using (var decryptor = des.CreateDecryptor(des.Key, des.IV))
                {
                    return PerformCryptography(data, decryptor);
                }
            }
        }

        public static byte[] GenerateIV()
        {
            using (var des = DES.Create())
            {
                des.GenerateIV();
                return des.IV;
            }
        }

        //Add parity bits to DES key
        static byte[] ExpandKey(byte[] key)
        {
            using (var msA = new MemoryStream(key))
            using (var msB = new MemoryStream())
            using (var br = new BitReader(msA))
            using (var bw = new BitWriter(msB))
            {
                //I have no clue why I need to reverse these, DES is weird
                br.ReverseReadOrder = true;
                bw.ReverseWriteOrder = true;
                for (int i = 0; i < 64; i += 8)
                {
                    int bitCount = 0;
                    var b = br.ReadByte(7);
                    //Calculate parity
                    for (int x=0; x < 8; x++)
                    {
                        if ((b & (1 << x)) != 0)
                            bitCount++;
                    }
                    //Check if partiy is odd
                    if (bitCount % 2 != 0)
                        b |= 0x80;  //Set our parity bit
                    bw.WriteByte(b, 8); //Write the final bit
                }
                bw.Flush();
                return msB.ToArray();
            }
        }

        static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }
    }
}

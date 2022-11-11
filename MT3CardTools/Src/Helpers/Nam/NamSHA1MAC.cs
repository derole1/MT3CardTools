using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace MT3CardTools.Src.Helpers.Nam
{
    public class NamSHA1MAC
    {
        public class NamSignature
        {
            public byte[] Mac { get; }

            public NamSignature(byte[] mac) => Mac = mac;

            public override bool Equals(object obj)
            {
                var mac1 = ((NamSignature)obj).Mac;
                var mac2 = Mac;
                for (int i = 0; i < mac1.Length; i++)
                {
                    if (mac1[i] != mac2[i])
                        return false;
                }
                return true;
            }
            public override int GetHashCode() => BitConverter.ToInt32(Mac, 0);
        }

        public static NamSignature Sign(byte[] data, byte[] key)
        {
            using (var ms = new MemoryStream())
            using (var sha1 = SHA1.Create())
            {
                ms.Write(data, 0, data.Length);
                ms.Write(key, 0, key.Length);
                return new NamSignature(sha1.ComputeHash(ms.ToArray()).Take(8).ToArray());
            }
        }

        public static NamSignature GetSignature(byte[] mac) => new NamSignature(mac);
    }
}

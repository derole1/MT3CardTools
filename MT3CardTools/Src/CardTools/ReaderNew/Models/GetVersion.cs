using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.CardTools.ReaderNew.Models
{
    class GetVersion
    {
        const ReaderConstants.ERequestType REQUEST_MAGIC = ReaderConstants.ERequestType.GetVersion;

        public class Request : IRequest
        {
            public byte[] Serialize()
            {
                using (var ms = new MemoryStream())
                using (var bw = new EndiannessAwareBinaryWriter(ms, EEndianness.Big))
                {
                    bw.Write((byte)REQUEST_MAGIC);
                    bw.Write((byte)0);
                    bw.Write((byte)0);
                    bw.Write((byte)0);
                    bw.Flush();
                    return ms.ToArray();
                }
            }
        }

        public class Response : IResponse
        {
            public bool IsError { get; protected set; }

            public ReaderConstants.ER R { get; protected set; }
            public ReaderConstants.EP P { get; protected set; }
            public ReaderConstants.ES S { get; protected set; }
            public string Version { get; protected set; }

            public bool FromData(byte[] data)
            {
                using (var ms = new MemoryStream(data))
                using (var br = new EndiannessAwareBinaryReader(ms, EEndianness.Big))
                {
                    if (br.ReadByte() != (byte)REQUEST_MAGIC)
                    {
                        IsError = true;
                        return false;
                    }
                    R = (ReaderConstants.ER)br.ReadByte();
                    P = (ReaderConstants.EP)br.ReadByte();
                    S = (ReaderConstants.ES)br.ReadByte();
                    Version = Encoding.ASCII.GetString(br.ReadBytes(data.Length - 4));
                    return true;
                }
            }
        }
    }
}

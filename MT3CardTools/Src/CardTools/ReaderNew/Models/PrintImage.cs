using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.CardTools.ReaderNew.Models
{
    class PrintImage
    {
        const ReaderConstants.ERequestType REQUEST_MAGIC = ReaderConstants.ERequestType.PrintImage;

        public class Request : IRequest
        {
            public byte Mode { get; set; }
            public byte BuffClr { get; set; }
            public Rectangle Rect { get; set; }
            public byte SequenceNum { get; set; }
            public byte SequenceCount { get; set; }
            //1bpp, 1=B, 0=W
            public byte[] Data { get; set; }

            public byte[] Serialize()
            {
                using (var ms = new MemoryStream())
                using (var bw = new EndiannessAwareBinaryWriter(ms, EEndianness.Big))
                {
                    bw.Write((byte)REQUEST_MAGIC);
                    bw.Write((byte)0);
                    bw.Write((byte)0);
                    bw.Write((byte)0);
                    bw.Write(Mode);
                    bw.Write(BuffClr);
                    bw.Write((ushort)Rect.Width);
                    bw.Write((ushort)Rect.Height);
                    bw.Write((ushort)Rect.X);
                    bw.Write((ushort)Rect.Y);
                    bw.Write(SequenceNum);
                    bw.Write(SequenceCount);
                    bw.Write(Data);
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
                    return true;
                }
            }
        }
    }
}

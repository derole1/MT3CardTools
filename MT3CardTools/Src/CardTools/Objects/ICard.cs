using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.CardTools.Objects
{
    public interface ICard
    {
        void Load(byte[] data1, byte[] data2);
        void GeneratePad(Card card);
        byte[] Data1Serialize();
        byte[] Data2Serialize();
    }
}

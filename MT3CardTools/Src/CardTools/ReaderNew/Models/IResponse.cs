using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.CardTools.ReaderNew.Models
{
    interface IResponse
    {
        bool IsError { get; }
        ReaderConstants.ER R { get; }
        ReaderConstants.EP P { get; }
        ReaderConstants.ES S { get; }

        bool FromData(byte[] data);
    }
}

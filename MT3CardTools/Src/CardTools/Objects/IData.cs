﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.CardTools.Objects
{
    public interface IData
    {
        byte[] Serialize();
    }
}

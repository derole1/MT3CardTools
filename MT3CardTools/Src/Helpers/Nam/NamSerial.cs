using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.Helpers.Nam
{
    class NamSerial
    {
        private static Random rnd = new Random();

        public static int GenerateMachineId() => rnd.Next(0, 2) == 0 ? rnd.Next(100000, 200000) : rnd.Next(300000, 1000000);    // 1048576 = Upper limit
    }
}

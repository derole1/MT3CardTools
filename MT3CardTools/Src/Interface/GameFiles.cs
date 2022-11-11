using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.Interface;
using MT3CardTools.Src.CardTools;

namespace MT3CardTools.Src.Interface
{
    class GameFiles
    {
        public Card GetCard(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Card data not found at {fileName}");
            var data = File.ReadAllBytes(fileName);
            return new Card(data);
        }
    }
}

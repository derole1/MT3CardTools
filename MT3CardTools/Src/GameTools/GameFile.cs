using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.GameTools
{
    class GameFile : IDisposable
    {
        public class Keys
        {
            public byte[] Data1Key { get; set; }
            public byte[] Data2KeyTable { get; set; }
            public byte[] MacKeyTable { get; set; }
            public byte[] PadKeyTable { get; set; }
        }

        public FileStream BaseStream { get; }

        private BinaryReader _br;

        public GameFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                BaseStream = File.OpenRead(fileName);
                _br = new BinaryReader(BaseStream);
            }
        }

        public Keys GetKeys()
        {
            if (_br.BaseStream.Length < 256)
            {
                Log.Error($"GetKeys: Length of file <256 bytes");
                return null;
            }
            var baseAddress = 0;
            var hasPad = true;
            var hash = _br.ReadBytes(256).Hash().ToHex().Replace("\r\n", "");
            switch (hash)
            {
                case "22A6CBE924FFEDDC51E1F4CCBA0EFD5A":    // WM3100-1-NA-DAT0-A70
                    Log.Info("GetKeys: Detected WM3100-1-NA-DAT0-A70");
                    baseAddress = 0x12D0720;
                    hasPad = false;
                    break;
                case "E66A3F2DB72F4F88D2261F7AB2F1D1DC":    // WM3100-2-NA-DAT0-A70
                    Log.Info("GetKeys: Detected WM3100-2-NA-DAT0-A70");
                    baseAddress = 0x12F9B60;
                    hasPad = false;
                    break;
                case "60575E1EDBD64F33BBF644A324D7EEB0":    // WM3100-3-NA-DAT0-A70
                    Log.Info("GetKeys: Detected WM3100-3-NA-DAT0-A70");
                    baseAddress = 0x12EC9C0;
                    hasPad = false;
                    break;
                case "1767E2DE0452A5F1842CE596B77B5B21":    // W3X100-4-NA-DAT0-A20
                    Log.Info("GetKeys: Detected W3X100-4-NA-DAT0-A20");
                    baseAddress = 0x1393560;
                    break;
                case "5DEC42B9398C1F8A4F9C5BD75EBD0644":    // W3P100-2-NA-DAT0-A16
                    Log.Info("GetKeys: Detected W3P100-2-NA-DAT0-A16");
                    baseAddress = 0x139F780;
                    break;
                case "3CAFF45933F1BFF735C42E9C3FC03071":    // W3P100-1-NA-DAT0-B02
                    Log.Info("GetKeys: Detected W3P100-1-NA-DAT0-B02");
                    baseAddress = 0x13997A0;
                    break;
                case "08D06625D73F02761A8827E036C99BE1":    // W3P100-2-NA-DAT0-B02 (Bootleg?)
                    Log.Info("GetKeys: Detected W3P100-2-NA-DAT0-B02");
                    baseAddress = 0x139F8C0;
                    break;
                default:
                    Log.Error($"GetKeys: Unknown hash {hash}");
                    return null;
            }
            _br.BaseStream.Position = baseAddress;
            return new Keys
            {
                MacKeyTable = _br.ReadBytes(128),
                Data2KeyTable = _br.ReadBytes(112),
                Data1Key = _br.ReadBytes(16).Take(8).ToArray(),
                PadKeyTable = hasPad ? _br.ReadBytes(4096) : new byte[0]
            };
        }

    //    return new Keys
    //        {
    //            Data1Key = new byte[8],
    //            Data2KeyTable = new byte[112],
    //            MacKeyTable = new byte[128],
    //            PadKeyTable = new byte[4096]
    //};

    public void Close()
        {
            _br.Close();
            BaseStream.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.CardTools.Objects;
using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Helpers.Nam;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.CardTools
{
    public class Card
    {
        const int DATA1_SIZE = 56;
        const int DATA2_SIZE = 120;
        const int MAC_CHECK_SIZE = 197;
        const int SUM_CHECK_SIZE = 205;

        const int DATA2_CRYPT_OFFSET = 19;
        const int MAC_SIGN_OFFSET = 15;

        public enum EVersion : ushort
        {
            v307 = 0x7651,
            v322_EXP = 0x8127,
            v322_JPN = 0x4816,
            v337_EXP = 0xDFB6,
            v337_JPN = 0x0FDE,
            v337_EXP_LOC_TEST = 0x073D,
            v337_JPN_LOC_TEST = 0x2B10,
            v363_EXP = 0x4563,
            v363_JPN = 0xD4EF,
            v363_EXP_LOC_TEST_A = 0xA02D,
            v363_JPN_LOC_TEST_A = 0x0D99,
            v363_EXP_LOC_TEST_B = 0xFDB3,
            v363_JPN_LOC_TEST_B = 0x8550,
            v386_EXP = 0xE71F,
            v386_JPN = 0x1D2B
        }

        public byte[] Data1Key { get; } = Convert.FromBase64String(Properties.Settings.Default.Data1Key);
        public byte[] Data2KeyTable { get; } = Convert.FromBase64String(Properties.Settings.Default.Data2KeyTable);
        public byte[] MacKeyTable { get; } = Convert.FromBase64String(Properties.Settings.Default.MacKeyTable);

        public EVersion Version { get; set; }   //TEMP PUBLIC
        public byte[] Iv1 { get; set; }
        public byte[] Data1 { get; set; }
        public byte Unk_v322 { get; set; }
        public byte Sum_v322 { get; set; }
        public byte Status_v322 { get; set; }
        public byte[] Iv2 { get; set; }
        public byte[] Data2 { get; set; }
        public NamSHA1MAC.NamSignature Mac { get; set; }
        public ushort Sum { get; set; }

        public bool HasCorrectMac { get; protected set; }
        public bool HasCorrectSum { get; protected set; }

        public byte[] RawData { get; protected set; }

        public Card(EVersion version)
        {
            Version = version;
            Iv1 = NamDES.GenerateIV();
            Iv2 = NamDES.GenerateIV();
        }
        public Card(Card parentCard, EVersion version)
        {
            Version = version;
            Iv1 = parentCard.Iv1;
            Iv2 = parentCard.Iv2;
            Unk_v322 = parentCard.Unk_v322;
            Sum_v322 = parentCard.Sum_v322;
            Status_v322 = parentCard.Status_v322;
        }
        public Card(byte[] data) => RawData = data;

        public void Read()
        {
            using (var ms = new MemoryStream(RawData))
            using (var br = new BinaryReader(ms))
            {
                Version = (EVersion)br.ReadUInt16();
                Iv1 = br.ReadBytes(8);
                Data1 = NamDES.Decrypt(Data1Key, Iv1, br.ReadBytes(DATA1_SIZE));
                Unk_v322 = br.ReadByte();
                Sum_v322 = br.ReadByte();
                Status_v322 = br.ReadByte();
                Iv2 = br.ReadBytes(8);
                Data2 = NamDES.Decrypt(Data2KeyTable.GetData(7 * (BitConverter.ToInt32(Data1, DATA2_CRYPT_OFFSET) & 0xF), 8), Iv2, br.ReadBytes(DATA2_SIZE));
                Mac = NamSHA1MAC.GetSignature(br.ReadBytes(8));
                Sum = br.ReadUInt16();

                ms.Position = 0;
                HasCorrectMac = NamSHA1MAC.Equals(NamSHA1MAC.Sign(br.ReadBytes(MAC_CHECK_SIZE), MacKeyTable.GetData(8 * ((BitConverter.ToInt16(Data1, MAC_SIGN_OFFSET) >> 12) & 0xF), 8)), Mac);
                ms.Position = 0;
                HasCorrectSum = NamSum.Calculate(br.ReadBytes(SUM_CHECK_SIZE)) == Sum;
            }
        }

        public T GetObject<T>() where T : new()
        {
            if (!typeof(ICard).IsAssignableFrom(typeof(T)))
                throw new Exception($"Attempted to get object that does not inherit ICard!");
            var c = (ICard)new T();
            c.Load(Data1, Data2);
            return (T)c;
        }

        public void SetObject(ICard card)
        {
            Data1 = card.Data1Serialize();
            Log.Debug($"SetObject: Data1Length:{Data1.Length}");
            Data2 = card.Data2Serialize();
            Log.Debug($"SetObject: Data2Length:{Data2.Length}");
        }

        public void Write()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((ushort)Version);
                bw.Write(Iv1);
                bw.Write(NamDES.Encrypt(Data1Key, Iv1, Data1));
                bw.Write(Unk_v322);
                bw.Write(Sum_v322);
                bw.Write(Status_v322);
                bw.Write(Iv2);
                bw.Write(NamDES.Encrypt(Data2KeyTable.GetData(7 * (BitConverter.ToInt32(Data1, DATA2_CRYPT_OFFSET) & 0xF), 8), Iv2, Data2));
                bw.Flush();
                bw.Write(NamSHA1MAC.Sign(ms.ToArray(), MacKeyTable.GetData(8 * ((BitConverter.ToInt16(Data1, MAC_SIGN_OFFSET) >> 12) & 0xF), 8)).Mac);
                bw.Flush();
                bw.Write(NamSum.Calculate(ms.ToArray()));
                bw.Flush();
                RawData = ms.ToArray();
            }
        }

        public static EVersion PeekVersion(string fileName)
        {
            var cardType = CardFile.DetermineCardType(ref fileName);
            fileName = fileName + (cardType == CardFile.ECardType.TrackSplit ? ".track_0" : "");
            if (File.Exists(fileName))
            {
                try
                {
                    using (var fs = File.OpenRead(fileName))
                    using (var br = new BinaryReader(fs))
                    {
                        return (EVersion)br.ReadUInt16();
                    }
                }
                catch
                {
                    Log.Error("PeekVersion: Card file in use!");
                    return 0;
                }
            }
            return 0;
        }
    }
}

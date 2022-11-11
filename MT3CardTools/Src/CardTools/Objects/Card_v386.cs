using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Helpers.Nam;

namespace MT3CardTools.Src.CardTools.Objects
{
    public class Card_v386 : ICard
    {
        const int DATA1_PAD = 34;
        const int DATA2_PAD = 1;

        public class Data1 : IData
        {
            public string Name { get; set; }
            public byte SpecialTitle { get; set; }
            public bool HasSpecialTitle { get; set; }
            public bool HasMaxi2NoLosePoint { get; set; }
            public bool HasMaxi2FullTunedPoint { get; set; }
            public uint RenewalId1 { get; set; }
            public uint RenewalId2 { get; set; }
            public bool RenewalIdDevel { get; set; }
            public uint Id1 { get; set; }
            public uint Id2 { get; set; }
            public bool IdDevel { get; set; }
            public bool IsDtor { get; set; }
            public byte UnTunePower { get; set; }
            public byte UnTuneHandle { get; set; }
            public string SName { get; set; }
            public byte UnCoolOrWild { get; set; }
            public byte UnSmoothOrRough { get; set; }
            public ushort UnBattleNum { get; set; }
            public byte UnTitleRankF { get; set; }
            public uint UnStoryFlag { get; set; }
            public byte UnStoryLap { get; set; }
            public bool BIsStoryLose { get; set; }
            public byte UnStoryLose { get; set; }
            public ushort UnBattleWin { get; set; }
            public ushort UnTitleRankB { get; set; }
            public byte UnWheel { get; set; }
            public bool BIsPresent { get; set; }
            public bool BIsAero { get; set; }
            public byte ECarType { get; set; }
            public byte UnBodyColor { get; set; }
            public List<ushort> UnTenRivalInfo { get; set; } = new List<ushort>();
            public byte UnStoryClearLoop { get; set; }
            public bool BBGMSpecial { get; set; }
            public ushort Key { get; set; }
            public bool IsPresentCard { get; set; }
            public byte[] Pad { get; set; }

            public Data1()
            {
                UnTenRivalInfo.AddRange(new ushort[5]);
            }
            public Data1(byte[] data)
            {
                using (var ms = new MemoryStream(data))
                using (var br = new BitReader(ms))
                {
                    Name = br.ReadString(Encoding.Unicode, 80);
                    SpecialTitle = br.ReadByte();
                    HasSpecialTitle = br.ReadBool();
                    HasMaxi2NoLosePoint = br.ReadBool();
                    HasMaxi2FullTunedPoint = br.ReadBool();
                    RenewalId1 = br.ReadUInt32(20);
                    RenewalId2 = br.ReadUInt32(20);
                    RenewalIdDevel = br.ReadBool();
                    Id1 = br.ReadUInt32(20);
                    Id2 = br.ReadUInt32(20);
                    IdDevel = br.ReadBool();
                    IsDtor = br.ReadBool();
                    UnTunePower = br.ReadByte(5);
                    UnTuneHandle = br.ReadByte(5);
                    SName = br.ReadString(Encoding.ASCII, 40);
                    UnCoolOrWild = br.ReadByte(3);
                    UnSmoothOrRough = br.ReadByte(3);
                    UnBattleNum = br.ReadUInt16(13);
                    UnTitleRankF = br.ReadByte(5);
                    UnStoryFlag = br.ReadUInt32(20);
                    UnStoryLap = br.ReadByte(2);
                    BIsStoryLose = br.ReadBool();
                    UnStoryLose = br.ReadByte(3);
                    UnBattleWin = br.ReadUInt16(13);
                    UnTitleRankB = br.ReadUInt16(11);
                    UnWheel = br.ReadByte(6);
                    BIsPresent = br.ReadBool();
                    BIsAero = br.ReadBool();
                    ECarType = br.ReadByte(5);
                    UnBodyColor = br.ReadByte(3);
                    for (int i = 0; i < 5; i++)
                        UnTenRivalInfo.Add(br.ReadUInt16());
                    UnStoryClearLoop = br.ReadByte();
                    BBGMSpecial = br.ReadBool();
                    Key = br.ReadUInt16(10);
                    IsPresentCard = br.ReadBool();
                    Pad = br.Read(DATA1_PAD);
                }
            }

            public byte[] Serialize()
            {
                using (var ms = new MemoryStream())
                using (var bw = new BitWriter(ms))
                {
                    bw.WriteString(Name, Encoding.Unicode, 80);
                    bw.WriteByte(SpecialTitle);
                    bw.WriteBool(HasSpecialTitle);
                    bw.WriteBool(HasMaxi2NoLosePoint);
                    bw.WriteBool(HasMaxi2FullTunedPoint);
                    bw.WriteUInt32(RenewalId1, 20);
                    bw.WriteUInt32(RenewalId2, 20);
                    bw.WriteBool(RenewalIdDevel);
                    bw.WriteUInt32(Id1, 20);
                    bw.WriteUInt32(Id2, 20);
                    bw.WriteBool(IdDevel);
                    bw.WriteBool(IsDtor);
                    bw.WriteByte(UnTunePower, 5);
                    bw.WriteByte(UnTuneHandle, 5);
                    bw.WriteString(SName, Encoding.ASCII, 40);
                    bw.WriteByte(UnCoolOrWild, 3);
                    bw.WriteByte(UnSmoothOrRough, 3);
                    bw.WriteUInt16(UnBattleNum, 13);
                    bw.WriteByte(UnTitleRankF, 5);
                    bw.WriteUInt32(UnStoryFlag, 20);
                    bw.WriteByte(UnStoryLap, 2);
                    bw.WriteBool(BIsStoryLose);
                    bw.WriteByte(UnStoryLose, 3);
                    bw.WriteUInt16(UnBattleWin, 13);
                    bw.WriteUInt16(UnTitleRankB, 11);
                    bw.WriteByte(UnWheel, 6);
                    bw.WriteBool(BIsPresent);
                    bw.WriteBool(BIsAero);
                    bw.WriteByte(ECarType, 5);
                    bw.WriteByte(UnBodyColor, 3);
                    for (int i = 0; i < 5; i++)
                        bw.WriteUInt16(UnTenRivalInfo[i]);
                    bw.WriteByte(UnStoryClearLoop);
                    bw.WriteBool(BBGMSpecial);
                    bw.WriteUInt16(Key, 10);
                    bw.WriteBool(IsPresentCard);
                    // Now, we SHOULD regenerate this padding each write, but I really cbf, it only changes if the IV changes anyways.
                    bw.Write(Pad, DATA1_PAD);
                    bw.Flush();
                    return ms.ToArray();
                }
            }

            public override string ToString()
            {
                string output = "Data1\n\n";
                foreach (var property in GetType().GetProperties())
                {
                    if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                        continue;

                    output += $"{property.Name}:{property.GetValue(this)}\n";
                }
                return output;
            }
        }

        public class Data2 : IData
        {
            public class TenResult
            {
                public byte Course { get; set; }
                public byte Level { get; set; }
                public byte Result { get; set; }
            }

            public class TA
            {
                public byte Power { get; set; }
                public uint Time { get; set; }
            }

            public byte Car { get; set; }
            public byte CustomColor { get; set; }
            public byte Vinyl { get; set; }
            public byte Bonnet { get; set; }
            public byte Wing { get; set; }
            public byte Aero { get; set; }
            public byte AeroMirror { get; set; }
            public byte Wheel { get; set; } = 143;
            public byte Neon { get; set; }
            public byte TunePower { get; set; }
            public byte TuneHandling { get; set; }
            public ushort Dress { get; set; }
            public byte DressX { get; set; }
            public byte Overhaul { get; set; } = 60;
            public byte Disused337 { get; set; }
            public ushort Renewal { get; set; }
            public bool View { get; set; } = true;
            public bool Mission { get; set; }
            public bool Retire { get; set; }
            public bool Meter337 { get; set; }
            public byte Volume { get; set; } = 2;
            public byte Disc { get; set; }
            public byte PrintPassType { get; set; }
            public byte LastPassType { get; set; }
            public ushort Title { get; set; } = 1;
            public byte Class { get; set; }
            public uint OdoCount { get; set; }
            public byte TunePoint { get; set; }
            public ushort DressPoint { get; set; }
            public uint JoinPlayCount { get; set; }
            public uint JoinStarCount { get; set; }
            public byte CoolWild { get; set; } = 3;
            public byte SmoothRough { get; set; } = 3;
            public bool HasStoryNoLosePoint { get; set; }
            public bool HasStoryLose { get; set; }
            public uint StoryClearBits { get; set; }
            public ushort StoryClearDivCount337 { get; set; }
            public List<byte> StoryLoseCount { get; set; } = new List<byte>();
            public uint TargetPlayCount { get; set; }
            public uint TargetWinCount { get; set; }
            public List<TenResult> TenResults { get; set; } = new List<TenResult>();
            public List<TA> Ta { get; set; } = new List<TA>();
            public byte Meter { get; set; }
            public ushort StoryClearDivCount { get; set; }
            public uint StorySuccessiveVictoryCount { get; set; }
            public bool HasStoryNoLosePoint2 { get; set; }
            public bool HasStoryClearPoint { get; set; }
            public uint MaxiCoin { get; set; }
            public uint StorySuccessiveVictoryCountMax { get; set; }
            public uint StoryClearCount { get; set; }
            public uint TournamentVictoryCount { get; set; }
            public uint TournamentHigherLevelVictoryCount { get; set; }
            public byte[] Pad { get; set; }

            public Data2()
            {
                StoryLoseCount.AddRange(new byte[20]);
                for (byte y = 0; y < 10; y++)
                {
                    for (byte x = 0; x < 10; x++)
                        TenResults.Add(new TenResult
                        {
                            Course = y,
                            Level = x,
                            Result = 0
                        });
                }
                for (int i = 0; i < 15; i++)
                    Ta.Add(new TA
                    {
                        Power = 0,
                        Time = 0
                    });
            }
            public Data2(byte[] data)
            {
                using (var ms = new MemoryStream(data))
                using (var br = new BitReader(ms))
                {
                    Car = br.ReadByte();
                    CustomColor = br.ReadByte(5);
                    Vinyl = br.ReadByte();
                    Bonnet = br.ReadByte(3);
                    Wing = br.ReadByte(3);
                    Aero = br.ReadByte(3);
                    AeroMirror = br.ReadByte(2);
                    Wheel = br.ReadByte();
                    Neon = br.ReadByte(4);
                    TunePower = br.ReadByte(5);
                    TuneHandling = br.ReadByte(5);
                    Dress = br.ReadUInt16();
                    DressX = br.ReadByte(5);
                    Overhaul = br.ReadByte(6);
                    Disused337 = br.ReadByte(2);
                    Renewal = br.ReadUInt16();
                    View = br.ReadBool();
                    Mission = br.ReadBool();
                    Retire = br.ReadBool();
                    Meter337 = br.ReadBool();
                    Volume = br.ReadByte(2);
                    Disc = br.ReadByte(2);
                    PrintPassType = br.ReadByte(5);
                    LastPassType = br.ReadByte(5);
                    Title = br.ReadUInt16();
                    Class = br.ReadByte(6);
                    OdoCount = br.ReadUInt32(24);
                    TunePoint = br.ReadByte(5);
                    DressPoint = br.ReadUInt16(10);
                    JoinPlayCount = br.ReadUInt32(20);
                    JoinStarCount = br.ReadUInt32(20);
                    CoolWild = br.ReadByte(3);
                    SmoothRough = br.ReadByte(3);
                    HasStoryNoLosePoint = br.ReadBool();
                    HasStoryLose = br.ReadBool();
                    StoryClearBits = br.ReadUInt32(20);
                    StoryClearDivCount337 = br.ReadUInt16(11);
                    for (int i = 0; i < 20; i++)
                        StoryLoseCount.Add(br.ReadByte(2));
                    TargetPlayCount = br.ReadUInt32(20);
                    TargetWinCount = br.ReadUInt32(20);
                    for (byte y = 0; y < 10; y++)
                    {
                        for (byte x = 0; x < 10; x++)
                            TenResults.Add(new TenResult
                            {
                                Course = y,
                                Level = x,
                                Result = br.ReadByte(2)
                            });
                    }
                    for (int i = 0; i < 12; i++)
                        Ta.Add(new TA
                        {
                            Power = br.ReadByte(5),
                            Time = br.ReadUInt32(20)
                        });
                    Meter = br.ReadByte(2);
                    StoryClearDivCount = br.ReadUInt16(13);
                    StorySuccessiveVictoryCount = br.ReadUInt32(17);
                    HasStoryNoLosePoint2 = br.ReadBool();
                    HasStoryClearPoint = br.ReadBool();
                    MaxiCoin = br.ReadUInt32(20);
                    StorySuccessiveVictoryCountMax = br.ReadUInt32(17);
                    StoryClearCount = br.ReadUInt32(17);
                    TournamentVictoryCount = br.ReadUInt32(17);
                    TournamentHigherLevelVictoryCount = br.ReadUInt32(17);
                    Pad = br.Read(DATA2_PAD);
                }
            }

            public byte[] Serialize()
            {
                using (var ms = new MemoryStream())
                using (var bw = new BitWriter(ms))
                {
                    bw.WriteByte(Car);
                    bw.WriteByte(CustomColor, 5);
                    bw.WriteByte(Vinyl);
                    bw.WriteByte(Bonnet, 3);
                    bw.WriteByte(Wing, 3);
                    bw.WriteByte(Aero, 3);
                    bw.WriteByte(AeroMirror, 2);
                    bw.WriteByte(Wheel);
                    bw.WriteByte(Neon, 4);
                    bw.WriteByte(TunePower, 5);
                    bw.WriteByte(TuneHandling, 5);
                    bw.WriteUInt16(Dress);
                    bw.WriteByte(DressX, 5);
                    bw.WriteByte(Overhaul, 6);
                    bw.WriteByte(Disused337, 2);
                    bw.WriteUInt16(Renewal);
                    bw.WriteBool(View);
                    bw.WriteBool(Mission);
                    bw.WriteBool(Retire);
                    bw.WriteBool(Meter337);
                    bw.WriteByte(Volume, 2);
                    bw.WriteByte(Disc, 2);
                    bw.WriteByte(PrintPassType, 5);
                    bw.WriteByte(LastPassType, 5);
                    bw.WriteUInt16(Title);
                    bw.WriteByte(Class, 6);
                    bw.WriteUInt32(OdoCount, 24);
                    bw.WriteByte(TunePoint, 5);
                    bw.WriteUInt16(DressPoint, 10);
                    bw.WriteUInt32(JoinPlayCount, 20);
                    bw.WriteUInt32(JoinStarCount, 20);
                    bw.WriteByte(CoolWild, 3);
                    bw.WriteByte(SmoothRough, 3);
                    bw.WriteBool(HasStoryNoLosePoint);
                    bw.WriteBool(HasStoryLose);
                    bw.WriteUInt32(StoryClearBits, 20);
                    bw.WriteUInt16(StoryClearDivCount337, 11);
                    for (int i = 0; i < 20; i++)
                        bw.WriteByte(StoryLoseCount[i], 2);
                    bw.WriteUInt32(TargetPlayCount, 20);
                    bw.WriteUInt32(TargetWinCount, 20);
                    for (byte y = 0; y < 10; y++)
                    {
                        for (byte x = 0; x < 10; x++)
                            bw.WriteByte(TenResults.Where(obj => obj.Course == y && obj.Level == x).FirstOrDefault().Result, 2);
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        bw.WriteByte(Ta[i].Power, 5);
                        bw.WriteUInt32(Ta[i].Time, 20);
                    }
                    bw.WriteByte(Meter, 2);
                    bw.WriteUInt16(StoryClearDivCount, 13);
                    bw.WriteUInt32(StorySuccessiveVictoryCount, 17);
                    bw.WriteBool(HasStoryNoLosePoint2);
                    bw.WriteBool(HasStoryClearPoint);
                    bw.WriteUInt32(MaxiCoin, 20);
                    bw.WriteUInt32(StorySuccessiveVictoryCountMax, 17);
                    bw.WriteUInt32(StoryClearCount, 17);
                    bw.WriteUInt32(TournamentVictoryCount, 17);
                    bw.WriteUInt32(TournamentHigherLevelVictoryCount, 17);
                    bw.Write(Pad, DATA2_PAD);
                    bw.Flush();
                    return ms.ToArray();
                }
            }

            public override string ToString()
            {
                string output = "Data2\n\n";
                foreach (var property in GetType().GetProperties())
                {
                    if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                        continue;

                    output += $"{property.Name}:{property.GetValue(this)}\n";
                }
                return output;
            }
        }

        public Data1 Data_1 { get; protected set; } = new Data1();
        public Data2 Data_2 { get; protected set; } = new Data2();

        public void Load(byte[] data1, byte[] data2)
        {
            Data_1 = new Data1(data1);
            Data_2 = new Data2(data2);
        }

        public byte[] Data1Serialize() => Data_1.Serialize();
        public byte[] Data2Serialize() => Data_2.Serialize();

        public void GeneratePad(Card card)
        {
            Data_1.Pad = NamPad.GeneratePaddingData1(card, DATA1_PAD);
            Data_2.Pad = NamPad.GeneratePaddingData2(card, DATA2_PAD);
        }

        public override string ToString()
        {
            string output = "Card_v386\n\n";
            foreach (var property in GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;

                output += $"{property.Name}:{property.GetValue(this)}\n";
            }
            return output;
        }
    }
}

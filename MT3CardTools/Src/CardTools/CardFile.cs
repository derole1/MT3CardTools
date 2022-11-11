using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MT3CardTools.Src.Interface;
using MT3CardTools.Src.CardTools.Objects;

namespace MT3CardTools.Src.CardTools
{
    public class CardFile : IDisposable
    {
        const int TRACK_SIZE = 0x45;

        const int RECENT_CARDS_SIZE = 10;

        public enum ECardType
        {
            SingleFile,
            TrackSplit,
            Invalid
        }

        public string FileName { get; protected set; }
        public ECardType CardType { get; }
        public Card BaseCard { get; }

        private List<FileStream> CardLocks { get; } = new List<FileStream>();

        public CardFile(string fileName)
        {
            AddFileToRecents(fileName);
            CardType = DetermineCardType(ref fileName);
            FileName = fileName;
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                switch (CardType)
                {
                    case ECardType.SingleFile:
                        if (!File.Exists(FileName))
                            throw new Exception($"Card does not exist at path {FileName}");
                        bw.Write(File.ReadAllBytes(FileName));
                        CardLocks.Add(File.OpenRead(fileName));
                        break;
                    case ECardType.TrackSplit:
                        for (int i=0; i<3; i++)
                        {
                            var trackFileName = $"{FileName}.track_{i}";
                            if (!File.Exists(trackFileName))
                                throw new Exception($"Card track does not exist at path {trackFileName}");
                            bw.Write(File.ReadAllBytes(trackFileName));
                            CardLocks.Add(File.OpenRead(trackFileName));
                        }
                        break;
                    default:
                        throw new Exception($"Unknown ECardType {CardType}");
                }
                bw.Flush();
                BaseCard = new Card(ms.ToArray());
                BaseCard.Read();
                if (!BaseCard.HasCorrectMac)
                    Msg.Warning("This card has an incorrect MAC! Data may have been corrupted or damaged.");
                else
                {
                    if (!BaseCard.HasCorrectSum)
                        Msg.Warning("This card has an incorrect checksum! Data may have been corrupted or damaged.");
                }
            }
        }

        public CardFile(string fileName, Card.EVersion version)
        {
            FileName = fileName;
            CardType = FileName.Contains(".track_") ? ECardType.TrackSplit : ECardType.SingleFile;
            BaseCard = new Card(version);
            AddFileToRecents(fileName);
        }

        public CardFile(CardFile parent, string fileName, Card.EVersion version)
        {
            FileName = fileName;
            CardType = FileName.Contains(".track_") ? ECardType.TrackSplit : ECardType.SingleFile;
            BaseCard = parent.BaseCard;
            AddFileToRecents(fileName);
        }

        public void Save() => Save(FileName);

        public void Save(string fileName)
        {
            if (!Properties.Settings.Default.CardEditor_HideUnsupportedCarsWarning &&
                BaseCard.GetObject<Card_v337>().Data_2.Car < 15 && Enum.GetName(typeof(Card.EVersion), BaseCard.Version).Split('_')[1] != "JPN")
                Msg.Warning("The selected car will cause the card to not work unless your game is modified to both allow cards with this car and you have restored the required files!\r\n\r\n" +
                     "This card likely will not load.");
            fileName = fileName.Replace(".track_0", "");
            BaseCard.Write();
            UnlockAllCards();
            switch (CardType)
            {
                case ECardType.SingleFile:
                    File.WriteAllBytes(fileName, BaseCard.RawData);
                    CardLocks.Add(File.OpenRead(fileName));
                    break;
                case ECardType.TrackSplit:
                    using (var ms = new MemoryStream(BaseCard.RawData))
                    using (var br = new BinaryReader(ms))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var trackFileName = $"{fileName}.track_{i}";
                            File.WriteAllBytes(trackFileName, br.ReadBytes(TRACK_SIZE));
                            CardLocks.Add(File.OpenRead(trackFileName));
                        }
                    }
                    break;
                default:
                    throw new Exception($"Unknown ECardType {CardType}");
            }
        }

        public void UpdateFileName(string fileName) => FileName = fileName;

        public static ECardType DetermineCardType(ref string fileName)
        {
            if (fileName.Contains(".track_"))
            {
                //if (!fileName.Contains(".track_0"))
                //    return ECardType.Invalid;
                fileName = fileName.Substring(0, fileName.IndexOf(".track_"));
                return ECardType.TrackSplit;
            }
            return ECardType.SingleFile;
        }

        void AddFileToRecents(string fileName)
        {
            if (Properties.Settings.Default.RecentCards.Contains(fileName))
                Properties.Settings.Default.RecentCards.Remove(fileName);
            Properties.Settings.Default.RecentCards.Insert(0, fileName);
            if (Properties.Settings.Default.RecentCards.Count > RECENT_CARDS_SIZE)
                Properties.Settings.Default.RecentCards.RemoveAt(Properties.Settings.Default.RecentCards.Count - 1);
            Properties.Settings.Default.Save();
        }

        void UnlockAllCards()
        {
            for (int i=0; i<CardLocks.Count; i++)
            {
                CardLocks[i].Close();
            }
            CardLocks.Clear();
        }

        public void Close() => UnlockAllCards();

        public void Dispose() => Close();

        public static string GetFileDialogFilter(int defaultType)
        {
            //OLD
            //return (defaultType == 0 ? "Split card bin (*.bin.track_0)|*.bin.track_0|Card bin (*.bin)|*.bin" :
            //    "Card bin (*.bin)|*.bin|Split card bin (*.bin.track_0)|*.bin.track_0") + (Properties.Settings.Default.CardEditor_AllowAllFiles ? "|All Files (*.*)|*.*" : "");
            //NEW
            return (defaultType == 0 ? "Split card bin (*.track_0)|*.track_0|Card bin (*.bin)|*.bin" :
                "Card bin (*.bin)|*.bin|Split card bin (*.track_0)|*.track_0") + (Properties.Settings.Default.CardEditor_AllowAllFiles ? "|All Files (*.*)|*.*" : "");
        }
        public static string GetFileDialogFilter() => GetFileDialogFilter(Properties.Settings.Default.CardEditor_DefaultType);
    }
}

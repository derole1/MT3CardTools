using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Forms;
using MT3CardTools.Src.CardTools;

namespace MT3CardTools.Src.Helpers
{
    class CardWindows
    {
        public static Form[] CreateCardWindows(params string[] fileNames)
        {
            var frms = new List<Form>();
            foreach (var fileName in fileNames)
                frms.Add(CreateCardWindow(fileName));
            return frms.ToArray();
        }

        public static Form CreateCardWindow(string fileName)
        {
            if (Properties.Settings.Default.Data1Key.Length < 8 ||
                   Properties.Settings.Default.Data2KeyTable.Length < 8 ||
                   Properties.Settings.Default.MacKeyTable.Length < 8)
            {
                Msg.Error("One or more encryption keys are either empty or invalid!\r\n" +
                    "Please visit \"Tools -> Encryption key extractor\" and set up encryption keys.");
                return null;
            }
            Form frm;
            var version = Card.PeekVersion(fileName);
            if (version == 0)
            {
                Msg.Error("Either the card file doesnt exist, or is in use. Please close any applications with this card open.");
                return null;
            }
            switch (version)
            {
                case Card.EVersion.v337_EXP:
                case Card.EVersion.v337_EXP_LOC_TEST:
                case Card.EVersion.v337_JPN:
                case Card.EVersion.v337_JPN_LOC_TEST:
                    frm = new frmCard_v337(fileName);
                    break;
                case Card.EVersion.v363_EXP:
                case Card.EVersion.v363_JPN:
                case Card.EVersion.v363_EXP_LOC_TEST_A:
                case Card.EVersion.v363_JPN_LOC_TEST_A:
                case Card.EVersion.v363_EXP_LOC_TEST_B:
                case Card.EVersion.v363_JPN_LOC_TEST_B:
                    frm = new frmCard_v363(fileName);
                    break;
                case Card.EVersion.v386_EXP:
                case Card.EVersion.v386_JPN:
                    frm = new frmCard_v386(fileName);
                    break;
                default:
                    Msg.Error($"Unsupported card type {((ushort)version).ToString("X2")}!");
                    return null;
            }
            return frm;
        }
    }
}

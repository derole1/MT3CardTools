using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MT3CardTools.Src.CardTools;
using MT3CardTools.Src.CardTools.Objects;
using MT3CardTools.Src.CardTools.Data;
using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Forms
{
    public partial class frmCardGenerator : Form
    {
        public frmCardGenerator()
        {
            InitializeComponent();
        }
        
        private bool IsError { get; set; }

        private string _oldName;

        private void frmCardGenerator_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Data1Key.Length < 8 ||
                      Properties.Settings.Default.Data2KeyTable.Length < 8 ||
                      Properties.Settings.Default.MacKeyTable.Length < 8)
            {
                Msg.Error("One or more encryption keys are either empty or invalid!\r\n" +
                    "Please visit \"Tools -> Encryption key extractor\" and set up encryption keys.");
                IsError = true;
                return;
            }
            cmbVersion.Items.AddRange(Enum.GetNames(typeof(Card.EVersion)).Where(x => !(x.StartsWith("v307") || x.StartsWith("v322"))).OrderBy(x => x).ToArray());
            cmbVersion.SelectedIndex = 0;
            cmbCar.SelectedIndex = 0;
            numId1.Value = Properties.Settings.Default.CardGenMachineSerial;
            numId2.Value = Properties.Settings.Default.CardGenCount + 1;

            BringToFront();
        }

        private void frmCardGenerator_Shown(object sender, EventArgs e)
        {
            if (IsError)
                Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //if (Properties.Settings.Default.CardEditor_UnsupportedCarsWarning && 
            //    cmbCar.SelectedIndex < 15 && cmbVersion.SelectedItem.ToString().Split('_')[1] != "JPN")
            //{
            //    if (Msg.Warning("The selected car will cause the generated card to not work unless your game is modified to both allow cards with this car and you have restored the required files!\r\n\r\n" +
            //        "This card likely will not load. Are you sure you want to continue?", MessageBoxButtons.YesNo) == DialogResult.No)
            //        return;
            //}
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save card";
                dlg.Filter = CardFile.GetFileDialogFilter();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //if (cmbVersion.SelectedIndex > 3)
                    //{
                    //    if (Msg.Warning("I can generate you a card for this version, but I currently cannot generate the same Mersenne Twister PRNG padding that the game does, " +
                    //        "and this game version checks each bit to ensure that the padding is correct. As such generated cards will likely fail to load.\n\n" +
                    //        "It is recommended that you modify an existing card for these games.\n\nWould you like to continue?", MessageBoxButtons.YesNo) == DialogResult.No)
                    //        return;
                    //}
                    Card.EVersion version;
                    ICard cardSerializer;
                    switch (cmbVersion.SelectedIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            {
                                switch (cmbVersion.SelectedIndex)
                                {
                                    case 0:
                                        version = Card.EVersion.v337_EXP;
                                        break;
                                    case 2:
                                        version = Card.EVersion.v337_JPN;
                                        break;
                                    case 1:
                                        version = Card.EVersion.v337_EXP_LOC_TEST;
                                        break;
                                    case 3:
                                        version = Card.EVersion.v337_JPN_LOC_TEST;
                                        break;
                                    default:
                                        version = 0;
                                        break;
                                }
                                var serializer = new Card_v337();
                                serializer.Data_1.Hp600IdDevel = true;
                                //serializer.Data_2.Title = 1;
                                //serializer.Data_2.CoolWild = 3;
                                //serializer.Data_2.SmoothRough = 3;
                                serializer.Data_2.Overhaul = (byte)(chkIsPresentOrSpecial.Checked ? 61 : 60);
                                //serializer.Data_2.Volume = 2;
                                this.GetValues(serializer.Data_1);
                                this.GetValues(serializer.Data_2);
                                cardSerializer = serializer;
                            }
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            {
                                if (Properties.Settings.Default.PadKeyTable.Length == 0)
                                {
                                    Msg.Error("You do not have a padding key table set! The game will not load cards with null padding, please set the padding key first in \"Tools -> Encryption key extractor\"!");
                                    return;
                                }
                                switch (cmbVersion.SelectedIndex)
                                {
                                    case 4:
                                        version = Card.EVersion.v363_EXP;
                                        break;
                                    case 7:
                                        version = Card.EVersion.v363_JPN;
                                        break;
                                    case 5:
                                        version = Card.EVersion.v363_EXP_LOC_TEST_A;
                                        break;
                                    case 8:
                                        version = Card.EVersion.v363_JPN_LOC_TEST_A;
                                        break;
                                    case 6:
                                        version = Card.EVersion.v363_EXP_LOC_TEST_B;
                                        break;
                                    case 9:
                                        version = Card.EVersion.v363_JPN_LOC_TEST_B;
                                        break;
                                    default:
                                        version = 0;
                                        break;
                                }
                                var serializer = new Card_v363();
                                serializer.Data_1.RenewalIdDevel = true;
                                //serializer.Data_2.Title = 1;
                                //serializer.Data_2.CoolWild = 3;
                                //serializer.Data_2.SmoothRough = 3;
                                serializer.Data_2.Overhaul = (byte)(chkIsPresentOrSpecial.Checked ? 61 : 60);
                                //serializer.Data_2.Volume = 2;
                                if (chkVerUp.Checked)
                                {
                                    Log.Debug("btnGenerate_Click: Ver Up selected! Attempting to create a version up card");
                                    serializer.Data_2.Overhaul = 62;
                                    serializer.Data_2.OdoCount = 2;
                                }
                                this.GetValues(serializer.Data_1);
                                this.GetValues(serializer.Data_2);
                                cardSerializer = serializer;
                            }
                            break;
                        case 10:
                        case 11:
                            {
                                if (Properties.Settings.Default.PadKeyTable.Length == 0)
                                {
                                    Msg.Error("You do not have a padding key table set! The game will not load cards with null padding, please set the padding key first in \"Tools -> Encryption key extractor\"!");
                                    return;
                                }
                                version = cmbVersion.SelectedIndex == 10 ? Card.EVersion.v386_EXP : Card.EVersion.v386_JPN;
                                var serializer = new Card_v386();
                                serializer.Data_1.RenewalIdDevel = true;
                                //serializer.Data_2.Title = 1;
                                //serializer.Data_2.CoolWild = 3;
                                //serializer.Data_2.SmoothRough = 3;
                                serializer.Data_2.Overhaul = (byte)(chkIsPresentOrSpecial.Checked ? 61 : 60);
                                //serializer.Data_2.Volume = 2;
                                this.GetValues(serializer.Data_1);
                                this.GetValues(serializer.Data_2);
                                cardSerializer = serializer;
                            }
                            break;
                        default:
                            throw new Exception("Invalid card version!");
                    }
                    using (var card = new CardFile(dlg.FileName, version))
                    {
                        //Correctly fill out the padding for v363/v386
                        cardSerializer.GeneratePad(card.BaseCard);
                        card.BaseCard.SetObject(cardSerializer);
                        card.Save();
                        Properties.Settings.Default.CardGenCount += 1;
                        Properties.Settings.Default.Save();
                        numId2.Value = Properties.Settings.Default.CardGenCount + 1;
                        if (Msg.Question("Generated card successfully! Would you like to edit it?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var frm = CardWindows.CreateCardWindow(card.FileName);
                            if (frm == null)
                                return;
                            frm.MdiParent = MdiParent;
                            frm.Show();
                        }
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var oldSelectionStart = txtName.SelectionStart;
            var oldSelectionLength = txtName.SelectionLength;
            txtName.Text = txtName.Text.MakeGameFriendly().ToFullWidth();
            txtName.SelectionStart = oldSelectionStart;
            txtName.SelectionLength = oldSelectionLength;
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCar.Items.Clear();
            switch (cmbVersion.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    cmbCar.Items.AddRange(Cars_v337.CarTable);
                    chkIsPresentOrSpecial.Text = "Present";
                    chkVerUp.Visible = false;
                    chkVerUp.Checked = false;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    cmbCar.Items.AddRange(Cars_v363.CarTable);
                    chkIsPresentOrSpecial.Text = "Special";
                    chkVerUp.Visible = true;
                    break;
                case 10:
                case 11:
                    cmbCar.Items.AddRange(Cars_v386.CarTable);
                    chkIsPresentOrSpecial.Text = "Special";
                    chkVerUp.Visible = false;
                    chkVerUp.Checked = false;
                    break;
                default:
                    throw new Exception("Invalid version selected!");
            }
        }

        private void chkVerUp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerUp.Checked)
            {
                txtName.Enabled = false;

                numId1.Value = 999999;
                numId2.Value = 999999;
                chkIdDevel.Checked = false;
                _oldName = txtName.Text;
                txtName.Text = "3DX";
            }
            else
            {
                numId1.Value = Properties.Settings.Default.CardGenMachineSerial;
                numId2.Value = Properties.Settings.Default.CardGenCount + 1;
                chkIdDevel.Checked = false;
                txtName.Text = _oldName;

                txtName.Enabled = true;
            }
        }
    }
}

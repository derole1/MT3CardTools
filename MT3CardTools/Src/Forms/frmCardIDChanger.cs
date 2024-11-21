using MT3CardTools.Src.CardTools.Objects;
using MT3CardTools.Src.CardTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Helpers;
using static MT3CardTools.Src.CardTools.Card;

namespace MT3CardTools.Src.Forms
{
    public partial class frmCardIDChanger : Form
    {
        public CardFile BaseCard { get; }
        public Card_v337 v337Card { get; protected set; }
        public Card_v363 v363Card { get; protected set; }
        public Card_v386 v386Card { get; protected set; }

        public frmCardIDChanger(string fileName)
        {
            BaseCard = new CardFile(fileName);
            InitializeComponent();
        }

        private void frmCardIDChanger_Load(object sender, EventArgs e)
        {
            switch (BaseCard.BaseCard.Version)
            {
                case Card.EVersion.v337_EXP:
                case Card.EVersion.v337_JPN:
                case Card.EVersion.v337_EXP_LOC_TEST:
                case Card.EVersion.v337_JPN_LOC_TEST:
                    v337Card = BaseCard.BaseCard.GetObject<Card_v337>();
                    txtName.Text = v337Card.Data_1.Name;
                    numPrevId1.Value = v337Card.Data_1.Id1;
                    numPrevId2.Value = v337Card.Data_1.Id2;
                    break;
                case Card.EVersion.v363_EXP:
                case Card.EVersion.v363_JPN:
                case Card.EVersion.v363_EXP_LOC_TEST_A:
                case Card.EVersion.v363_JPN_LOC_TEST_A:
                case Card.EVersion.v363_EXP_LOC_TEST_B:
                case Card.EVersion.v363_JPN_LOC_TEST_B:
                    v363Card = BaseCard.BaseCard.GetObject<Card_v363>();
                    txtName.Text = v363Card.Data_1.Name;
                    numPrevId1.Value = v363Card.Data_1.Id1;
                    numPrevId2.Value = v363Card.Data_1.Id2;
                    break;
                case Card.EVersion.v386_EXP:
                case Card.EVersion.v386_JPN:
                    v386Card = BaseCard.BaseCard.GetObject<Card_v386>();
                    txtName.Text = v386Card.Data_1.Name;
                    numPrevId1.Value = v386Card.Data_1.Id1;
                    numPrevId2.Value = v386Card.Data_1.Id2;
                    break;
                default:
                    throw new Exception($"Unknown card version! Got:{BaseCard.BaseCard.Version}");
            }
            txtVersion.Text = BaseCard.BaseCard.Version.ToString();
            numNewId1.Value = Properties.Settings.Default.CardGenMachineSerial;
            numNewId2.Value = Properties.Settings.Default.CardGenCount + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnChangeIDSave_Click(object sender, EventArgs e)
        {
            if (Msg.Warning("!!! IMPORTANT !!!\r\n" +
                "\r\n" +
                "Are you SURE you want to change your card ID?\r\n" +
                "Please note when changing your card ID the following will happen:\r\n" +
                "\r\n" +
                "- Your card will appear to all machines as a \"foreign\" card. This means any ghosts generated on the machine you play on will no longer be linked to this card\r\n" +
                "- Any outstanding revenge ghosts will be lost and will not appear when carding in\r\n" +
                "- You will no longer see the \"Your Ghost\" marker on any ghosts recorded by this card\r\n" +
                "\r\n" +
                "If you are happy with this, click yes, if not, please click no and cancel the ID change process", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Title = "Save card";
                    dlg.Filter = CardFile.GetFileDialogFilter();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        switch (BaseCard.BaseCard.Version)
                        {
                            case Card.EVersion.v337_EXP:
                            case Card.EVersion.v337_JPN:
                            case Card.EVersion.v337_EXP_LOC_TEST:
                            case Card.EVersion.v337_JPN_LOC_TEST:
                                v337Card.Data_1.Id1 = (uint)numNewId1.Value;
                                v337Card.Data_1.Id2 = (uint)numNewId2.Value;
                                using (var card = new CardFile(dlg.FileName, BaseCard.BaseCard.Version))
                                {
                                    card.BaseCard.SetObject(v337Card);
                                    card.Save();
                                }
                                break;
                            case Card.EVersion.v363_EXP:
                            case Card.EVersion.v363_JPN:
                            case Card.EVersion.v363_EXP_LOC_TEST_A:
                            case Card.EVersion.v363_JPN_LOC_TEST_A:
                            case Card.EVersion.v363_EXP_LOC_TEST_B:
                            case Card.EVersion.v363_JPN_LOC_TEST_B:
                                v363Card.Data_1.Id1 = (uint)numNewId1.Value;
                                v363Card.Data_1.Id2 = (uint)numNewId2.Value;
                                using (var card = new CardFile(dlg.FileName, BaseCard.BaseCard.Version))
                                {
                                    card.BaseCard.SetObject(v363Card);
                                    card.Save();
                                }
                                break;
                            case Card.EVersion.v386_EXP:
                            case Card.EVersion.v386_JPN:
                                v386Card.Data_1.Id1 = (uint)numNewId1.Value;
                                v386Card.Data_1.Id2 = (uint)numNewId2.Value;
                                using (var card = new CardFile(dlg.FileName, BaseCard.BaseCard.Version))
                                {
                                    card.BaseCard.SetObject(v386Card);
                                    card.Save();
                                }
                                break;
                            default:
                                throw new Exception($"Unknown card version! Got:{BaseCard.BaseCard.Version}");
                        }
                        Properties.Settings.Default.CardGenCount += 1;
                        Properties.Settings.Default.Save();
                        numNewId2.Value = Properties.Settings.Default.CardGenCount + 1;
                        Msg.Info("Generated card with new ID successfully!");
                    }
                }
            }
        }
    }
}

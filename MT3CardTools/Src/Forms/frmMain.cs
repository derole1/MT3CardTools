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
using System.Runtime.InteropServices;
using System.Configuration;

using MT3CardTools.Src.Interface;

using MT3CardTools.Src.CardTools;
using MT3CardTools.Src.CardTools.Objects;
using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Helpers.Nam;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private static Image Background { get; set; }
        private static MdiClient MdiClient { get; set; }

        protected override void WndProc(ref Message m)
        {
            MdiClient.HideScrollBars();
            base.WndProc(ref m);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.OptionFileVersion != Application.ProductVersion)
            {
                Log.Info($"frmMain_Load: Upgrading option file! {Properties.Settings.Default.OptionFileVersion} -> {Application.ProductVersion}");
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateDate = DateTime.UtcNow;
                Properties.Settings.Default.OptionFileVersion = Application.ProductVersion;
                Properties.Settings.Default.Save();
            }

            MdiClient = this.GetMdiClient();
            MdiClient.SetBevel(false);
            //The below is a super hacky way to get our bevel disable to work
            //This causes the form to redraw and pick up the bevel change
            //Want to find a cleaner way to do this, this will do for now
            MdiClient.Size += new Size(1, 0);
            MdiClient.Size += new Size(-1, 0);

            MdiClient.DragEnter += MdiClient_DragEnter;
            MdiClient.DragDrop += MdiClient_DragDrop;
            MdiClient.AllowDrop = true;

            MdiClient.ContextMenuStrip = ctxRightClickMenu;
            RefreshEditorBackground(true);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.CardReader_BaudRate)
            {
                case 9600:
                    radBaud9600.Checked = true;
                    break;
                case 19200:
                    radBaud19200.Checked = true;
                    break;
                case 38400:
                    radBaud38400.Checked = true;
                    break;
            }
            switch (Properties.Settings.Default.CardReader_Parity)
            {
                case (int)System.IO.Ports.Parity.None:
                    radParityNone.Checked = true;
                    break;
                case (int)System.IO.Ports.Parity.Even:
                    radParityEven.Checked = true;
                    break;
            }
            chkUsePipes.Checked = Properties.Settings.Default.CardReader_UsePipes;
            chkVerifyAfterWriting.Checked = Properties.Settings.Default.CardReader_VerifyWrite;

            switch (Properties.Settings.Default.CardEditor_DefaultType)
            {
                case 0:
                    radTrackSplit.Checked = true;
                    break;
                case 1:
                    radSingle.Checked = true;
                    break;
            }
            chkAddAllFiles.Checked = Properties.Settings.Default.CardEditor_AllowAllFiles;
            chkHideUnsupportedCarsWarning.Checked = Properties.Settings.Default.CardEditor_HideUnsupportedCarsWarning;

            if (Properties.Settings.Default.CardGenMachineSerial == 0)
            {
                Properties.Settings.Default.CardGenMachineSerial = NamSerial.GenerateMachineId();
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.FirstRun)
            {
                Properties.Settings.Default.CreationDate = DateTime.UtcNow;
                Properties.Settings.Default.Save();
                var frm = new frmWelcome();
                frm.MdiParent = this;
                frm.Show();
            }
            if (Properties.Settings.Default.Data1Key.Length < 8 ||
                Properties.Settings.Default.Data2KeyTable.Length < 8 ||
                Properties.Settings.Default.MacKeyTable.Length < 8)
            {
                Msg.Warning("One or more encryption keys are either empty or invalid!\r\n" +
                    "Please visit \"Tools -> Encryption key extractor\" and set up encryption keys.");
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            //Msg.Info(
            //    $"{Application.ProductName} by derole\n" +
            //    $"Version {Application.ProductVersion}\n\n" +
            //    $"Special thanks:\n" +
            //    $"PockyWitch - Signature RE\n\n" +
            //    $"NOT FOR RELEASE!"
            //    );
            var frm = new frmAbout();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e) => Close();

        private void btnOpenCard_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Choose card file";
                dlg.Filter = CardFile.GetFileDialogFilter();
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in dlg.FileNames)
                    {
                        var frm = CardWindows.CreateCardWindow(fileName);
                        if (frm == null)
                            continue;
                        frm.MdiParent = this;
                        frm.Show();
                    }
                }
            }

            //var rawData = System.IO.File.ReadAllBytes("card01.bin");
            //var card = new Card(rawData);
            //card.Read();

            //if (card.Version != Card.EVersion.v337)
            //    throw new Exception("Card type is not v337!");

            //var cardData = card.GetObject<Card_v337>();

            //var name = cardData.Data_1.Name;
            ////cardData.Data_1.Name = "　ＧＡＯ　";

            //card.SetObject(cardData);

            //card.Write();
            //rawData = card.RawData;

            //File.WriteAllBytes("debug_card.bin", card.RawData);

            //throw new NotImplementedException("Unimplemented btnOpenCard!");
        }

        private void btnEncryptionKeyExtractor_Click(object sender, EventArgs e)
        {
            var frm = new frmKeyExtractor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void MdiClient_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void MdiClient_DragDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var fileName in fileList)
            {
                var frm = CardWindows.CreateCardWindow(fileName);
                if (frm == null)
                    continue;
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = MdiClient.PointToClient(Cursor.Position);
                frm.Show();
            }
        }

        private void btnCardGenerator_Click(object sender, EventArgs e)
        {
            var frm = new frmCardGenerator();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnCardReaderInterface_Click(object sender, EventArgs e)
        {
            var frm = new frmCardReaderInterface();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnCardFileConverter_Click(object sender, EventArgs e)
        {
            using (var srcDlg = new OpenFileDialog())
            using (var dstDlg = new SaveFileDialog())
            {
                srcDlg.Title = "Load source card file";
                srcDlg.Filter = CardFile.GetFileDialogFilter();
                srcDlg.Multiselect = false;
                dstDlg.Title = "Save dest card file";
                dstDlg.Filter = CardFile.GetFileDialogFilter();
                if (srcDlg.ShowDialog() == DialogResult.OK &&
                    dstDlg.ShowDialog() == DialogResult.OK)
                {
                    var version = Card.PeekVersion(srcDlg.FileName);
                    if (version == 0)
                    {
                        Msg.Error("Either the card file doesnt exist, or is in use. Please close any applications with this card open.");
                        return;
                    }
                    if (Enum.IsDefined(typeof(Card.EVersion), version) &&
                        version != Card.EVersion.v307 && version != Card.EVersion.v322_EXP && 
                        version != Card.EVersion.v322_JPN)
                    {
                        using (var src = new CardFile(srcDlg.FileName))
                        {
                            if (src.BaseCard.HasCorrectMac && src.BaseCard.HasCorrectSum)
                            {
                                using (var dst = new CardFile(dstDlg.FileName, version))
                                {
                                    switch (src.BaseCard.Version)
                                    {
                                        case Card.EVersion.v337_EXP:
                                        case Card.EVersion.v337_EXP_LOC_TEST:
                                        case Card.EVersion.v337_JPN:
                                        case Card.EVersion.v337_JPN_LOC_TEST:
                                            dst.BaseCard.SetObject(src.BaseCard.GetObject<Card_v337>());
                                            break;
                                        case Card.EVersion.v363_EXP:
                                        case Card.EVersion.v363_JPN:
                                        case Card.EVersion.v363_EXP_LOC_TEST_A:
                                        case Card.EVersion.v363_JPN_LOC_TEST_A:
                                        case Card.EVersion.v363_EXP_LOC_TEST_B:
                                        case Card.EVersion.v363_JPN_LOC_TEST_B:
                                            dst.BaseCard.SetObject(src.BaseCard.GetObject<Card_v363>());
                                            break;
                                        case Card.EVersion.v386_EXP:
                                        case Card.EVersion.v386_JPN:
                                            dst.BaseCard.SetObject(src.BaseCard.GetObject<Card_v386>());
                                            break;
                                        default:
                                            Msg.Error($"Unsupported card version {((ushort)src.BaseCard.Version).ToString("X2")}!");
                                            return;
                                    }
                                    dst.Save();
                                    Msg.Info($"Converted card successfully!");
                                    return;
                                }
                            }
                        }
                        Msg.Error("MAC and/or Sum of source card is incorrect, conversion has been aborted!\r\nPlease load the card and verify data is valid, then save the card.");
                        return;
                    }
                    Msg.Error($"Source card version {version} is not valid! Aborting");
                }
            }
        }

        private void btnEditorBackgroundChange_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Select background";
                dlg.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.EditorBackground = Convert.ToBase64String(File.ReadAllBytes(dlg.FileName));
                    Properties.Settings.Default.Save();
                    RefreshEditorBackground(true);
                }
            }
        }

        private void btnEditorBackgroundRemove_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EditorBackground = "";
            Properties.Settings.Default.Save();
            RefreshEditorBackground(true);
        }

        private void RefreshEditorBackground(bool reload)
        {
            if (reload)
            {
                var data = Convert.FromBase64String(Properties.Settings.Default.EditorBackground);
                if (data.Length == 0)
                    Background = null;
                else
                {
                    using (var ms = new MemoryStream(data))
                    {
                        Background = Image.FromStream(ms);
                    }
                }
                data = null;
            }
            if (MdiClient == null)
            {
                Log.Error("RefreshEditorBackground: MdiClient is null? This shouldnt happen...");
                return;
            }
            if (MdiClient.BackgroundImage != null)
                MdiClient.BackgroundImage.Dispose();    //Prevent memory leaks
            if (Background == null)
                MdiClient.BackgroundImage = null;
            else
                MdiClient.SetCoverImage(Background);
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            RefreshEditorBackground(false);
        }

        private void btnOpenWelcomeMessage_Click(object sender, EventArgs e)
        {
            var frm = new frmWelcome();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chkUsePipes_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CardReader_UsePipes = chkUsePipes.Checked;
            Properties.Settings.Default.Save();
        }

        private void radBaud9600_Click(object sender, EventArgs e)
        {
            radBaud19200.Checked = false;
            radBaud38400.Checked = false;
            if (radBaud9600.Checked)
            {
                Properties.Settings.Default.CardReader_BaudRate = 9200;
                Properties.Settings.Default.Save();
            }
        }

        private void radBaud19200_Click(object sender, EventArgs e)
        {
            radBaud9600.Checked = false;
            radBaud38400.Checked = false;
            if (radBaud19200.Checked)
            {
                Properties.Settings.Default.CardReader_BaudRate = 19200;
                Properties.Settings.Default.Save();
            }
        }

        private void radBaud38400_Click(object sender, EventArgs e)
        {
            radBaud9600.Checked = false;
            radBaud19200.Checked = false;
            if (radBaud38400.Checked)
            {
                Properties.Settings.Default.CardReader_BaudRate = 38400;
                Properties.Settings.Default.Save();
            }
        }

        private void radParityNone_Click(object sender, EventArgs e)
        {
            radParityEven.Checked = false;
            if (radParityNone.Checked)
            {
                Properties.Settings.Default.CardReader_Parity = (int)System.IO.Ports.Parity.None;
                Properties.Settings.Default.Save();
            }
        }

        private void radParityEven_Click(object sender, EventArgs e)
        {
            radParityNone.Checked = false;
            if (radParityEven.Checked)
            {
                Properties.Settings.Default.CardReader_Parity = (int)System.IO.Ports.Parity.Even;
                Properties.Settings.Default.Save();
            }
        }

        private void mnuFile_DropDownOpening(object sender, EventArgs e)
        {
            mnuRecentCards.DropDownItems.Clear();
            if (Properties.Settings.Default.RecentCards != null)
            {
                int i = 0;
                foreach (var file in Properties.Settings.Default.RecentCards)
                {
                    var mnuItem = new ToolStripMenuItem
                    {
                        Name = $"btnRecentCard{i}",
                        Text = $"{file}"
                    };
                    mnuItem.Click += MnuItem_Click;
                    mnuRecentCards.DropDownItems.Add(mnuItem);
                    i++;
                }
            }
        }

        private void MnuItem_Click(object sender, EventArgs e)
        {
            var frm = CardWindows.CreateCardWindow(((ToolStripMenuItem)sender).Text);
            if (frm == null)
                return;
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnResetAllOptions_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Are you SURE you want to reset options? All custom options will be lost.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var serial = Properties.Settings.Default.CardGenMachineSerial;
                var count = Properties.Settings.Default.CardGenCount;
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.CardGenMachineSerial = serial;
                Properties.Settings.Default.CardGenCount = count;
                Properties.Settings.Default.Save();
                RefreshEditorBackground(true);
            }
        }

        private void chkVerifyAfterWriting_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CardReader_VerifyWrite = chkVerifyAfterWriting.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnCloseAllWindows_Click(object sender, EventArgs e)
        {
            foreach (var window in MdiClient.MdiChildren)
                window.Close();
        }

        private void ctxRightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            foreach (var window in MdiClient.MdiChildren)
            {
                if (window.ClientRectangle.IntersectsWith(new Rectangle(window.PointToClient(Cursor.Position), new Size(1, 1))))
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void radTrackSplit_Click(object sender, EventArgs e)
        {
            radSingle.Checked = false;
            if (radTrackSplit.Checked)
            {
                Properties.Settings.Default.CardEditor_DefaultType = 0;
                Properties.Settings.Default.Save();
            }
        }

        private void radSingle_Click(object sender, EventArgs e)
        {
            radTrackSplit.Checked = false;
            if (radSingle.Checked)
            {
                Properties.Settings.Default.CardEditor_DefaultType = 1;
                Properties.Settings.Default.Save();
            }
        }

        private void chkAddAllFiles_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CardEditor_AllowAllFiles = chkAddAllFiles.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkUnsupportedCarsWarning_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CardEditor_HideUnsupportedCarsWarning = chkHideUnsupportedCarsWarning.Checked;
            Properties.Settings.Default.Save();
        }

        private void cardIDChangerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var srcDlg = new OpenFileDialog())
            {
                srcDlg.Title = "Load source card file";
                srcDlg.Filter = CardFile.GetFileDialogFilter();
                srcDlg.Multiselect = false;
                if (srcDlg.ShowDialog() == DialogResult.OK)
                {
                    var version = Card.PeekVersion(srcDlg.FileName);
                    if (version == 0)
                    {
                        Msg.Error("Either the card file doesnt exist, or is in use. Please close any applications with this card open.");
                        return;
                    }
                    if (Enum.IsDefined(typeof(Card.EVersion), version) &&
                        version != Card.EVersion.v307 && version != Card.EVersion.v322_EXP &&
                        version != Card.EVersion.v322_JPN)
                    {
                        var frm = new frmCardIDChanger(srcDlg.FileName);
                        frm.MdiParent = this;
                        frm.Show();
                        return;
                    }
                    Msg.Error($"Source card version {version} is not valid! Aborting");
                }
            }
        }
    }
}

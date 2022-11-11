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
using System.IO.Ports;

using MT3CardTools.Src.Interface;
using MT3CardTools.Src.CardTools;
using MT3CardTools.Src.CardTools.ReaderNew;
using MT3CardTools.Src.CardTools.ReaderNew.Models;
using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.Forms
{
    public partial class frmCardReaderInterface : Form
    {
        public enum EWriteCardType
        {
            FromDispenser,
            FromInsert
        }

        public frmCardReaderInterface()
        {
            InitializeComponent();
        }

        private ReaderConnection Reader { get; set; }
        private frmCardReaderInterfaceWait WaitMessage { get; set; }

        private GetVersion.Response ReaderVersion { get; set; }

        private void frmCardReaderInterface_Load(object sender, EventArgs e)
        {
            cmbComPort.Items.AddRange(Properties.Settings.Default.CardReader_UsePipes
                ? Directory.GetFiles(@"\\.\pipe\").Select(s => s.Replace(@"\\.\pipe\", "")).ToArray() : SerialPort.GetPortNames());
            if (cmbComPort.Items.Contains(Properties.Settings.Default.CardReader_LastPort))
                cmbComPort.SelectedItem = Properties.Settings.Default.CardReader_LastPort;
            else
                cmbComPort.SelectedIndex = 0;

            WaitMessage = new frmCardReaderInterfaceWait();
            WaitMessage.ParentWindow = this;
            WaitMessage.MdiParent = ParentForm;
            BringToFront();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            var portName = cmbComPort.Text;
            btnConnect.Enabled = false;
            if (Reader == null)
            {
                Reader = new ReaderConnection(portName, Properties.Settings.Default.CardReader_UsePipes ? SerialConnection.EPortType.Pipe : SerialConnection.EPortType.COM
                    , Properties.Settings.Default.CardReader_BaudRate, (Parity)Properties.Settings.Default.CardReader_Parity);
                await Reader.Open();
            START:
                if (Reader.IsOpen)
                {
                    WaitMessage.Show("WAIT! Connecting...");
                    var result = await Reader.GetStatus();
                    WaitMessage.Hide();
                    if (result != null &&
                        result.P == ReaderConstants.EP.NoError &&
                        result.S == ReaderConstants.ES.JobEnd)
                    {
                        WaitMessage.Show("WAIT! Connecting...");
                        ReaderVersion = await Reader.GetVersion();
                        await CheckCardEject(result);
                        WaitMessage.Show("WAIT! Initalizing...");
                        var initResult = await Reader.Init(false, false);
                        WaitMessage.Hide();
                        if (initResult != null)
                        {
                            lblStatus.Text = $"{ReaderVersion.Version}\nR:{result.R},P:{result.P},S:{result.S}";
                            Text = $"Reader - Connected to {portName}({ReaderVersion})";
                            btnConnect.Text = "Disconnect";
                            btnReadCard.Enabled = true;
                            btnWriteCard.Enabled = true;
                            cmbComPort.Enabled = false;
                            btnConnect.Enabled = true;
                            Properties.Settings.Default.CardReader_LastPort = portName;
                            Properties.Settings.Default.Save();
                            tmrStatus.Start();
                            return;
                        }
                    }
                    else if (result == null || result.S == ReaderConstants.ES.ExecutingCommand || result.IsError)
                    {
                        WaitMessage.Show("WAIT! Resetting...");
                        await Reader.Cancel();
                        WaitMessage.Hide();
                        goto START;
                    }
                    Msg.Error($"Card reader did not respond or has an error.");
                    btnConnect.Enabled = true;
                    Reader.Dispose();
                    Reader = null;
                    return;
                }
                Msg.Error($"The specified serial port does not exist!");
                Reader.Dispose();
                Reader = null;
            }
            else
            {
                Reader.Dispose();
                Reader = null;
                lblStatus.Text = "Idle";
                Text = "Reader - Disconnected";
                btnConnect.Text = "Connect";
                btnReadCard.Enabled = false;
                btnWriteCard.Enabled = false;
                cmbComPort.Enabled = true;
                tmrStatus.Stop();
            }
            btnConnect.Enabled = true;
        }

        private async void btnReadCard_Click(object sender, EventArgs e)
        {
            WaitMessage.Show("Insert a card into the reader");
            await AwaitCardInsert();
            WaitMessage.Show("WAIT! Reading...");
            var data = await Reader.Read(Read.EMode.Full, false, Read.ETrack.TrackAll);
            WaitMessage.Hide();
            if (data != null &&
                data.R == ReaderConstants.ER.CardOverMag &&
                data.P == ReaderConstants.EP.NoError &&
                data.S == ReaderConstants.ES.JobEnd)
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.Title = "Save card as";
                    dlg.Filter = CardFile.GetFileDialogFilter();
                    var success = false;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(dlg.FileName, data.Data);
                        success = true;
                    }
                    WaitMessage.Show("WAIT! Ejecting...");
                    await AwaitCardEject();
                    WaitMessage.Hide();
                    if (success)
                        Msg.Info("Dumped card successfully!");
                }
                return;
            }
            Msg.Error($"Card reader did not respond.");
        }

        private async void btnWriteCard_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Load source card file";
                dlg.Filter = CardFile.GetFileDialogFilter();
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var src = new CardFile(dlg.FileName))
                    {
                        var writeType = EWriteCardType.FromInsert;
                        WaitMessage.Show("WAIT! Checking reader status...");
                        var readerHasCards = await Reader.GetCard(false);
                        WaitMessage.Hide();
                        if (readerHasCards != null &&
                            readerHasCards.R == ReaderConstants.ER.NoCard &&
                            readerHasCards.P == ReaderConstants.EP.NoError &&
                            (readerHasCards.S == ReaderConstants.ES.JobEnd || readerHasCards.S == ReaderConstants.ES.FullDispenser))
                        {
                            if (Msg.Question("Your dispenser seems to have cards in stock! Do you want me to pull a card from the dispenser and use that?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                WaitMessage.Show("WAIT! Pulling card from dispenser...");
                                var dispenseResult = await Reader.GetCard(true);
                                WaitMessage.Hide();
                                //TODO: Check R, should = ER.CardBetweenThermalDispenser
                                if (dispenseResult.P == ReaderConstants.EP.NoError &&
                                    dispenseResult.S == ReaderConstants.ES.JobEnd)
                                    writeType = EWriteCardType.FromDispenser;
                                else
                                    Msg.Warning("I was unable to pull a card from the dispenser! You will need to insert one");
                            }
                        }
                        if (writeType == EWriteCardType.FromInsert)
                        {
                            WaitMessage.Show("Insert a card into the reader");
                            await AwaitCardInsert();
                        }
                        WaitMessage.Show("WAIT! Writing...");
                        var result = await Reader.Write(Write.EMode.Full, false, Write.ETrack.TrackAll, src.BaseCard.RawData);
                        if (Properties.Settings.Default.CardReader_VerifyWrite)
                        {
                            WaitMessage.Show("WAIT! Reading...");
                            var readResult = await Reader.Read(Read.EMode.Full, false, Read.ETrack.TrackAll);
                            if (readResult == null ||
                                readResult.P != ReaderConstants.EP.NoError ||
                                readResult.S != ReaderConstants.ES.JobEnd ||
                                readResult.Data.Hash().ToHex() != src.BaseCard.RawData.Hash().ToHex())
                            {
                                Msg.Error("Verifying card failed! There may be an issue with the card, or the reader isnt responding");
                                WaitMessage.Show("WAIT! Ejecting...");
                                await AwaitCardEject();
                                WaitMessage.Hide();
                                return;
                            }
                        }
                        WaitMessage.Show("WAIT! Ejecting...");
                        await AwaitCardEject();
                        WaitMessage.Hide();
                        if (result != null &&
                            result.P == ReaderConstants.EP.NoError &&
                            result.S == ReaderConstants.ES.JobEnd)
                        {
                            Msg.Info("Wrote card successfully!");
                            return;
                        }
                        Msg.Error($"Card reader did not respond.");
                    }
                }
            }
        }

        private async Task AwaitCardInsert()
        {
            IResponse awaitCard = await Reader.Read(Read.EMode.None, false, Read.ETrack.TrackAll);
            while (true)
            {
                if (awaitCard != null &&
                    awaitCard.P == ReaderConstants.EP.NoError &&
                    awaitCard.S == ReaderConstants.ES.JobEnd)
                {
                    if (awaitCard.R == ReaderConstants.ER.CardOverMag)
                        return;
                }
                await Task.Delay(10);
                awaitCard = await Reader.GenericEnq<Read.Response>();
            }
        }

        private async Task AwaitCardEject()
        {
            IResponse awaitCard = await Reader.Eject();
            while (true)
            {
                if (awaitCard != null &&
                    awaitCard.P == ReaderConstants.EP.NoError &&
                    awaitCard.S == ReaderConstants.ES.JobEnd)
                {
                    if (awaitCard.R == ReaderConstants.ER.CardInSlot)
                        WaitMessage.Show("Ejected! Take card");
                    else if (awaitCard.R == ReaderConstants.ER.NoCard)
                        return;
                }
                await Task.Delay(10);
                awaitCard = await Reader.GenericEnq<Eject.Response>();
            }
        }

        private async Task CheckCardEject(IResponse result)
        {
            if (result.R == ReaderConstants.ER.CardOverMag ||
                result.R == ReaderConstants.ER.CardOverThermal ||
                result.R == ReaderConstants.ER.CardBetweenThermalDispenser ||
                result.R == ReaderConstants.ER.CardInSlot)
            {
                WaitMessage.Show("WAIT! Ejecting...");
                await AwaitCardEject();
            }
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (Reader.LastResponse != null)
                lblStatus.Text = $"{ReaderVersion.Version}\nR:{Reader.LastResponse.R},P:{Reader.LastResponse.P},S:{Reader.LastResponse.S}";
        }

        private void frmCardReaderInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Reader != null)
                Reader.Dispose();
        }

        private async void btnTstPrintImage_Click(object sender, EventArgs e)
        {
            WaitMessage.Show("WAIT! Printing...");
            var result = await Reader.PrintImage(0, 0, 128, 128, new byte[1024]);
            WaitMessage.Hide();
            if (result != null &&
                result.R == ReaderConstants.ER.NoCard &&
                result.P == ReaderConstants.EP.NoError &&
                result.S == ReaderConstants.ES.JobEnd)
            {
                Msg.Info("Print completed!");
            }
        }

        private void btnTestConvertImage_Click(object sender, EventArgs e)
        {
            var img = (Bitmap)Image.FromFile("test.png");
            img.To1Bpp().Save("testB.png");
        }

        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Load image";
                dlg.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var img = ((Bitmap)Image.FromFile(dlg.FileName)).To1Bpp())
                    {
                        
                    }
                }
            }
        }
    }
}

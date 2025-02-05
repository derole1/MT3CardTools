﻿using System;
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

namespace MT3CardTools.Src.Forms
{
    public partial class frmCard_v363 : Form
    {
        const int TENFIGHT_FONT_SIZE = 12;
        const int MAX_STEP = 24;

        public CardFile BaseCard { get; }
        public Card_v363 v363Card { get; protected set; }

        private Form TenFightForm { get; set; }
        private Form TAForm { get; set; }

        public frmCard_v363(string fileName)
        {
            BaseCard = new CardFile(fileName);
            InitializeComponent();
        }

        private void frmCard_v363_Load(object sender, EventArgs e)
        {
            if (BaseCard.BaseCard.Version != Card.EVersion.v363_EXP &&
                BaseCard.BaseCard.Version != Card.EVersion.v363_JPN &&
                BaseCard.BaseCard.Version != Card.EVersion.v363_EXP_LOC_TEST_A &&
                BaseCard.BaseCard.Version != Card.EVersion.v363_JPN_LOC_TEST_A &&
                BaseCard.BaseCard.Version != Card.EVersion.v363_EXP_LOC_TEST_B &&
                BaseCard.BaseCard.Version != Card.EVersion.v363_JPN_LOC_TEST_B)
                throw new Exception($"Wrong card version! Expected:{Card.EVersion.v363_EXP}/{Card.EVersion.v363_JPN}/{Card.EVersion.v363_EXP_LOC_TEST_A}/{Card.EVersion.v363_JPN_LOC_TEST_A}/" +
                    $"{Card.EVersion.v363_EXP_LOC_TEST_B}/{Card.EVersion.v363_JPN_LOC_TEST_B} Got:{BaseCard.BaseCard.Version}");
            v363Card = BaseCard.BaseCard.GetObject<Card_v363>();
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            cmbCar.Items.AddRange(Cars_v363.CarTable);
            cmdWheel.Items.AddRange(Parts_v363.WheelTable);
            tabData1.FillValues(v363Card.Data_1);
            tabData2.FillValues(v363Card.Data_2);
            UpdateTitleList();
            radV363Exp.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_EXP;
            radV363Jpn.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_JPN;
            radV363ExpLocTestA.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_EXP_LOC_TEST_A;
            radV363JpnLocTestA.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_JPN_LOC_TEST_A;
            radV363ExpLocTestB.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_EXP_LOC_TEST_B;
            radV363JpnLocTestB.Checked = BaseCard.BaseCard.Version == Card.EVersion.v363_JPN_LOC_TEST_B;

            BringToFront();
        }

        private void UpdateTitleList()
        {
            var titles = Titles_v363.GetTitleList(BaseCard.BaseCard.Version);
            var collection = new AutoCompleteStringCollection();
            collection.AddRange(titles);
            ctxTitle.AutoCompleteCustomSource = collection;
            ctxTitle.Text = titles[v363Card.Data_2.Title];
        }

        private void saveCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabData1.GetValues(v363Card.Data_1);
            tabData2.GetValues(v363Card.Data_2);
            BaseCard.BaseCard.SetObject(v363Card);
            BaseCard.Save();
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            Msg.Info("Saved card successfully!");
        }

        private void saveCardAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save card as";
                dlg.Filter = CardFile.GetFileDialogFilter(BaseCard.CardType == CardFile.ECardType.TrackSplit ? 0 : 1);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var tmpCard = new CardFile(BaseCard, dlg.FileName, BaseCard.BaseCard.Version))
                    {
                        tabData1.GetValues(v363Card.Data_1);
                        tabData2.GetValues(v363Card.Data_2);
                        tmpCard.BaseCard.SetObject(v363Card);
                        tmpCard.Save(dlg.FileName);
                        Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
                        Msg.Info("Saved card successfully!");
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

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnTenFightResults_Click(object sender, EventArgs e)
        {
            if (TenFightForm != null && !TenFightForm.IsDisposed)
                return;
            TenFightForm = new Form
            {
                Name = "frmTenFightResults",
                Text = $"{Text} - TenFight results",
                MdiParent = MdiParent,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                ShowIcon = false,
                Padding = new Padding(10, 10, 10, 10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            int x = TenFightForm.Padding.Left;
            int y = TenFightForm.Padding.Top;
            for (int c = 0; c < 10; c++)
            {
                var lblCourse = new Label
                {
                    Name = $"lblCourse{c}",
                    Font = new Font(FontFamily.GenericMonospace, TENFIGHT_FONT_SIZE),
                    Text = Properties.Settings.Default.CardEditor_ShowCourseNames ? $"{Courses.CourseTable[c]}: " : $"{c}: ",
                    Location = new Point(x, y),
                    AutoSize = true
                };
                TenFightForm.Controls.Add(lblCourse);
                x += lblCourse.Width + lblCourse.Margin.Right;
                for (int l = 0; l < 10; l++)
                {
                    var lblLevel = new Label
                    {
                        Name = $"lblLevelResult{c}_{l}",
                        Font = new Font(FontFamily.GenericMonospace, TENFIGHT_FONT_SIZE),
                        Text = v363Card.Data_2.TenResults.Where(o => o.Course == c && o.Level == l).FirstOrDefault().Result.GetTenFightRank(),
                        Location = new Point(x, y),
                        Margin = new Padding(10, 10, 10, 10),
                        AutoSize = true
                    };
                    TenFightForm.Controls.Add(lblLevel);
                    x += lblLevel.Width + lblLevel.Margin.Right;
                }
                y += lblCourse.Height + lblCourse.Margin.Bottom;
                x = TenFightForm.Padding.Left;
            }
            var btnTenFightClearResults = new Button
            {
                Name = "btnTenFightClearResults",
                Text = "Clear results",
                Margin = new Padding(10, 10, 10, 10),
                Size = new Size(TenFightForm.Width - (20 + 20), 30),
                Location = new Point(10, TenFightForm.Bottom - 30 - 20)
            };
            btnTenFightClearResults.Click += BtnTenFightClearResults_Click;
            TenFightForm.Controls.Add(btnTenFightClearResults);
            TenFightForm.Show();
        }

        private void btnTAResults_Click(object sender, EventArgs e)
        {
            if (TAForm != null && !TAForm.IsDisposed)
                return;
            TAForm = new Form
            {
                Name = "frmTAResults",
                Text = $"{Text} - TA results",
                MdiParent = MdiParent,
                ShowIcon = false
            };
            var lstTAResults = new ListView
            {
                Name = "lstTAResults",
                Columns =
                {
                    new ColumnHeader { Name = "clmTAResultCourse", Text = "Course", Width = Properties.Settings.Default.CardEditor_ShowCourseNames ? 150 : 50 },
                    new ColumnHeader { Name = "clmTAResultPower", Text = "Power", Width = 50 },
                    new ColumnHeader { Name = "clmTAResultTime", Text = "Time", Width = 70 }
                },
                FullRowSelect = true,
                GridLines = true,
                View = View.Details,
                Dock = DockStyle.Fill
            };
            int i = 0;
            foreach (var result in v363Card.Data_2.Ta)
            {
                lstTAResults.Items.Add(new ListViewItem(new string[]
                {
                    Properties.Settings.Default.CardEditor_ShowCourseNames ? Courses.CourseTable[i] : i.ToString(),
                    result.Power.ToString(),
                    result.Time.GetTime()
                }));
                i++;
            }
            var btnTAClearResults = new Button
            {
                Name = "btnClearTAResults",
                Text = "Clear records",
                Dock = DockStyle.Bottom
            };
            btnTAClearResults.Click += BtnTAClearResults_Click;
            TAForm.Controls.Add(lstTAResults);
            TAForm.Controls.Add(btnTAClearResults);
            TAForm.Show();
        }

        private void BtnTAClearResults_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Are you SURE you want to clear TA records? This will clear all time attack records from your local card ONLY, not the machine!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < v363Card.Data_2.Ta.Count; i++)
                    v363Card.Data_2.Ta[i] = new Card_v363.Data2.TA();
                ((Form)((Button)sender).Parent).Close();
            }
        }

        private void BtnTenFightClearResults_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Are you SURE you want to clear TenFight results? This will clear all TenFight results!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < v363Card.Data_2.TenResults.Count; i++)
                    v363Card.Data_2.TenResults[i].Result = 0;
                ((Form)((Button)sender).Parent).Close();
            }
        }

        private void frmCard_v363_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TenFightForm != null && !TenFightForm.IsDisposed)
                TenFightForm.Close();
            if (TAForm != null && !TAForm.IsDisposed)
                TAForm.Close();
            BaseCard.Dispose();
        }

        private void radV363_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = true;
            radV363Jpn.Checked = false;
            radV363ExpLocTestA.Checked = false;
            radV363JpnLocTestA.Checked = false;
            radV363ExpLocTestB.Checked = false;
            radV363JpnLocTestB.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v363_EXP;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void radV363Jpn_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = false;
            radV363Jpn.Checked = true;
            radV363ExpLocTestA.Checked = false;
            radV363JpnLocTestA.Checked = false;
            radV363ExpLocTestB.Checked = false;
            radV363JpnLocTestB.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v363_JPN;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void radV363LocTestA_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = false;
            radV363Jpn.Checked = false;
            radV363ExpLocTestA.Checked = true;
            radV363JpnLocTestA.Checked = false;
            radV363ExpLocTestB.Checked = false;
            radV363JpnLocTestB.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v363_EXP_LOC_TEST_A;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
            Msg.Warning("The game likely used a slightly different structure during location testing. As I dont have access to a location test card, " +
                "I cannot confirm or deny this. Proceed with caution!");
        }

        private void radV363JpnLocTestA_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = false;
            radV363Jpn.Checked = false;
            radV363ExpLocTestA.Checked = false;
            radV363JpnLocTestA.Checked = true;
            radV363ExpLocTestB.Checked = false;
            radV363JpnLocTestB.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v363_JPN_LOC_TEST_A;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
            Msg.Warning("The game likely used a slightly different structure during location testing. As I dont have access to a location test card, " +
                "I cannot confirm or deny this. Proceed with caution!");
        }

        private void radV363LocTestB_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = false;
            radV363Jpn.Checked = false;
            radV363ExpLocTestA.Checked = false;
            radV363JpnLocTestA.Checked = false;
            radV363ExpLocTestB.Checked = true;
            radV363JpnLocTestB.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v363_EXP_LOC_TEST_B;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
            Msg.Warning("The game likely used a slightly different structure during location testing. As I dont have access to a location test card, " +
                "I cannot confirm or deny this. Proceed with caution!");
        }

        private void radV363JpnLocTestB_Click(object sender, EventArgs e)
        {
            radV363Exp.Checked = false;
            radV363Jpn.Checked = false;
            radV363ExpLocTestA.Checked = false;
            radV363JpnLocTestA.Checked = false;
            radV363ExpLocTestB.Checked = false;
            radV363JpnLocTestB.Checked = true;
            BaseCard.BaseCard.Version = Card.EVersion.v363_JPN_LOC_TEST_B;
            Text = $"{v363Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
            Msg.Warning("The game likely used a slightly different structure during location testing. As I dont have access to a location test card, " +
                "I cannot confirm or deny this. Proceed with caution!");
        }

        private void ctxTitle_TextChanged(object sender, EventArgs e)
        {
            var titleId = Titles_v363.GetTitleIndex(BaseCard.BaseCard.Version, ctxTitle.Text);
            if (titleId != -1)
            {
                ctxTitle.ForeColor = SystemColors.ControlText;
                v363Card.Data_2.Title = (ushort)titleId;
            }
            else
                ctxTitle.ForeColor = Color.Red;
        }

        private void numTunePower_ValueChanged(object sender, EventArgs e)
        {
            if (numTunePower.Value > MAX_STEP)
                numTunePower.Value = MAX_STEP;
            var max = MAX_STEP - (numTunePower.Value - 10);
            if (numTunePower.Value > 10 && numTuneHandling.Value > max)
                numTuneHandling.Value = max;
            else if ((numTunePower.Value < 10 && numTuneHandling.Value > 10) ||
                (numTuneHandling.Value < 10 && numTunePower.Value > 10))
                numTuneHandling.Value = 10;
        }

        private void numTuneHandling_ValueChanged(object sender, EventArgs e)
        {
            if (numTuneHandling.Value > MAX_STEP)
                numTuneHandling.Value = MAX_STEP;
            var max = MAX_STEP - (numTuneHandling.Value - 10);
            if (numTuneHandling.Value > 10 && numTunePower.Value > max)
                numTunePower.Value = max;
            else if ((numTunePower.Value < 10 && numTuneHandling.Value > 10) ||
                (numTuneHandling.Value < 10 && numTunePower.Value > 10))
                numTunePower.Value = 10;
        }
    }
}

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

namespace MT3CardTools.Src.Forms
{
    public partial class frmCard_v337 : Form
    {
        const int TENFIGHT_FONT_SIZE = 12;
        const int MAX_STEP = 23;

        public CardFile BaseCard { get; }
        public Card_v337 v337Card { get; protected set; }

        private Form TenFightForm { get; set; }
        private Form TAForm { get; set; }

        public frmCard_v337(string fileName)
        {
            BaseCard = new CardFile(fileName);
            InitializeComponent();
        }

        private void frmCard_v337_Load(object sender, EventArgs e)
        {
            if (BaseCard.BaseCard.Version != Card.EVersion.v337_EXP &&
                BaseCard.BaseCard.Version != Card.EVersion.v337_JPN &&
                BaseCard.BaseCard.Version != Card.EVersion.v337_EXP_LOC_TEST &&
                BaseCard.BaseCard.Version != Card.EVersion.v337_JPN_LOC_TEST)
                throw new Exception($"Wrong card version! Expected:{Card.EVersion.v337_EXP}/{Card.EVersion.v337_JPN}/{Card.EVersion.v337_EXP_LOC_TEST}/{Card.EVersion.v337_JPN_LOC_TEST} " +
                    $"Got:{BaseCard.BaseCard.Version}");
            v337Card = BaseCard.BaseCard.GetObject<Card_v337>();
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            cmbCar.Items.AddRange(Cars_v337.CarTable);
            cmdWheel.Items.AddRange(Parts_v337.WheelTable);
            tabData1.FillValues(v337Card.Data_1);
            tabData2.FillValues(v337Card.Data_2);
            UpdateTitleList();
            radV337Exp.Checked = BaseCard.BaseCard.Version == Card.EVersion.v337_EXP;
            radV337ExpSP.Checked = BaseCard.BaseCard.Version == Card.EVersion.v337_EXP_LOC_TEST;
            radV337Jpn.Checked = BaseCard.BaseCard.Version == Card.EVersion.v337_JPN;
            radV337JpnSP.Checked = BaseCard.BaseCard.Version == Card.EVersion.v337_JPN_LOC_TEST;

            BringToFront();
        }

        private void UpdateTitleList()
        {
            var titles = Titles_v337.GetTitleList(BaseCard.BaseCard.Version);
            var collection = new AutoCompleteStringCollection();
            collection.AddRange(titles);
            ctxTitle.AutoCompleteCustomSource = collection;
            ctxTitle.Text = titles[v337Card.Data_2.Title];
        }

        private void saveCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabData1.GetValues(v337Card.Data_1);
            tabData2.GetValues(v337Card.Data_2);
            BaseCard.BaseCard.SetObject(v337Card);
            BaseCard.Save();
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
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
                        tabData1.GetValues(v337Card.Data_1);
                        tabData2.GetValues(v337Card.Data_2);
                        tmpCard.BaseCard.SetObject(v337Card);
                        tmpCard.Save(dlg.FileName);
                        Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
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
            for (int c=0; c<10; c++)
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
                        Text = v337Card.Data_2.TenResults.Where(o => o.Course == c && o.Level == l).FirstOrDefault().Result.GetTenFightRank(),
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
                    new ColumnHeader { Name = "clmTAResultCar", Text = "Car", Width = 40 },
                    new ColumnHeader { Name = "clmTAResultPower", Text = "Power", Width = 50 },
                    new ColumnHeader { Name = "clmTAResultTime", Text = "Time", Width = 70 }
                },
                FullRowSelect = true,
                GridLines = true,
                View = View.Details,
                Dock = DockStyle.Fill
            };
            int i = 0;
            foreach (var result in v337Card.Data_2.Ta)
            {
                lstTAResults.Items.Add(new ListViewItem(new string[]
                {
                    Properties.Settings.Default.CardEditor_ShowCourseNames ? Courses.CourseTable[i] : i.ToString(),
                    result.Car.ToString(),
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
                for (int i = 0; i < v337Card.Data_2.Ta.Count; i++)
                    v337Card.Data_2.Ta[i] = new Card_v337.Data2.TA();
                ((Form)((Button)sender).Parent).Close();
            }
        }

        private void BtnTenFightClearResults_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Are you SURE you want to clear TenFight results? This will clear all TenFight results!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < v337Card.Data_2.TenResults.Count; i++)
                    v337Card.Data_2.TenResults[i].Result = 0;
                ((Form)((Button)sender).Parent).Close();
            }
        }

        private void frmCard_v337_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TenFightForm != null && !TenFightForm.IsDisposed)
                TenFightForm.Close();
            if (TAForm != null && !TAForm.IsDisposed)
                TAForm.Close();
            BaseCard.Dispose();
        }

        private void radV337_Click(object sender, EventArgs e)
        {
            radV337Exp.Checked = true;
            radV337ExpSP.Checked = false;
            radV337Jpn.Checked = false;
            radV337JpnSP.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v337_EXP;
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void radV337SP_Click(object sender, EventArgs e)
        {
            radV337Exp.Checked = false;
            radV337ExpSP.Checked = true;
            radV337Jpn.Checked = false;
            radV337JpnSP.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v337_EXP_LOC_TEST;
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void radV337Jpn_Click(object sender, EventArgs e)
        {
            radV337Exp.Checked = false;
            radV337ExpSP.Checked = false;
            radV337Jpn.Checked = true;
            radV337JpnSP.Checked = false;
            BaseCard.BaseCard.Version = Card.EVersion.v337_JPN;
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void radV337JpnSP_Click(object sender, EventArgs e)
        {
            radV337Exp.Checked = false;
            radV337ExpSP.Checked = false;
            radV337Jpn.Checked = false;
            radV337JpnSP.Checked = true;
            BaseCard.BaseCard.Version = Card.EVersion.v337_JPN_LOC_TEST;
            Text = $"{v337Card.Data_1.Name} {BaseCard.BaseCard.Version}";
            UpdateTitleList();
        }

        private void ctxTitle_TextChanged(object sender, EventArgs e)
        {
            var titleId = Titles_v337.GetTitleIndex(BaseCard.BaseCard.Version, ctxTitle.Text);
            if (titleId != -1)
            {
                ctxTitle.ForeColor = SystemColors.ControlText;
                v337Card.Data_2.Title = (ushort)titleId;
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

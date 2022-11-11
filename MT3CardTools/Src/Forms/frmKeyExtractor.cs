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

using MT3CardTools.Src.GameTools;
using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Helpers;

namespace MT3CardTools.Src.Forms
{
    public partial class frmKeyExtractor : Form
    {
        public frmKeyExtractor()
        {
            InitializeComponent();
        }

        private void frmKeyExtractor_Load(object sender, EventArgs e)
        {
            txtData1Key.Text = Convert.FromBase64String(Properties.Settings.Default.Data1Key).ToHex();
            txtData2KeyTable.Text = Convert.FromBase64String(Properties.Settings.Default.Data2KeyTable).ToHex();
            txtMacKeyTable.Text = Convert.FromBase64String(Properties.Settings.Default.MacKeyTable).ToHex();
            txtPadKeyTable.Text = Convert.FromBase64String(Properties.Settings.Default.PadKeyTable).ToHex();

            BringToFront();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Data1Key = Convert.ToBase64String(txtData1Key.Text.FromHex());
            Properties.Settings.Default.Data2KeyTable = Convert.ToBase64String(txtData2KeyTable.Text.FromHex());
            Properties.Settings.Default.MacKeyTable = Convert.ToBase64String(txtMacKeyTable.Text.FromHex());
            Properties.Settings.Default.PadKeyTable = Convert.ToBase64String(txtPadKeyTable.Text.FromHex());
            Properties.Settings.Default.Save();
            //Manually refresh PadKeyTable
            Convert.FromBase64String(Properties.Settings.Default.PadKeyTable);
            Msg.Info("Saved keys!");
            Close();
        }

        private void btnFromGameExe_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Find game elf file";
                dlg.Filter = "Game executable (main)|main|All files (*.*)|*.*";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK &&
                    File.Exists(dlg.FileName))
                {
                    using (var file = new GameFile(dlg.FileName))
                    {
                        var keys = file.GetKeys();
                        if (keys == null)
                        {
                            Msg.Error("The selected game elf was not recognised!");
                            return;
                        }
                        txtData1Key.Text = keys.Data1Key.ToHex();
                        txtData2KeyTable.Text = keys.Data2KeyTable.ToHex();
                        txtMacKeyTable.Text = keys.MacKeyTable.ToHex();
                        txtPadKeyTable.Text = keys.PadKeyTable.ToHex();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT3CardTools.Src.Forms
{
    public partial class frmCardFileConverter : Form
    {
        public frmCardFileConverter()
        {
            InitializeComponent();
        }

        private void frmCardFileConverter_Load(object sender, EventArgs e)
        {
            cmbFrom.SelectedIndex = 0;
            cmbTo.SelectedIndex = 1;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            using (var srcDlg = new OpenFileDialog())
            using (var dstDlg = new SaveFileDialog())
            {
                srcDlg.Title = "Load source card file";
                srcDlg.Filter = "Card bin (*.bin)|*.bin|Split card bin (*.bin.track_0)|*.bin.track_0";
                srcDlg.Multiselect = false;
                dstDlg.Title = "Load source card file";
                dstDlg.Filter = "Card bin (*.bin)|*.bin|Split card bin (*.bin.track_0)|*.bin.track_0";
                if (srcDlg.ShowDialog() == DialogResult.OK &&
                    dstDlg.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}

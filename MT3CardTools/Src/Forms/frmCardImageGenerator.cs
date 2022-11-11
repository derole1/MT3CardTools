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
    public partial class frmCardImageGenerator : Form
    {
        public frmCardImageGenerator(Bitmap bmp)
        {
            InitializeComponent();
        }

        private void frmCardImageGenerator_Load(object sender, EventArgs e)
        {

        }

        private void frmCardImageGenerator_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        //public async Task ShowDialog(string message, bool cancel = false)
        //{
        //    ControlBox = cancel;
        //    lblWait.Text = message;
        //    ParentWindow.Enabled = false;
        //    Show();
        //    Enabled = true;
        //}

        //public new void Hide()
        //{
        //    ParentWindow.Enabled = true;
        //    base.Hide();
        //}
    }
}

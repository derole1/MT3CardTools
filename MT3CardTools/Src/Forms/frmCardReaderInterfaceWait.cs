using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MT3CardTools.Src.CardTools.ReaderNew;

namespace MT3CardTools.Src.Forms
{
    public partial class frmCardReaderInterfaceWait : Form
    {
        public frmCardReaderInterfaceWait()
        {
            InitializeComponent();
        }

        public Form ParentWindow { get; set; }
        private Func<Task> Callback { get; set; }

        private void frmCardReaderInterfaceWait_Load(object sender, EventArgs e)
        {

        }

        private void frmCardReaderInterfaceWait_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        public void Show(string message, bool cancel = false)
        {
            ControlBox = cancel;
            lblWait.Text = message;
            ParentWindow.Enabled = false;
            Show();
            Enabled = true;
        }

        public new void Hide()
        {
            ParentWindow.Enabled = true;
            base.Hide();
        }

        private async void frmCardReaderInterfaceWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (ControlBox)
            {
                ParentWindow.Close();
                Hide();
            }
        }
    }
}

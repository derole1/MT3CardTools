using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MT3CardTools.Src.Controls;

namespace MT3CardTools.Src.Forms
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        public Random Rnd { get; } = new Random();

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblName.Text = Application.ProductName;
            lblVersion.Text = Application.ProductVersion;
            lblStats.Text = $"Machine Id: {Properties.Settings.Default.CardGenMachineSerial}\n" +
                $"Card count: {Properties.Settings.Default.CardGenCount}";
        }

        private void btnOk_Click(object sender, EventArgs e) => Close();

        private void lblPoweredBy_Click(object sender, EventArgs e)
        {
            var ctrl = new GravityBox(PointToClient(Cursor.Position), new PointF(-1, -3), (float)(Rnd.NextDouble() - 0.5));
            ctrl.SetImage((Bitmap)Properties.Resources.ResourceManager.GetObject("test"));
            Controls.Add(ctrl);
            ctrl.Show();
        }
    }
}

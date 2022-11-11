using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Controls
{
    public partial class GravityBox : UserControl
    {
        const int UPDATE_INTERVAL = 16;

        public PointF Position { get; set; }
        public float Gravity { get; set; }
        public PointF Velocity { get; set; }
        public float Rotation { get; set; }
        public float RVelocity { get; set; }
        public Bitmap Image { get; set; }

        private Stopwatch PerfSWatch { get; set; } = new Stopwatch();
        private Stopwatch UpdateSWatch { get; set; } = new Stopwatch();
        private long RotationInterval { get; set; }

        public GravityBox(Point location, PointF velocity, float rVelocity = 0.01f, float gravity = 0.04f)
        {
            Position = location;
            Velocity = velocity;
            RVelocity = rVelocity;
            Gravity = gravity;
            InitializeComponent();
        }

        public void SetImage(Bitmap image)
        {
            Image = image;
        }

        private void GravityBox_Load(object sender, EventArgs e)
        {
            Location = Position.ToPoint();
            tmrUpdate.Interval = UPDATE_INTERVAL;
            UpdateSWatch.Start();
            tmrUpdate.Start();

            BringToFront();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            PerfSWatch.Start();
            Position = Position.Add(Velocity.Multiply(Math.Max(RotationInterval / 2, 1)));
            Rotation += RVelocity * Math.Max(RotationInterval, 1);
            Velocity = Velocity.Zero(new PointF(0, Gravity)).Add(new PointF(0, Gravity * 3));
            Location = Position.ToPoint();
            if (UpdateSWatch.ElapsedMilliseconds > Math.Pow(RotationInterval, 2) && Image != null)
            {
                if (pctMain.BackgroundImage != null)
                    pctMain.BackgroundImage.Dispose();
                pctMain.BackgroundImage = Image.RotateImage(Rotation);
                //Log.Debug($"tmrUpdate_Tick: UpdateSWatch.ElapsedMilliseconds:{UpdateSWatch.ElapsedMilliseconds}");
                UpdateSWatch.Reset();
                UpdateSWatch.Start();
            }
            if (!Bounds.IntersectsWith(ParentForm.ClientRectangle))
            {
                PerfSWatch.Stop();
                UpdateSWatch.Stop();
                tmrUpdate.Stop();
                pctMain.BackgroundImage.Dispose();
                pctMain.Dispose();
                Dispose();
            }
            else
            {
                PerfSWatch.Stop();
                RotationInterval = PerfSWatch.ElapsedMilliseconds;
                //Log.Debug($"tmrUpdate_Tick: PerfSWatch.ElapsedMilliseconds:{PerfSWatch.ElapsedMilliseconds}");
                PerfSWatch.Reset();
            }
        }
    }
}

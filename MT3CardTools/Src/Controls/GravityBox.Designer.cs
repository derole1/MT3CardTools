
namespace MT3CardTools.Src.Controls
{
    partial class GravityBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.pctMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 16;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // pctMain
            // 
            this.pctMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctMain.Location = new System.Drawing.Point(0, 0);
            this.pctMain.Name = "pctMain";
            this.pctMain.Size = new System.Drawing.Size(150, 150);
            this.pctMain.TabIndex = 0;
            this.pctMain.TabStop = false;
            // 
            // GravityBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pctMain);
            this.Name = "GravityBox";
            this.Load += new System.EventHandler(this.GravityBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.PictureBox pctMain;
    }
}


namespace MT3CardTools.Src.Forms
{
    partial class frmCardReaderInterfaceWait
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWait = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWait
            // 
            this.lblWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWait.Location = new System.Drawing.Point(0, 0);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(238, 86);
            this.lblWait.TabIndex = 0;
            this.lblWait.Text = "WAIT! Executing command...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCardReaderInterfaceWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 86);
            this.ControlBox = false;
            this.Controls.Add(this.lblWait);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCardReaderInterfaceWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please wait...";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCardReaderInterfaceWait_FormClosing);
            this.Load += new System.EventHandler(this.frmCardReaderInterfaceWait_Load);
            this.Shown += new System.EventHandler(this.frmCardReaderInterfaceWait_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWait;
    }
}
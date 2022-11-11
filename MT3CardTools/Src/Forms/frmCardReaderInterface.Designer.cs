
namespace MT3CardTools.Src.Forms
{
    partial class frmCardReaderInterface
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
            this.components = new System.ComponentModel.Container();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblComPort = new System.Windows.Forms.Label();
            this.btnReadCard = new System.Windows.Forms.Button();
            this.btnWriteCard = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.btnPrintCard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(255, 26);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Idle";
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(12, 41);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(29, 13);
            this.lblComPort.TabIndex = 2;
            this.lblComPort.Text = "Port:";
            // 
            // btnReadCard
            // 
            this.btnReadCard.Enabled = false;
            this.btnReadCard.Location = new System.Drawing.Point(12, 94);
            this.btnReadCard.Name = "btnReadCard";
            this.btnReadCard.Size = new System.Drawing.Size(125, 23);
            this.btnReadCard.TabIndex = 3;
            this.btnReadCard.Text = "Read card";
            this.btnReadCard.UseVisualStyleBackColor = true;
            this.btnReadCard.Click += new System.EventHandler(this.btnReadCard_Click);
            // 
            // btnWriteCard
            // 
            this.btnWriteCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteCard.Enabled = false;
            this.btnWriteCard.Location = new System.Drawing.Point(143, 94);
            this.btnWriteCard.Name = "btnWriteCard";
            this.btnWriteCard.Size = new System.Drawing.Size(125, 23);
            this.btnWriteCard.TabIndex = 4;
            this.btnWriteCard.Text = "Write card";
            this.btnWriteCard.UseVisualStyleBackColor = true;
            this.btnWriteCard.Click += new System.EventHandler(this.btnWriteCard_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(12, 65);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(256, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbComPort
            // 
            this.cmbComPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(41, 38);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(226, 21);
            this.cmbComPort.TabIndex = 7;
            // 
            // tmrStatus
            // 
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // btnPrintCard
            // 
            this.btnPrintCard.Enabled = false;
            this.btnPrintCard.Location = new System.Drawing.Point(12, 123);
            this.btnPrintCard.Name = "btnPrintCard";
            this.btnPrintCard.Size = new System.Drawing.Size(255, 23);
            this.btnPrintCard.TabIndex = 8;
            this.btnPrintCard.Text = "Print card";
            this.btnPrintCard.UseVisualStyleBackColor = true;
            this.btnPrintCard.Visible = false;
            this.btnPrintCard.Click += new System.EventHandler(this.btnPrintCard_Click);
            // 
            // frmCardReaderInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 127);
            this.Controls.Add(this.btnPrintCard);
            this.Controls.Add(this.cmbComPort);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnWriteCard);
            this.Controls.Add(this.btnReadCard);
            this.Controls.Add(this.lblComPort);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCardReaderInterface";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reader - Disconnected";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCardReaderInterface_FormClosing);
            this.Load += new System.EventHandler(this.frmCardReaderInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Button btnReadCard;
        private System.Windows.Forms.Button btnWriteCard;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.Button btnPrintCard;
    }
}

namespace MT3CardTools.Src.Forms
{
    partial class frmWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelcome));
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblParagraph1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblParagraph2 = new System.Windows.Forms.Label();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlMainContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(12, 9);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(391, 31);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "Welcome to MT3 Card Tools!";
            // 
            // lblParagraph1
            // 
            this.lblParagraph1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParagraph1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParagraph1.Location = new System.Drawing.Point(0, 0);
            this.lblParagraph1.Name = "lblParagraph1";
            this.lblParagraph1.Size = new System.Drawing.Size(846, 329);
            this.lblParagraph1.TabIndex = 1;
            this.lblParagraph1.Text = resources.GetString("lblParagraph1.Text");
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(783, 424);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblParagraph2
            // 
            this.lblParagraph2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParagraph2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParagraph2.Location = new System.Drawing.Point(12, 375);
            this.lblParagraph2.Name = "lblParagraph2";
            this.lblParagraph2.Size = new System.Drawing.Size(765, 78);
            this.lblParagraph2.TabIndex = 3;
            this.lblParagraph2.Text = resources.GetString("lblParagraph2.Text");
            this.lblParagraph2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMainContent.AutoScroll = true;
            this.pnlMainContent.Controls.Add(this.lblParagraph1);
            this.pnlMainContent.Location = new System.Drawing.Point(12, 43);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(846, 329);
            this.pnlMainContent.TabIndex = 4;
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 459);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.lblParagraph2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblTitle1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(886, 527);
            this.MinimizeBox = false;
            this.Name = "frmWelcome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gao Gao! Welcome to MT3 Card Tools!";
            this.pnlMainContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblParagraph1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblParagraph2;
        private System.Windows.Forms.Panel pnlMainContent;
    }
}
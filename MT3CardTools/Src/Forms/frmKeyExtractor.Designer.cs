
namespace MT3CardTools.Src.Forms
{
    partial class frmKeyExtractor
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
            this.lblData1Key = new System.Windows.Forms.Label();
            this.txtData1Key = new System.Windows.Forms.TextBox();
            this.txtData2KeyTable = new System.Windows.Forms.TextBox();
            this.lblData2KeyTable = new System.Windows.Forms.Label();
            this.txtMacKeyTable = new System.Windows.Forms.TextBox();
            this.lblMacKeyTable = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFromGameExe = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPadKeyTable = new System.Windows.Forms.TextBox();
            this.lblPadKeyTable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblData1Key
            // 
            this.lblData1Key.AutoSize = true;
            this.lblData1Key.Location = new System.Drawing.Point(12, 9);
            this.lblData1Key.Name = "lblData1Key";
            this.lblData1Key.Size = new System.Drawing.Size(56, 13);
            this.lblData1Key.TabIndex = 0;
            this.lblData1Key.Text = "Data1 key";
            // 
            // txtData1Key
            // 
            this.txtData1Key.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData1Key.Location = new System.Drawing.Point(12, 25);
            this.txtData1Key.Name = "txtData1Key";
            this.txtData1Key.Size = new System.Drawing.Size(324, 20);
            this.txtData1Key.TabIndex = 1;
            // 
            // txtData2KeyTable
            // 
            this.txtData2KeyTable.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData2KeyTable.Location = new System.Drawing.Point(12, 64);
            this.txtData2KeyTable.Multiline = true;
            this.txtData2KeyTable.Name = "txtData2KeyTable";
            this.txtData2KeyTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData2KeyTable.Size = new System.Drawing.Size(324, 60);
            this.txtData2KeyTable.TabIndex = 3;
            // 
            // lblData2KeyTable
            // 
            this.lblData2KeyTable.AutoSize = true;
            this.lblData2KeyTable.Location = new System.Drawing.Point(12, 48);
            this.lblData2KeyTable.Name = "lblData2KeyTable";
            this.lblData2KeyTable.Size = new System.Drawing.Size(82, 13);
            this.lblData2KeyTable.TabIndex = 2;
            this.lblData2KeyTable.Text = "Data2 key table";
            // 
            // txtMacKeyTable
            // 
            this.txtMacKeyTable.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMacKeyTable.Location = new System.Drawing.Point(12, 143);
            this.txtMacKeyTable.Multiline = true;
            this.txtMacKeyTable.Name = "txtMacKeyTable";
            this.txtMacKeyTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMacKeyTable.Size = new System.Drawing.Size(324, 60);
            this.txtMacKeyTable.TabIndex = 5;
            // 
            // lblMacKeyTable
            // 
            this.lblMacKeyTable.AutoSize = true;
            this.lblMacKeyTable.Location = new System.Drawing.Point(12, 127);
            this.lblMacKeyTable.Name = "lblMacKeyTable";
            this.lblMacKeyTable.Size = new System.Drawing.Size(74, 13);
            this.lblMacKeyTable.TabIndex = 4;
            this.lblMacKeyTable.Text = "Mac key table";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(591, 209);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFromGameExe
            // 
            this.btnFromGameExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFromGameExe.Location = new System.Drawing.Point(460, 209);
            this.btnFromGameExe.Name = "btnFromGameExe";
            this.btnFromGameExe.Size = new System.Drawing.Size(125, 23);
            this.btnFromGameExe.TabIndex = 7;
            this.btnFromGameExe.Text = "From game executable";
            this.btnFromGameExe.UseVisualStyleBackColor = true;
            this.btnFromGameExe.Click += new System.EventHandler(this.btnFromGameExe_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(379, 209);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPadKeyTable
            // 
            this.txtPadKeyTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPadKeyTable.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPadKeyTable.Location = new System.Drawing.Point(342, 25);
            this.txtPadKeyTable.Multiline = true;
            this.txtPadKeyTable.Name = "txtPadKeyTable";
            this.txtPadKeyTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPadKeyTable.Size = new System.Drawing.Size(324, 178);
            this.txtPadKeyTable.TabIndex = 10;
            // 
            // lblPadKeyTable
            // 
            this.lblPadKeyTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPadKeyTable.AutoSize = true;
            this.lblPadKeyTable.Location = new System.Drawing.Point(342, 9);
            this.lblPadKeyTable.Name = "lblPadKeyTable";
            this.lblPadKeyTable.Size = new System.Drawing.Size(72, 13);
            this.lblPadKeyTable.TabIndex = 9;
            this.lblPadKeyTable.Text = "Pad key table";
            // 
            // frmKeyExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 240);
            this.Controls.Add(this.txtPadKeyTable);
            this.Controls.Add(this.lblPadKeyTable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFromGameExe);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtMacKeyTable);
            this.Controls.Add(this.lblMacKeyTable);
            this.Controls.Add(this.txtData2KeyTable);
            this.Controls.Add(this.lblData2KeyTable);
            this.Controls.Add(this.txtData1Key);
            this.Controls.Add(this.lblData1Key);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmKeyExtractor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encryption key extractor";
            this.Load += new System.EventHandler(this.frmKeyExtractor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblData1Key;
        private System.Windows.Forms.TextBox txtData1Key;
        private System.Windows.Forms.TextBox txtData2KeyTable;
        private System.Windows.Forms.Label lblData2KeyTable;
        private System.Windows.Forms.TextBox txtMacKeyTable;
        private System.Windows.Forms.Label lblMacKeyTable;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFromGameExe;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPadKeyTable;
        private System.Windows.Forms.Label lblPadKeyTable;
    }
}
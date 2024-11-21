namespace MT3CardTools.Src.Forms
{
    partial class frmCardIDChanger
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
            this.grpPrevId = new System.Windows.Forms.GroupBox();
            this.numPrevId1 = new System.Windows.Forms.NumericUpDown();
            this.numPrevId2 = new System.Windows.Forms.NumericUpDown();
            this.grpNewId = new System.Windows.Forms.GroupBox();
            this.numNewId1 = new System.Windows.Forms.NumericUpDown();
            this.numNewId2 = new System.Windows.Forms.NumericUpDown();
            this.btnChangeIDSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.grpPrevId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrevId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrevId2)).BeginInit();
            this.grpNewId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNewId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewId2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPrevId
            // 
            this.grpPrevId.Controls.Add(this.numPrevId1);
            this.grpPrevId.Controls.Add(this.numPrevId2);
            this.grpPrevId.Location = new System.Drawing.Point(12, 64);
            this.grpPrevId.Name = "grpPrevId";
            this.grpPrevId.Size = new System.Drawing.Size(182, 74);
            this.grpPrevId.TabIndex = 15;
            this.grpPrevId.TabStop = false;
            this.grpPrevId.Text = "Previous Id";
            // 
            // numPrevId1
            // 
            this.numPrevId1.Enabled = false;
            this.numPrevId1.Location = new System.Drawing.Point(6, 19);
            this.numPrevId1.Maximum = new decimal(new int[] {
            1048575,
            0,
            0,
            0});
            this.numPrevId1.Name = "numPrevId1";
            this.numPrevId1.ReadOnly = true;
            this.numPrevId1.Size = new System.Drawing.Size(170, 20);
            this.numPrevId1.TabIndex = 10;
            // 
            // numPrevId2
            // 
            this.numPrevId2.Enabled = false;
            this.numPrevId2.Location = new System.Drawing.Point(6, 45);
            this.numPrevId2.Maximum = new decimal(new int[] {
            1048575,
            0,
            0,
            0});
            this.numPrevId2.Name = "numPrevId2";
            this.numPrevId2.ReadOnly = true;
            this.numPrevId2.Size = new System.Drawing.Size(170, 20);
            this.numPrevId2.TabIndex = 11;
            // 
            // grpNewId
            // 
            this.grpNewId.Controls.Add(this.numNewId1);
            this.grpNewId.Controls.Add(this.numNewId2);
            this.grpNewId.Location = new System.Drawing.Point(200, 64);
            this.grpNewId.Name = "grpNewId";
            this.grpNewId.Size = new System.Drawing.Size(182, 74);
            this.grpNewId.TabIndex = 16;
            this.grpNewId.TabStop = false;
            this.grpNewId.Text = "New Id";
            // 
            // numNewId1
            // 
            this.numNewId1.Enabled = false;
            this.numNewId1.Location = new System.Drawing.Point(6, 19);
            this.numNewId1.Maximum = new decimal(new int[] {
            1048575,
            0,
            0,
            0});
            this.numNewId1.Name = "numNewId1";
            this.numNewId1.ReadOnly = true;
            this.numNewId1.Size = new System.Drawing.Size(170, 20);
            this.numNewId1.TabIndex = 10;
            // 
            // numNewId2
            // 
            this.numNewId2.Enabled = false;
            this.numNewId2.Location = new System.Drawing.Point(6, 45);
            this.numNewId2.Maximum = new decimal(new int[] {
            1048575,
            0,
            0,
            0});
            this.numNewId2.Name = "numNewId2";
            this.numNewId2.ReadOnly = true;
            this.numNewId2.Size = new System.Drawing.Size(170, 20);
            this.numNewId2.TabIndex = 11;
            // 
            // btnChangeIDSave
            // 
            this.btnChangeIDSave.Location = new System.Drawing.Point(12, 144);
            this.btnChangeIDSave.Name = "btnChangeIDSave";
            this.btnChangeIDSave.Size = new System.Drawing.Size(370, 23);
            this.btnChangeIDSave.TabIndex = 17;
            this.btnChangeIDSave.Text = "Change to new ID and save";
            this.btnChangeIDSave.UseVisualStyleBackColor = true;
            this.btnChangeIDSave.Click += new System.EventHandler(this.btnChangeIDSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(370, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 41);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 20;
            this.lblVersion.Text = "Version";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(60, 12);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(322, 20);
            this.txtName.TabIndex = 21;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(60, 38);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(322, 20);
            this.txtVersion.TabIndex = 22;
            // 
            // frmCardIDChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 205);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChangeIDSave);
            this.Controls.Add(this.grpNewId);
            this.Controls.Add(this.grpPrevId);
            this.MaximizeBox = false;
            this.Name = "frmCardIDChanger";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card ID Changer";
            this.Load += new System.EventHandler(this.frmCardIDChanger_Load);
            this.grpPrevId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPrevId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrevId2)).EndInit();
            this.grpNewId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numNewId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewId2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPrevId;
        private System.Windows.Forms.NumericUpDown numPrevId1;
        private System.Windows.Forms.NumericUpDown numPrevId2;
        private System.Windows.Forms.GroupBox grpNewId;
        private System.Windows.Forms.NumericUpDown numNewId1;
        private System.Windows.Forms.NumericUpDown numNewId2;
        private System.Windows.Forms.Button btnChangeIDSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtVersion;
    }
}
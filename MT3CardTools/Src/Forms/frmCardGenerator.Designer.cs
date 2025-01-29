
namespace MT3CardTools.Src.Forms
{
    partial class frmCardGenerator
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
            this.grpId = new System.Windows.Forms.GroupBox();
            this.numId1 = new System.Windows.Forms.NumericUpDown();
            this.chkIdDevel = new System.Windows.Forms.CheckBox();
            this.numId2 = new System.Windows.Forms.NumericUpDown();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCar = new System.Windows.Forms.Label();
            this.cmbCar = new System.Windows.Forms.ComboBox();
            this.chkMission = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.chkIsPresentOrSpecial = new System.Windows.Forms.CheckBox();
            this.chkVerUp = new System.Windows.Forms.CheckBox();
            this.grpId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpId
            // 
            this.grpId.Controls.Add(this.numId1);
            this.grpId.Controls.Add(this.chkIdDevel);
            this.grpId.Controls.Add(this.numId2);
            this.grpId.Enabled = false;
            this.grpId.Location = new System.Drawing.Point(12, 52);
            this.grpId.Name = "grpId";
            this.grpId.Size = new System.Drawing.Size(276, 93);
            this.grpId.TabIndex = 15;
            this.grpId.TabStop = false;
            this.grpId.Text = "Id";
            // 
            // numId1
            // 
            this.numId1.Location = new System.Drawing.Point(6, 19);
            this.numId1.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.numId1.Name = "numId1";
            this.numId1.Size = new System.Drawing.Size(264, 20);
            this.numId1.TabIndex = 10;
            this.numId1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkIdDevel
            // 
            this.chkIdDevel.AutoSize = true;
            this.chkIdDevel.Location = new System.Drawing.Point(6, 71);
            this.chkIdDevel.Name = "chkIdDevel";
            this.chkIdDevel.Size = new System.Drawing.Size(54, 17);
            this.chkIdDevel.TabIndex = 12;
            this.chkIdDevel.Text = "Devel";
            this.chkIdDevel.UseVisualStyleBackColor = true;
            // 
            // numId2
            // 
            this.numId2.Location = new System.Drawing.Point(6, 45);
            this.numId2.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.numId2.Name = "numId2";
            this.numId2.Size = new System.Drawing.Size(264, 20);
            this.numId2.TabIndex = 11;
            this.numId2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 148);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 164);
            this.txtName.MaxLength = 5;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(276, 20);
            this.txtName.TabIndex = 17;
            this.txtName.Text = "ＧＵＥＳＴ";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblCar
            // 
            this.lblCar.AutoSize = true;
            this.lblCar.Location = new System.Drawing.Point(9, 187);
            this.lblCar.Name = "lblCar";
            this.lblCar.Size = new System.Drawing.Size(23, 13);
            this.lblCar.TabIndex = 18;
            this.lblCar.Text = "Car";
            // 
            // cmbCar
            // 
            this.cmbCar.FormattingEnabled = true;
            this.cmbCar.Location = new System.Drawing.Point(12, 203);
            this.cmbCar.Name = "cmbCar";
            this.cmbCar.Size = new System.Drawing.Size(276, 21);
            this.cmbCar.TabIndex = 19;
            // 
            // chkMission
            // 
            this.chkMission.AutoSize = true;
            this.chkMission.Location = new System.Drawing.Point(12, 230);
            this.chkMission.Name = "chkMission";
            this.chkMission.Size = new System.Drawing.Size(61, 17);
            this.chkMission.TabIndex = 20;
            this.chkMission.Text = "Mission";
            this.chkMission.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 253);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(276, 23);
            this.btnGenerate.TabIndex = 21;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(9, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 22;
            this.lblVersion.Text = "Version";
            // 
            // cmbVersion
            // 
            this.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(12, 25);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(276, 21);
            this.cmbVersion.TabIndex = 23;
            this.cmbVersion.SelectedIndexChanged += new System.EventHandler(this.cmbVersion_SelectedIndexChanged);
            // 
            // chkIsPresentOrSpecial
            // 
            this.chkIsPresentOrSpecial.AutoSize = true;
            this.chkIsPresentOrSpecial.Location = new System.Drawing.Point(79, 230);
            this.chkIsPresentOrSpecial.Name = "chkIsPresentOrSpecial";
            this.chkIsPresentOrSpecial.Size = new System.Drawing.Size(62, 17);
            this.chkIsPresentOrSpecial.TabIndex = 24;
            this.chkIsPresentOrSpecial.Text = "Present";
            this.chkIsPresentOrSpecial.UseVisualStyleBackColor = true;
            // 
            // chkVerUp
            // 
            this.chkVerUp.AutoSize = true;
            this.chkVerUp.Location = new System.Drawing.Point(147, 230);
            this.chkVerUp.Name = "chkVerUp";
            this.chkVerUp.Size = new System.Drawing.Size(59, 17);
            this.chkVerUp.TabIndex = 25;
            this.chkVerUp.Text = "Ver Up";
            this.chkVerUp.UseVisualStyleBackColor = true;
            this.chkVerUp.CheckedChanged += new System.EventHandler(this.chkVerUp_CheckedChanged);
            // 
            // frmCardGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 289);
            this.Controls.Add(this.chkVerUp);
            this.Controls.Add(this.chkIsPresentOrSpecial);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmbVersion);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.chkMission);
            this.Controls.Add(this.lblCar);
            this.Controls.Add(this.cmbCar);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.grpId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCardGenerator";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card Generator";
            this.Load += new System.EventHandler(this.frmCardGenerator_Load);
            this.Shown += new System.EventHandler(this.frmCardGenerator_Shown);
            this.grpId.ResumeLayout(false);
            this.grpId.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpId;
        private System.Windows.Forms.NumericUpDown numId1;
        private System.Windows.Forms.CheckBox chkIdDevel;
        private System.Windows.Forms.NumericUpDown numId2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCar;
        private System.Windows.Forms.ComboBox cmbCar;
        private System.Windows.Forms.CheckBox chkMission;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.CheckBox chkIsPresentOrSpecial;
        private System.Windows.Forms.CheckBox chkVerUp;
    }
}
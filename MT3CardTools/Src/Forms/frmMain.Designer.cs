
namespace MT3CardTools.Src.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenCard = new System.Windows.Forms.ToolStripMenuItem();
            this.sepFile1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentCards = new System.Windows.Forms.ToolStripMenuItem();
            this.sepFile2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEncryptionKeyExtractor = new System.Windows.Forms.ToolStripMenuItem();
            this.sepTools1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCardFileConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCardGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCardImageGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.cardIDChangerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCardReaderInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditorBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditorBackgroundChange = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditorBackgroundRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCardReaderInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaudRate = new System.Windows.Forms.ToolStripMenuItem();
            this.radBaud9600 = new System.Windows.Forms.ToolStripMenuItem();
            this.radBaud19200 = new System.Windows.Forms.ToolStripMenuItem();
            this.radBaud38400 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuParity = new System.Windows.Forms.ToolStripMenuItem();
            this.radParityNone = new System.Windows.Forms.ToolStripMenuItem();
            this.radParityEven = new System.Windows.Forms.ToolStripMenuItem();
            this.chkUsePipes = new System.Windows.Forms.ToolStripMenuItem();
            this.chkVerifyAfterWriting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCardEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDefaultCardType = new System.Windows.Forms.ToolStripMenuItem();
            this.radTrackSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.radSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.chkAddAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.chkHideUnsupportedCarsWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.sepOptions1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResetAllOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.sepOptions2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenWelcomeMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCloseAllWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.chkShowCourseNames = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.ctxRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuOptions});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1324, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "mnuMain";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenCard,
            this.sepFile1,
            this.mnuRecentCards,
            this.sepFile2,
            this.btnAbout,
            this.btnExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            this.mnuFile.DropDownOpening += new System.EventHandler(this.mnuFile_DropDownOpening);
            // 
            // btnOpenCard
            // 
            this.btnOpenCard.Name = "btnOpenCard";
            this.btnOpenCard.Size = new System.Drawing.Size(141, 22);
            this.btnOpenCard.Text = "Open card";
            this.btnOpenCard.Click += new System.EventHandler(this.btnOpenCard_Click);
            // 
            // sepFile1
            // 
            this.sepFile1.Name = "sepFile1";
            this.sepFile1.Size = new System.Drawing.Size(138, 6);
            // 
            // mnuRecentCards
            // 
            this.mnuRecentCards.Name = "mnuRecentCards";
            this.mnuRecentCards.Size = new System.Drawing.Size(141, 22);
            this.mnuRecentCards.Text = "Recent cards";
            // 
            // sepFile2
            // 
            this.sepFile2.Name = "sepFile2";
            this.sepFile2.Size = new System.Drawing.Size(138, 6);
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(141, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(141, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEncryptionKeyExtractor,
            this.sepTools1,
            this.btnCardFileConverter,
            this.btnCardGenerator,
            this.btnCardImageGenerator,
            this.cardIDChangerToolStripMenuItem,
            this.btnCardReaderInterface});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(47, 20);
            this.mnuTools.Text = "Tools";
            // 
            // btnEncryptionKeyExtractor
            // 
            this.btnEncryptionKeyExtractor.Name = "btnEncryptionKeyExtractor";
            this.btnEncryptionKeyExtractor.Size = new System.Drawing.Size(201, 22);
            this.btnEncryptionKeyExtractor.Text = "Encryption key extractor";
            this.btnEncryptionKeyExtractor.Click += new System.EventHandler(this.btnEncryptionKeyExtractor_Click);
            // 
            // sepTools1
            // 
            this.sepTools1.Name = "sepTools1";
            this.sepTools1.Size = new System.Drawing.Size(198, 6);
            // 
            // btnCardFileConverter
            // 
            this.btnCardFileConverter.Name = "btnCardFileConverter";
            this.btnCardFileConverter.Size = new System.Drawing.Size(201, 22);
            this.btnCardFileConverter.Text = "Card file converter";
            this.btnCardFileConverter.Click += new System.EventHandler(this.btnCardFileConverter_Click);
            // 
            // btnCardGenerator
            // 
            this.btnCardGenerator.Name = "btnCardGenerator";
            this.btnCardGenerator.Size = new System.Drawing.Size(201, 22);
            this.btnCardGenerator.Text = "Card generator";
            this.btnCardGenerator.Click += new System.EventHandler(this.btnCardGenerator_Click);
            // 
            // btnCardImageGenerator
            // 
            this.btnCardImageGenerator.Name = "btnCardImageGenerator";
            this.btnCardImageGenerator.Size = new System.Drawing.Size(201, 22);
            this.btnCardImageGenerator.Text = "Card image generator";
            this.btnCardImageGenerator.Visible = false;
            // 
            // cardIDChangerToolStripMenuItem
            // 
            this.cardIDChangerToolStripMenuItem.Name = "cardIDChangerToolStripMenuItem";
            this.cardIDChangerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cardIDChangerToolStripMenuItem.Text = "Card ID changer";
            this.cardIDChangerToolStripMenuItem.Click += new System.EventHandler(this.cardIDChangerToolStripMenuItem_Click);
            // 
            // btnCardReaderInterface
            // 
            this.btnCardReaderInterface.Name = "btnCardReaderInterface";
            this.btnCardReaderInterface.Size = new System.Drawing.Size(201, 22);
            this.btnCardReaderInterface.Text = "Card reader interface";
            this.btnCardReaderInterface.Click += new System.EventHandler(this.btnCardReaderInterface_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditorBackground,
            this.mnuCardReaderInterface,
            this.mnuCardEditor,
            this.sepOptions1,
            this.btnResetAllOptions,
            this.sepOptions2,
            this.btnOpenWelcomeMessage});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuEditorBackground
            // 
            this.mnuEditorBackground.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundToolStripMenuItem});
            this.mnuEditorBackground.Name = "mnuEditorBackground";
            this.mnuEditorBackground.Size = new System.Drawing.Size(219, 22);
            this.mnuEditorBackground.Text = "Main window";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEditorBackgroundChange,
            this.btnEditorBackgroundRemove});
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.backgroundToolStripMenuItem.Text = "Background";
            // 
            // btnEditorBackgroundChange
            // 
            this.btnEditorBackgroundChange.Name = "btnEditorBackgroundChange";
            this.btnEditorBackgroundChange.Size = new System.Drawing.Size(117, 22);
            this.btnEditorBackgroundChange.Text = "Change";
            this.btnEditorBackgroundChange.Click += new System.EventHandler(this.btnEditorBackgroundChange_Click);
            // 
            // btnEditorBackgroundRemove
            // 
            this.btnEditorBackgroundRemove.Name = "btnEditorBackgroundRemove";
            this.btnEditorBackgroundRemove.Size = new System.Drawing.Size(117, 22);
            this.btnEditorBackgroundRemove.Text = "Remove";
            this.btnEditorBackgroundRemove.Click += new System.EventHandler(this.btnEditorBackgroundRemove_Click);
            // 
            // mnuCardReaderInterface
            // 
            this.mnuCardReaderInterface.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaudRate,
            this.mnuParity,
            this.chkUsePipes,
            this.chkVerifyAfterWriting});
            this.mnuCardReaderInterface.Name = "mnuCardReaderInterface";
            this.mnuCardReaderInterface.Size = new System.Drawing.Size(219, 22);
            this.mnuCardReaderInterface.Text = "Card reader interface";
            // 
            // mnuBaudRate
            // 
            this.mnuBaudRate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radBaud9600,
            this.radBaud19200,
            this.radBaud38400});
            this.mnuBaudRate.Name = "mnuBaudRate";
            this.mnuBaudRate.Size = new System.Drawing.Size(209, 22);
            this.mnuBaudRate.Text = "Baud rate";
            // 
            // radBaud9600
            // 
            this.radBaud9600.CheckOnClick = true;
            this.radBaud9600.Name = "radBaud9600";
            this.radBaud9600.Size = new System.Drawing.Size(104, 22);
            this.radBaud9600.Text = "9600";
            this.radBaud9600.Click += new System.EventHandler(this.radBaud9600_Click);
            // 
            // radBaud19200
            // 
            this.radBaud19200.CheckOnClick = true;
            this.radBaud19200.Name = "radBaud19200";
            this.radBaud19200.Size = new System.Drawing.Size(104, 22);
            this.radBaud19200.Text = "19200";
            this.radBaud19200.Click += new System.EventHandler(this.radBaud19200_Click);
            // 
            // radBaud38400
            // 
            this.radBaud38400.CheckOnClick = true;
            this.radBaud38400.Name = "radBaud38400";
            this.radBaud38400.Size = new System.Drawing.Size(104, 22);
            this.radBaud38400.Text = "38400";
            this.radBaud38400.Click += new System.EventHandler(this.radBaud38400_Click);
            // 
            // mnuParity
            // 
            this.mnuParity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radParityNone,
            this.radParityEven});
            this.mnuParity.Name = "mnuParity";
            this.mnuParity.Size = new System.Drawing.Size(209, 22);
            this.mnuParity.Text = "Parity";
            // 
            // radParityNone
            // 
            this.radParityNone.CheckOnClick = true;
            this.radParityNone.Name = "radParityNone";
            this.radParityNone.Size = new System.Drawing.Size(103, 22);
            this.radParityNone.Text = "None";
            this.radParityNone.Click += new System.EventHandler(this.radParityNone_Click);
            // 
            // radParityEven
            // 
            this.radParityEven.CheckOnClick = true;
            this.radParityEven.Name = "radParityEven";
            this.radParityEven.Size = new System.Drawing.Size(103, 22);
            this.radParityEven.Text = "Even";
            this.radParityEven.Click += new System.EventHandler(this.radParityEven_Click);
            // 
            // chkUsePipes
            // 
            this.chkUsePipes.CheckOnClick = true;
            this.chkUsePipes.Name = "chkUsePipes";
            this.chkUsePipes.Size = new System.Drawing.Size(209, 22);
            this.chkUsePipes.Text = "Use pipes instead of serial";
            this.chkUsePipes.Click += new System.EventHandler(this.chkUsePipes_Click);
            // 
            // chkVerifyAfterWriting
            // 
            this.chkVerifyAfterWriting.CheckOnClick = true;
            this.chkVerifyAfterWriting.Name = "chkVerifyAfterWriting";
            this.chkVerifyAfterWriting.Size = new System.Drawing.Size(209, 22);
            this.chkVerifyAfterWriting.Text = "Verify after writing";
            this.chkVerifyAfterWriting.Click += new System.EventHandler(this.chkVerifyAfterWriting_Click);
            // 
            // mnuCardEditor
            // 
            this.mnuCardEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDefaultCardType,
            this.chkAddAllFiles,
            this.chkHideUnsupportedCarsWarning,
            this.chkShowCourseNames});
            this.mnuCardEditor.Name = "mnuCardEditor";
            this.mnuCardEditor.Size = new System.Drawing.Size(219, 22);
            this.mnuCardEditor.Text = "Card editor";
            // 
            // mnuDefaultCardType
            // 
            this.mnuDefaultCardType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radTrackSplit,
            this.radSingle});
            this.mnuDefaultCardType.Name = "mnuDefaultCardType";
            this.mnuDefaultCardType.Size = new System.Drawing.Size(258, 22);
            this.mnuDefaultCardType.Text = "Default card type";
            // 
            // radTrackSplit
            // 
            this.radTrackSplit.CheckOnClick = true;
            this.radTrackSplit.Name = "radTrackSplit";
            this.radTrackSplit.Size = new System.Drawing.Size(127, 22);
            this.radTrackSplit.Text = "Track split";
            this.radTrackSplit.Click += new System.EventHandler(this.radTrackSplit_Click);
            // 
            // radSingle
            // 
            this.radSingle.CheckOnClick = true;
            this.radSingle.Name = "radSingle";
            this.radSingle.Size = new System.Drawing.Size(127, 22);
            this.radSingle.Text = "Single";
            this.radSingle.Click += new System.EventHandler(this.radSingle_Click);
            // 
            // chkAddAllFiles
            // 
            this.chkAddAllFiles.CheckOnClick = true;
            this.chkAddAllFiles.Name = "chkAddAllFiles";
            this.chkAddAllFiles.Size = new System.Drawing.Size(258, 22);
            this.chkAddAllFiles.Text = "Add \"All Files\" file type";
            this.chkAddAllFiles.Click += new System.EventHandler(this.chkAddAllFiles_Click);
            // 
            // chkHideUnsupportedCarsWarning
            // 
            this.chkHideUnsupportedCarsWarning.CheckOnClick = true;
            this.chkHideUnsupportedCarsWarning.Name = "chkHideUnsupportedCarsWarning";
            this.chkHideUnsupportedCarsWarning.Size = new System.Drawing.Size(258, 22);
            this.chkHideUnsupportedCarsWarning.Text = "Hide warning for unsupported cars";
            this.chkHideUnsupportedCarsWarning.Click += new System.EventHandler(this.chkUnsupportedCarsWarning_Click);
            // 
            // sepOptions1
            // 
            this.sepOptions1.Name = "sepOptions1";
            this.sepOptions1.Size = new System.Drawing.Size(216, 6);
            // 
            // btnResetAllOptions
            // 
            this.btnResetAllOptions.Name = "btnResetAllOptions";
            this.btnResetAllOptions.Size = new System.Drawing.Size(219, 22);
            this.btnResetAllOptions.Text = "Reset all options to defaults";
            this.btnResetAllOptions.Click += new System.EventHandler(this.btnResetAllOptions_Click);
            // 
            // sepOptions2
            // 
            this.sepOptions2.Name = "sepOptions2";
            this.sepOptions2.Size = new System.Drawing.Size(216, 6);
            // 
            // btnOpenWelcomeMessage
            // 
            this.btnOpenWelcomeMessage.Name = "btnOpenWelcomeMessage";
            this.btnOpenWelcomeMessage.Size = new System.Drawing.Size(219, 22);
            this.btnOpenWelcomeMessage.Text = "Open welcome message";
            this.btnOpenWelcomeMessage.Click += new System.EventHandler(this.btnOpenWelcomeMessage_Click);
            // 
            // ctxRightClickMenu
            // 
            this.ctxRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseAllWindows});
            this.ctxRightClickMenu.Name = "ctxRightClickMenu";
            this.ctxRightClickMenu.Size = new System.Drawing.Size(169, 26);
            this.ctxRightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxRightClickMenu_Opening);
            // 
            // btnCloseAllWindows
            // 
            this.btnCloseAllWindows.Name = "btnCloseAllWindows";
            this.btnCloseAllWindows.Size = new System.Drawing.Size(168, 22);
            this.btnCloseAllWindows.Text = "Close all windows";
            this.btnCloseAllWindows.Click += new System.EventHandler(this.btnCloseAllWindows_Click);
            // 
            // chkShowCourseNames
            // 
            this.chkShowCourseNames.CheckOnClick = true;
            this.chkShowCourseNames.Name = "chkShowCourseNames";
            this.chkShowCourseNames.Size = new System.Drawing.Size(258, 22);
            this.chkShowCourseNames.Text = "Show course names";
            this.chkShowCourseNames.Click += new System.EventHandler(this.chkShowCourseNames_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1324, 786);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MT3 Card Tools";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ctxRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem btnOpenCard;
        private System.Windows.Forms.ToolStripSeparator sepFile1;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem btnEncryptionKeyExtractor;
        private System.Windows.Forms.ToolStripMenuItem btnCardGenerator;
        private System.Windows.Forms.ToolStripMenuItem btnCardReaderInterface;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuEditorBackground;
        private System.Windows.Forms.ToolStripMenuItem btnCardFileConverter;
        private System.Windows.Forms.ToolStripMenuItem btnOpenWelcomeMessage;
        private System.Windows.Forms.ToolStripMenuItem btnCardImageGenerator;
        private System.Windows.Forms.ToolStripSeparator sepOptions1;
        private System.Windows.Forms.ToolStripMenuItem mnuCardReaderInterface;
        private System.Windows.Forms.ToolStripMenuItem mnuBaudRate;
        private System.Windows.Forms.ToolStripMenuItem radBaud9600;
        private System.Windows.Forms.ToolStripMenuItem radBaud19200;
        private System.Windows.Forms.ToolStripMenuItem radBaud38400;
        private System.Windows.Forms.ToolStripMenuItem mnuParity;
        private System.Windows.Forms.ToolStripMenuItem radParityNone;
        private System.Windows.Forms.ToolStripMenuItem radParityEven;
        private System.Windows.Forms.ToolStripMenuItem chkUsePipes;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnEditorBackgroundChange;
        private System.Windows.Forms.ToolStripMenuItem btnEditorBackgroundRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentCards;
        private System.Windows.Forms.ToolStripSeparator sepFile2;
        private System.Windows.Forms.ToolStripMenuItem btnResetAllOptions;
        private System.Windows.Forms.ToolStripSeparator sepOptions2;
        private System.Windows.Forms.ToolStripMenuItem chkVerifyAfterWriting;
        private System.Windows.Forms.ContextMenuStrip ctxRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem btnCloseAllWindows;
        private System.Windows.Forms.ToolStripMenuItem mnuCardEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuDefaultCardType;
        private System.Windows.Forms.ToolStripMenuItem radTrackSplit;
        private System.Windows.Forms.ToolStripMenuItem radSingle;
        private System.Windows.Forms.ToolStripMenuItem chkAddAllFiles;
        private System.Windows.Forms.ToolStripMenuItem chkHideUnsupportedCarsWarning;
        private System.Windows.Forms.ToolStripSeparator sepTools1;
        private System.Windows.Forms.ToolStripMenuItem cardIDChangerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chkShowCourseNames;
    }
}
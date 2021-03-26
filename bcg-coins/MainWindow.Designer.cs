namespace bcg_coins {
    partial class MainWindow {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.btnDispHistogram = new System.Windows.Forms.Button();
            this.baseImageBox = new Emgu.CV.UI.ImageBox();
            this.histogramBox = new Emgu.CV.UI.HistogramBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxMetadata = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.binaryImageBox = new Emgu.CV.UI.ImageBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.contoursImageBox = new Emgu.CV.UI.ImageBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuOpen = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.menuSettings = new System.Windows.Forms.MenuItem();
            this.menuResetZoom = new System.Windows.Forms.MenuItem();
            this.menuSettingsInvertBinary = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuCredits = new System.Windows.Forms.MenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.btnBinaryImage = new System.Windows.Forms.Button();
            this.btnFillHoles = new System.Windows.Forms.Button();
            this.btnFindContours = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.baseImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binaryImageBox)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contoursImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDispHistogram
            // 
            this.btnDispHistogram.Location = new System.Drawing.Point(140, 12);
            this.btnDispHistogram.Name = "btnDispHistogram";
            this.btnDispHistogram.Size = new System.Drawing.Size(101, 23);
            this.btnDispHistogram.TabIndex = 0;
            this.btnDispHistogram.Text = "Display Histogram";
            this.btnDispHistogram.UseVisualStyleBackColor = true;
            this.btnDispHistogram.Click += new System.EventHandler(this.btnDispHistogram_Click);
            // 
            // baseImageBox
            // 
            this.baseImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baseImageBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.baseImageBox.Location = new System.Drawing.Point(3, 16);
            this.baseImageBox.Name = "baseImageBox";
            this.baseImageBox.Size = new System.Drawing.Size(350, 200);
            this.baseImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.baseImageBox.TabIndex = 2;
            this.baseImageBox.TabStop = false;
            // 
            // histogramBox
            // 
            this.histogramBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.histogramBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.histogramBox.Location = new System.Drawing.Point(3, 16);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(350, 200);
            this.histogramBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.histogramBox.TabIndex = 2;
            this.histogramBox.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 42);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1114, 470);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.baseImageBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 220);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base Image";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxMetadata);
            this.groupBox2.Location = new System.Drawing.Point(365, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 220);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MetaData";
            // 
            // textBoxMetadata
            // 
            this.textBoxMetadata.AcceptsReturn = true;
            this.textBoxMetadata.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxMetadata.Location = new System.Drawing.Point(3, 16);
            this.textBoxMetadata.Multiline = true;
            this.textBoxMetadata.Name = "textBoxMetadata";
            this.textBoxMetadata.ReadOnly = true;
            this.textBoxMetadata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMetadata.Size = new System.Drawing.Size(350, 200);
            this.textBoxMetadata.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.histogramBox);
            this.groupBox3.Location = new System.Drawing.Point(727, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 220);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Histogram";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.binaryImageBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 220);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Binary Image";
            // 
            // binaryImageBox
            // 
            this.binaryImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.binaryImageBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.binaryImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.binaryImageBox.Location = new System.Drawing.Point(3, 16);
            this.binaryImageBox.MinimumSize = new System.Drawing.Size(200, 200);
            this.binaryImageBox.Name = "binaryImageBox";
            this.binaryImageBox.Size = new System.Drawing.Size(350, 200);
            this.binaryImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.binaryImageBox.TabIndex = 2;
            this.binaryImageBox.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.contoursImageBox);
            this.groupBox5.Location = new System.Drawing.Point(365, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(356, 220);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Contours";
            // 
            // contoursImageBox
            // 
            this.contoursImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contoursImageBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.contoursImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.contoursImageBox.Location = new System.Drawing.Point(3, 16);
            this.contoursImageBox.MinimumSize = new System.Drawing.Size(200, 200);
            this.contoursImageBox.Name = "contoursImageBox";
            this.contoursImageBox.Size = new System.Drawing.Size(350, 200);
            this.contoursImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.contoursImageBox.TabIndex = 2;
            this.contoursImageBox.TabStop = false;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile,
            this.menuSettings,
            this.menuAbout});
            // 
            // menuFile
            // 
            this.menuFile.Index = 0;
            this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOpen,
            this.menuExit});
            this.menuFile.Text = "File";
            // 
            // menuOpen
            // 
            this.menuOpen.Index = 0;
            this.menuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuOpen.Text = "Open new Image";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuExit
            // 
            this.menuExit.Index = 1;
            this.menuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.Index = 1;
            this.menuSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuResetZoom,
            this.menuSettingsInvertBinary});
            this.menuSettings.Text = "Settings";
            // 
            // menuResetZoom
            // 
            this.menuResetZoom.Index = 0;
            this.menuResetZoom.Text = "Reset Zoom";
            this.menuResetZoom.Click += new System.EventHandler(this.menuResetZoom_Click);
            // 
            // menuSettingsInvertBinary
            // 
            this.menuSettingsInvertBinary.Checked = true;
            this.menuSettingsInvertBinary.Index = 1;
            this.menuSettingsInvertBinary.Text = "Invert Binary Image";
            this.menuSettingsInvertBinary.Click += new System.EventHandler(this.menuSettingsInvertBinary_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Index = 2;
            this.menuAbout.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuCredits});
            this.menuAbout.Text = "About";
            // 
            // menuCredits
            // 
            this.menuCredits.Index = 0;
            this.menuCredits.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuCredits.Text = "Credits";
            this.menuCredits.Click += new System.EventHandler(this.menuCredits_Click);
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Location = new System.Drawing.Point(12, 12);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(122, 23);
            this.btnApplyAll.TabIndex = 0;
            this.btnApplyAll.Text = "Apply all Operations";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // btnBinaryImage
            // 
            this.btnBinaryImage.Location = new System.Drawing.Point(247, 12);
            this.btnBinaryImage.Name = "btnBinaryImage";
            this.btnBinaryImage.Size = new System.Drawing.Size(124, 23);
            this.btnBinaryImage.TabIndex = 0;
            this.btnBinaryImage.Text = "Calculate Binary Image";
            this.btnBinaryImage.UseVisualStyleBackColor = true;
            this.btnBinaryImage.Click += new System.EventHandler(this.btnBinaryImage_Click);
            // 
            // btnFillHoles
            // 
            this.btnFillHoles.Location = new System.Drawing.Point(377, 12);
            this.btnFillHoles.Name = "btnFillHoles";
            this.btnFillHoles.Size = new System.Drawing.Size(63, 23);
            this.btnFillHoles.TabIndex = 4;
            this.btnFillHoles.Text = "Fill Holes";
            this.btnFillHoles.UseVisualStyleBackColor = true;
            this.btnFillHoles.Click += new System.EventHandler(this.btnFillHoles_Click);
            // 
            // btnFindContours
            // 
            this.btnFindContours.Location = new System.Drawing.Point(446, 13);
            this.btnFindContours.Name = "btnFindContours";
            this.btnFindContours.Size = new System.Drawing.Size(85, 23);
            this.btnFindContours.TabIndex = 5;
            this.btnFindContours.Text = "Find Contours";
            this.btnFindContours.UseVisualStyleBackColor = true;
            this.btnFindContours.Click += new System.EventHandler(this.btnFindContours_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 524);
            this.Controls.Add(this.btnFindContours);
            this.Controls.Add(this.btnFillHoles);
            this.Controls.Add(this.btnApplyAll);
            this.Controls.Add(this.btnBinaryImage);
            this.Controls.Add(this.btnDispHistogram);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "BCG - Coins";
            ((System.ComponentModel.ISupportInitialize)(this.baseImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.binaryImageBox)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contoursImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDispHistogram;
        private Emgu.CV.UI.ImageBox baseImageBox;
        private Emgu.CV.UI.HistogramBox histogramBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuFile;
        private System.Windows.Forms.MenuItem menuOpen;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuSettings;
        private System.Windows.Forms.MenuItem menuAbout;
        private System.Windows.Forms.MenuItem menuCredits;
        private System.Windows.Forms.TextBox textBoxMetadata;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private Emgu.CV.UI.ImageBox binaryImageBox;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Button btnBinaryImage;
        private System.Windows.Forms.MenuItem menuResetZoom;
        private System.Windows.Forms.Button btnFillHoles;
        private System.Windows.Forms.MenuItem menuSettingsInvertBinary;
        private System.Windows.Forms.GroupBox groupBox5;
        private Emgu.CV.UI.ImageBox contoursImageBox;
        private System.Windows.Forms.Button btnFindContours;
    }
}


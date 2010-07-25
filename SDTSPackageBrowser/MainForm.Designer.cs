namespace STDSPackageBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.File_OpenPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.File_OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.projectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.latLongnoProjectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uTMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectionToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size( 810, 24 );
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size( 35, 20 );
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.File_OpenPackage,
            this.File_OpenFile} );
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size( 152, 22 );
            this.openToolStripMenuItem.Text = "Open";
            // 
            // File_OpenPackage
            // 
            this.File_OpenPackage.Name = "File_OpenPackage";
            this.File_OpenPackage.Size = new System.Drawing.Size( 137, 22 );
            this.File_OpenPackage.Text = "Package...";
            this.File_OpenPackage.Click += new System.EventHandler( this.File_OpenPackage_Click );
            // 
            // File_OpenFile
            // 
            this.File_OpenFile.Name = "File_OpenFile";
            this.File_OpenFile.Size = new System.Drawing.Size( 137, 22 );
            this.File_OpenFile.Text = "File...";
            this.File_OpenFile.Click += new System.EventHandler( this.File_OpenFile_Click );
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add( this.tabPage1 );
            this.mainTabControl.Location = new System.Drawing.Point( 0, 27 );
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size( 810, 495 );
            this.mainTabControl.TabIndex = 1;
            this.mainTabControl.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 802, 469 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // projectionToolStripMenuItem
            // 
            this.projectionToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.latLongnoProjectionToolStripMenuItem,
            this.uTMToolStripMenuItem} );
            this.projectionToolStripMenuItem.Name = "projectionToolStripMenuItem";
            this.projectionToolStripMenuItem.Size = new System.Drawing.Size( 67, 20 );
            this.projectionToolStripMenuItem.Text = "Projection";
            // 
            // latLongnoProjectionToolStripMenuItem
            // 
            this.latLongnoProjectionToolStripMenuItem.Name = "latLongnoProjectionToolStripMenuItem";
            this.latLongnoProjectionToolStripMenuItem.Size = new System.Drawing.Size( 201, 22 );
            this.latLongnoProjectionToolStripMenuItem.Text = "Lat/Long (no projection)";
            this.latLongnoProjectionToolStripMenuItem.Click += new System.EventHandler( this.latLongnoProjectionToolStripMenuItem_Click );
            // 
            // uTMToolStripMenuItem
            // 
            this.uTMToolStripMenuItem.Name = "uTMToolStripMenuItem";
            this.uTMToolStripMenuItem.Size = new System.Drawing.Size( 201, 22 );
            this.uTMToolStripMenuItem.Text = "UTM";
            this.uTMToolStripMenuItem.Click += new System.EventHandler( this.uTMToolStripMenuItem_Click );
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 810, 523 );
            this.Controls.Add( this.mainTabControl );
            this.Controls.Add( this.menuStrip1 );
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SDTS Package Browser";
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem File_OpenPackage;
        private System.Windows.Forms.ToolStripMenuItem File_OpenFile;
        private System.Windows.Forms.ToolStripMenuItem projectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem latLongnoProjectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uTMToolStripMenuItem;
    }
}


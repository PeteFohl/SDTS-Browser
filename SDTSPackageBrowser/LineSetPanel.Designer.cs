namespace STDSPackageBrowser
{
    partial class LineSetPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LineSetPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 9F, 20F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.Name = "LineSetPanel";
            this.Size = new System.Drawing.Size( 929, 614 );
            this.Paint += new System.Windows.Forms.PaintEventHandler( this.LineSetPanel_Paint );
            this.MouseMove += new System.Windows.Forms.MouseEventHandler( this.LineSetPanel_MouseMove );
            this.MouseClick += new System.Windows.Forms.MouseEventHandler( this.LineSetPanel_MouseClick );
            this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.LineSetPanel_MouseDown );
            this.Resize += new System.EventHandler( this.LineSetPanel_Resize );
            this.MouseUp += new System.Windows.Forms.MouseEventHandler( this.LineSetPanel_MouseUp );
            this.ResumeLayout( false );

        }

        #endregion

    }
}

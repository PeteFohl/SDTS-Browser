using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SDTSLib.ISO8211Data;
using SDTSLib.SDTSData;

namespace STDSPackageBrowser
{ 
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            latLongnoProjectionToolStripMenuItem.Checked = false;
            uTMToolStripMenuItem.Checked = true;
        }

        private void LoadCatalogDirectoryPanel( DataDefinitionFile ddf )
        {
            EnsureTabControl();
            TabPage tab = new TabPage( ddf.DescriptiveRecord.Filename );
            CatalogDirectoryPanel panel = new CatalogDirectoryPanel { Dock = DockStyle.Fill };
            panel.OpenSDTSFile += new OpenSDTSFileHandler( panel_OpenSDTSFile );
            panel.LoadDDF( ddf );
            tab.Controls.Add( panel );
            mainTabControl.TabPages.Add( tab );
            mainTabControl.SelectedTab = tab;
        }

        private void panel_OpenSDTSFile( string fileName, string fileType )
        {
            if (!File.Exists(fileName))
                MessageBox.Show( string.Format( "Could not find file {0}.", fileName ) );
            else
                OpenDDFFile( fileName );
        }

        private void LoadLineSetPanelTab( DataDefinitionFile ddf )
        {
            EnsureTabControl();
            TabPage tab = new TabPage( ddf.DescriptiveRecord.Filename );
            LineSetPanel panel = new LineSetPanel { Dock = DockStyle.Fill };
            panel.LoadLineSet( ddf );
            tab.Controls.Add( panel );
            mainTabControl.TabPages.Add( tab );
            mainTabControl.SelectedTab = tab;
        }

        private void LoadGenericPanel( DataDefinitionFile ddf )
        {
            EnsureTabControl();
            TabPage tab = new TabPage( ddf.DescriptiveRecord.Filename );
            var panel = new GenericPanel { Dock = DockStyle.Fill };
            if (panel.LoadDDF( ddf ))
            {
                tab.Controls.Add( panel );
                mainTabControl.TabPages.Add( tab );
                mainTabControl.SelectedTab = tab;
            }
        }

        private void EnsureTabControl()
        {
            if( !mainTabControl.Visible )
            {
                mainTabControl.TabPages.Clear();
                mainTabControl.Visible = true;
            }
        }

        private void File_OpenFile_Click( object sender, EventArgs e )
        {
            var dlg = new OpenFileDialog { CheckFileExists = true, DefaultExt = "DDF" };
            if( dlg.ShowDialog() == DialogResult.OK )
                OpenDDFFile( dlg.FileName );

        }

        private void OpenDDFFile( string fileName )
        {
            try
            {
                var ddf = DataDefinitionFile.FromFile( fileName );

                var tag = ddf.DescriptiveRecord.Directories.ElementAt( 2 ).Tag;
                switch( tag )
                {
                    case DirectoryDataType.CATD:
                        LoadCatalogDirectoryPanel( ddf );
                        break;
                    case DirectoryDataType.LINE:
                        LoadLineSetPanelTab( ddf );
                        break;
                    default:
                        LoadGenericPanel( ddf );
                        break;
                }

            }
            catch( Exception exc )
            {
                System.Diagnostics.Debug.WriteLine( exc.Message );
                MessageBox.Show( "This does not appear to be a supported SDTS file, or it may be corrupt." );
            }
        }

        private void File_OpenPackage_Click( object sender, EventArgs e )
        {
            var dlg = new OpenFileDialog { CheckFileExists = true, DefaultExt = "DDF" };
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    var catd = DataDefinitionFile.FromFile( dlg.FileName );

                    var tag = catd.DescriptiveRecord.Directories.ElementAt( 2 ).Tag;
                    if( tag != DirectoryDataType.CATD )
                    {
                        MessageBox.Show( "This does not appear to be an SDTS Catalog/Directory file." );
                        return;
                    }
                    var package = new CatalogDirectory(catd);

                    EnsureTabControl();
                    TabPage tab = new TabPage( catd.DescriptiveRecord.Filename );
                    LineSetPanel panel = new LineSetPanel { Dock = DockStyle.Fill };
                    panel.LoadPackage( package );
                    panel.UseProjection = uTMToolStripMenuItem.Checked;
                    tab.Controls.Add( panel );
                    mainTabControl.TabPages.Add( tab );
                }
                catch( Exception exc )
                {
                    MessageBox.Show( exc.Message );
                }
            }
        }

        private void latLongnoProjectionToolStripMenuItem_Click( object sender, EventArgs e )
        {
            SetLinesetProjection( false );
            latLongnoProjectionToolStripMenuItem.Checked = true;
            uTMToolStripMenuItem.Checked = false;
        }

        private void uTMToolStripMenuItem_Click( object sender, EventArgs e )
        {
            SetLinesetProjection( true );
            latLongnoProjectionToolStripMenuItem.Checked = false;
            uTMToolStripMenuItem.Checked = true;
        }

        private static LineSetPanel GetLineSetPanel( TabPage tab )
        {
            foreach( var ctl in tab.Controls )
            {
                if( ctl is LineSetPanel )
                    return ctl as LineSetPanel;
            }
            return null;
        }
        private void SetLinesetProjection( bool useProjection )
        {
            foreach( TabPage tab in mainTabControl.TabPages )
            {
                LineSetPanel panel = GetLineSetPanel( tab );
                if( panel != null )
                {
                    if( panel.UseProjection != useProjection )
                    {
                        panel.UseProjection = useProjection;
                        panel.Refresh();
                    }
                }
            }
        }

        public override void Refresh()
        {
            var panel = GetLineSetPanel( mainTabControl.SelectedTab );
            if( panel != null )
                panel.Refresh();
            base.Refresh();
        }
    }
}

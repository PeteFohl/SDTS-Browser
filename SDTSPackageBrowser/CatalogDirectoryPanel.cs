using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDTSLib.ISO8211Data;
using SDTSLib.SDTSData;

namespace STDSPackageBrowser
{
    public class OpenSDTSFileEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
    }
    public delegate void OpenSDTSFileHandler(string fileName, string fileType);

    public partial class CatalogDirectoryPanel : UserControl
    {
        public event OpenSDTSFileHandler OpenSDTSFile;

        private CatalogDirectory _catalog;
        public CatalogDirectoryPanel()
        {
            InitializeComponent();
        }

        public void LoadDDF( DataDefinitionFile ddf )
        {
            _catalog = new CatalogDirectory( ddf );
            foreach( var entry in _catalog.Entries.OrderBy(e => e.Contents))
            {
                ListViewItem item = new ListViewItem();
                item.Text = entry.FileName;
                item.SubItems.Add( new ListViewItem.ListViewSubItem( item, entry.Contents ) );
                listView1.Items.Add( item );                
            }
        }

        private void listView1_DoubleClick( object sender, EventArgs args )
        {
            if( listView1.SelectedItems.Count > 0 )
            {
                var listItem = listView1.SelectedItems[ 0 ];
                if (OpenSDTSFile != null)
                    {
                        var entry = _catalog.Entries.Find( e => e.FileName == listItem.Text );
                        OpenSDTSFile( entry.FileName, entry.Type );
                    }
            }
        }
    }
}

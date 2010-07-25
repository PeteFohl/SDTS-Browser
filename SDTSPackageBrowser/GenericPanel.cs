using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDTSLib.ISO8211Data;

namespace STDSPackageBrowser
{
    public partial class GenericPanel : UserControl
    {
        public GenericPanel()
        {
            InitializeComponent();
        }
        public bool LoadDDF( DataDefinitionFile ddf )
        {
            listView1.Columns.Clear();
            if (ddf.DescriptiveRecord.Directories.OfType<DDRDirectoryEntry>().Any(dir => dir.Type == DDRDirectoryEntryType.ArrayFieldList))
            {
                MessageBox.Show( "This file contains multiple value fields, which are not supported in generic mode at this time." );
                return false;
            }

            IEnumerable<DDRDirectoryEntry> directories = ddf.DescriptiveRecord.Directories.OfType<DDRDirectoryEntry>().Where( dir => dir.Type == DDRDirectoryEntryType.FieldList );
            IEnumerable<DataField> colDefs = directories.SelectMany( dir => dir.SubFields );
            foreach (var col in colDefs)
            {
                listView1.Columns.Add( new ColumnHeader { Name = col.Name, Text = col.Name } );            	
            }
            foreach( var row in ddf.DataRecords.SelectMany(dr => dr.Rows) )
            {
                listView1.Items.Add( new ListViewItem( row.Fields.Select( field => field.AsString ).ToArray() ) );
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDTSLib.SDTSData;
using SDTSLib.ISO8211Data;

namespace STDSPackageBrowser
{
    public partial class AttributeDisplayer : UserControl
    {
        public AttributeDisplayer(Line line, CatalogDirectory package)
        {
            InitializeComponent();

            foreach( var attribute in line.AttributeIDs)
            {
                var attributeFile = package.Entries.FirstOrDefault( entry => entry.Type == attribute.Key );
                if (attributeFile != null)
                {
                    var rows = attributeFile.File.DataRecords
                        .SelectMany( dr => dr.Rows )
                        .Where( row => row.Fields.ElementAt( 0 ).AsString == attribute.Key && row.Fields.ElementAt( 1 ).AsInt == attribute.Value );
                    foreach( var attrRow in rows)
                    {
                        listView1.Items.Add( new ListViewItem( new string[] { attribute.Key, attrRow.Fields.ElementAt(2).AsString } ) );
                    }
                }
            }
        }
    }
}

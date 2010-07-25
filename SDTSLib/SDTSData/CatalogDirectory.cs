using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDTSLib.ISO8211Data;
using Projection;

namespace SDTSLib.SDTSData
{
    public class CatalogDirectoryEntry
    {
        private DataDefinitionFile _file;
        public DataDefinitionFile File 
        {
            get 
            {
                if (_file == null)
                    _file = DataDefinitionFile.FromFile( FileName );
                return _file;
            }
        }
        public readonly string FileName;
        public readonly string Contents;
        public readonly string Type;

        public CatalogDirectoryEntry( DataRecordRow row )
        {
            FileName = row.Fields.ElementAt( 4 ).AsString;
            Contents = row.Fields.ElementAt( 3 ).AsString;
            Type = row.Fields.ElementAt( 2 ).AsString;
        }
    }

    public class CatalogDirectory
    {
        public IProjection Projection { get; private set; }
        public List<CatalogDirectoryEntry> Entries { get; private set; }

        public CatalogDirectory( DataDefinitionFile ddf )
        {
            if( ddf.DescriptiveRecord.Directories.Last().Tag == DirectoryDataType.CATD )
            {
                Entries = ddf.DataRecords[ 0 ].Rows.Select( row => new CatalogDirectoryEntry( row ) ).ToList();
                var xref = Entries.First( entry => entry.Type == "XREF" );
                var directory = xref.File.DescriptiveRecord.Directories.OfType<DDRDirectoryEntry>().First( dir => dir.Type == DDRDirectoryEntryType.FieldList );
                var projCol = directory.SubFields.IndexOf( directory.SubFields.First( field => field.Name == "RSNM" ) );
                var projection = xref.File.DataRecords[ 0 ].Rows[ 0 ].Fields[projCol].AsString;
                if( projection.ToUpper() == "UTM" )
                {
                    var zoneCol = directory.SubFields.IndexOf( directory.SubFields.First( field => field.Name == "ZONE" ) );
                    Projection = new UTM( int.Parse(xref.File.DataRecords[ 0 ].Rows[ 0 ].Fields[ zoneCol ].AsString) );
                }
            }
        }
    }
}

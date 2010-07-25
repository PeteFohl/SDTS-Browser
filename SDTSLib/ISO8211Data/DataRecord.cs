using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public class DataRecord : Record
    {
        public DataDescriptiveRecord DescriptiveRecord { get; set; }
        public List<DataRecordRow> Rows { get; private set; }

        public static DataRecord FromStream( DataDescriptiveRecord ddr, BinaryReader reader )
        {
            var record = new DataRecord { Leader = DataRecordLeader.FromStream( reader ), DescriptiveRecord = ddr };

            record.LoadDirectoriesFromStream( reader );

            record.LoadRows( reader );

            return record;
        }

        private void LoadRows( BinaryReader reader )
        {
            Rows = new List<DataRecordRow>();

            if( Leader.LeaderType == LeaderType.DataRecord )
            {
                Rows.Add( LoadRow( GetRowData( reader ) ) );
            }
            else
            {
                while( true )
                {
                    List<KeyValuePair<DDRDirectoryEntry, byte[]>> rowData = GetRowData( reader );
                    if( rowData.Count == 0 )
                        break;
                    Rows.Add( LoadRow( rowData ) );
                }
            }
        }

        private List<KeyValuePair<DDRDirectoryEntry, byte[]>> GetRowData( BinaryReader reader )
        {
            var rowData = new List<KeyValuePair<DDRDirectoryEntry, byte[]>>();
            foreach( DirectoryEntry entry in Directories.OrderBy( dir => dir.Position ) )
            {
                int peek = reader.PeekChar();
                if( peek == INT_RecordSeparator || peek == INT_EOF )
                    break;
                var fieldDescDir = DescriptiveRecord.Directories.OfType<DDRDirectoryEntry>().FirstOrDefault( ddrEntry => ddrEntry.Fields.First() == entry.Tag.GetDescription() );

                byte[] block;
                if( fieldDescDir.SubFields != null && fieldDescDir.SubFields.Any( field => field.Type == FieldType.Binary ) )
                    block = reader.ReadBytes( entry.Length - 1 );
                else
                    block = ReadUntilSeparator( reader );
                reader.Read();

                if( fieldDescDir.Type == DDRDirectoryEntryType.ArrayFieldList )
                {
                    int dataLength = fieldDescDir.SubFields.Sum( field => field.Length.Value );
                    for( int i = 0; i < block.Length / dataLength; i++ )
                    {
                        byte[] subBlock = new byte[ dataLength ];
                        Array.Copy( block, i * dataLength, subBlock, 0, dataLength );
                        rowData.Add( new KeyValuePair<DDRDirectoryEntry, byte[]>( fieldDescDir, subBlock ) );
                    }
                }
                else
                {
                    rowData.Add( new KeyValuePair<DDRDirectoryEntry, byte[]>( fieldDescDir, block ) );
                }
            }
            return rowData;
        }

        private DataRecordRow LoadRow( List<KeyValuePair<DDRDirectoryEntry, byte[]>> rowData )
        {
            DataRecordRow row = new DataRecordRow();

            row.ID = int.Parse( new string( rowData[ 0 ].Value.AsCharArray() ) );

            row.Fields = new List<DataRecordField>();
            for( int dataIndex = 1; dataIndex < rowData.Count; dataIndex++ )
            {
                var fieldDescDir = DescriptiveRecord.Directories.OfType<DDRDirectoryEntry>().FirstOrDefault( entry => entry.Fields.First() == rowData[ dataIndex ].Key.Tag.GetDescription() );
                if( fieldDescDir == null )
                    throw new InvalidDataException( String.Format( "Could not find directory for data block {0}", rowData[ dataIndex ].Key.Tag ) );
                if( fieldDescDir.SubFields[ 0 ].Length.HasValue )
                {
                    int currentIndex = 0;
                    for( int fieldIndex = 0; fieldIndex < fieldDescDir.SubFields.Count; fieldIndex++ )
                    {
                        byte[] field = new byte[fieldDescDir.SubFields[fieldIndex].Length.Value];
                        Array.Copy( rowData[ dataIndex ].Value, currentIndex, field, 0, field.Length );
                        row.Fields.Add( new DataRecordField( fieldDescDir.SubFields[ fieldIndex ], field ) );
                        currentIndex += fieldDescDir.SubFields[ fieldIndex ].Length.Value;
                    }
                }
                else
                {
                    var fields = rowData[ dataIndex ].Value.SplitOnSeparator( INT_FieldSeparator );
                    if( fields.Count != fieldDescDir.SubFields.Count )
                        throw new InvalidDataException( "The number of fields does not match expected" );
                    for( int fieldIndex = 0; fieldIndex < fields.Count; fieldIndex++ )
                    {
                        row.Fields.Add( new DataRecordField( fieldDescDir.SubFields[ fieldIndex ], fields[ fieldIndex ] ) );
                    }
                }
            }
            return row;
        }

        private static byte[] ReadUntilSeparator( BinaryReader reader )
        {
            var bytes = new List<byte>();

            int peek = reader.PeekChar();
            while( peek != INT_RecordSeparator && peek != INT_EOF)
            {
                bytes.Add( reader.ReadByte() );
                peek = reader.PeekChar();
            }

            return bytes.ToArray();
        }
        protected override DirectoryEntry CreateDirectoryObject()
        {
            return new DirectoryEntry();
        }
    }
}

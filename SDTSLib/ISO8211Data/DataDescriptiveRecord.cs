using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public class DataDescriptiveRecord : Record
    {
        public string Filename { get; private set; }

        public static DataDescriptiveRecord FromStream( BinaryReader reader )
        {
            DataDescriptiveRecord ddr = new DataDescriptiveRecord { Leader = DataDescriptiveRecordLeader.FromStream( reader ) };
            ddr.LoadDirectoriesFromStream( reader );

            foreach( DDRDirectoryEntry directory in ddr.Directories.OfType<DDRDirectoryEntry>() )
            {
                char[] buffer = reader.ReadChars(directory.Length);
                directory.Type = TypeFromTag( new string( buffer, 0, 4 ) );
                directory.FieldTerminatorChar = buffer[ 4 ];
                directory.UnitTerminatorChar = buffer[ 5 ];
                string fields = new string( buffer, 6, directory.Length - 7 );
                directory.Fields = fields.Split( new char[] { (char)INT_FieldSeparator } ).ToList();
                switch( directory.Type )
                {
                    case DDRDirectoryEntryType.FileName:
                        ddr.Filename = directory.Fields.First();
                        break;
                    case DDRDirectoryEntryType.RecordIdentifier:
                        break;
                    case DDRDirectoryEntryType.FieldList:
                        directory.SubFields = directory.Fields[1].Split( '!' ).Select( str => new DataField { Name = str, Directory = directory } ).ToList();
                        ApplyFieldTypes( directory );
                        break;
                    case DDRDirectoryEntryType.ArrayFieldList:
                        directory.SubFields = directory.Fields[1].Substring( 1 ).Split( '!' ).Select( str => new DataField { Name = str, Directory = directory } ).ToList();
                        ApplyFieldTypes( directory );
                        break;
                }
            }
            return ddr;
        }

        private static void ApplyFieldTypes( DDRDirectoryEntry directory )
        {
            string types = directory.Fields.ElementAt( 2 );
            while( types[ 0 ] == '(' )
                types = types.Substring( 1, types.Length - 2 );

            string[] splitTypes = types.Split( new char[] { ',' } );
            List<FieldType> fieldTypes = new List<FieldType>();
            List<int?> fieldLengths = new List<int?>();

            foreach( string splitType in splitTypes )
            {
                int stringIndex = 0;
                int typeCount = 0;
                while( stringIndex < splitType.Length && splitType[ stringIndex ] >= '0' && splitType[ stringIndex ] <= '9' )
                {
                    typeCount = (typeCount * 10) + (splitType[ stringIndex ] - '0');
                    stringIndex++;
                }
                if( typeCount == 0 )
                    typeCount = 1;

                FieldType fieldType;
                switch( splitType[ stringIndex ] )
                {
                    case 'A':
                        fieldType = FieldType.ASCII;
                        break;
                    case 'I':
                        fieldType = FieldType.Integer;
                        break;
                    case 'R':
                        fieldType = FieldType.Real;
                        break;
                    case 'B':
                        fieldType = FieldType.Binary;
                        break;
                    default:
                        throw new Exception( "Unknown field type" );
                }
                int? fieldLength = null;
                if( splitType.Length > stringIndex + 3 )
                {
                    fieldLength = int.Parse( splitType.Substring( stringIndex + 2, splitType.Length - stringIndex - 3 ) );
                    if( fieldType == FieldType.Binary )
                        fieldLength = fieldLength / 8;
                }
                for( int i = 0; i < typeCount; i++ )
                {
                    fieldTypes.Add( fieldType );
                    fieldLengths.Add( fieldLength );
                }
            }

            if( fieldTypes.Count != directory.SubFields.Count )
                throw new Exception( "Incorrect number of field types" );

            for( int i = 0; i < fieldTypes.Count; i++ )
            {
                directory.SubFields[i].Type = fieldTypes[ i ];
                directory.SubFields[ i ].Length = fieldLengths[ i ];
            }
        }

        private static DDRDirectoryEntryType TypeFromTag( string tag )
        {
            DDRDirectoryEntryType entryType;

            switch( tag )
            {
                case "0000":
                    entryType = DDRDirectoryEntryType.FileName;
                    break;
                case "0100":
                    entryType = DDRDirectoryEntryType.RecordIdentifier;
                    break;
                case "1600":
                    entryType = DDRDirectoryEntryType.FieldList;
                    break;
                case "2600":
                    entryType = DDRDirectoryEntryType.ArrayFieldList;
                    break;
                default:
                    throw new NotImplementedException( String.Format( "Unknown ISO tag {0}", tag ) );
            }

            return entryType;
        }

        protected override DirectoryEntry CreateDirectoryObject()
        {
            return new DDRDirectoryEntry();
        }
    }
}

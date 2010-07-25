using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public abstract class Record
    {
        protected const int INT_RecordSeparator = 30;
        protected const int INT_FieldSeparator = 31;
        protected const int INT_EOF = -1;

        public List<DirectoryEntry> Directories { get; private set; }

        public Leader Leader { get; set; }

        protected void LoadDirectoriesFromStream( BinaryReader reader )
        {
            Directories = new List<DirectoryEntry>();

            while( reader.PeekChar() != INT_RecordSeparator )
            {
                var entry = CreateDirectoryObject();
                char[] buffer = reader.ReadChars( Leader.SizeFieldTag );
                string tag = new string( buffer );
                if( tag == "0000" )
                    entry.Tag = DirectoryDataType.Filename;
                else if( tag == "0001" )
                    entry.Tag = DirectoryDataType.DDFRecordIdentifier;
                else
                    entry.Tag = (DirectoryDataType)Enum.Parse( typeof( DirectoryDataType ), tag );
                if( entry.Tag == DirectoryDataType.Unknown )
                    throw new InvalidDataException( String.Format("Unknown tag {0}", tag) );
                buffer = reader.ReadChars( Leader.SizeFieldLength );
                entry.Length = int.Parse( new string( buffer ) );
                buffer = reader.ReadChars( Leader.SizeFieldPos );
                entry.Position = int.Parse( new string( buffer ) );
                Directories.Add( entry );
            }
            reader.Read();
        }

        protected abstract DirectoryEntry CreateDirectoryObject();        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SDTSLib.ISO8211Data;

namespace SDTSConsoleReader
{
    class Program
    {
        static void Main( string[] args )
        {
            if( args.Length == 0 )
            {
                Console.Write( "Filename is required" );
                return;
            }

            if( !File.Exists( args[ 0 ] ) )
            {
                Console.Write( "File not found" );
                return;
            }

            var ddf = DataDefinitionFile.FromFile( args[ 0 ] );
            foreach( DataRecord record in ddf.DataRecords)
            {
                foreach( DataRecordRow row in record.Rows )
                {
                    for( int i = 0; i < row.Fields.Count; i++ )
                    {
                        Console.Write( string.Format( "{0}", row.Fields[ i ].AsString ) );
                        if( i < row.Fields.Count - 1 )
                            Console.Write( "," );
                    }
                    Console.WriteLine( "" );
                }
            }
        }
    }
}

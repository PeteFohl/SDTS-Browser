using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public class DataDefinitionFile
    {
        public DataDescriptiveRecord DescriptiveRecord { get; private set; }
        public List<DataRecord> DataRecords { get; private set; }

        public static DataDefinitionFile FromFile( string path )
        {
            var ddf = new DataDefinitionFile();
            using( FileStream stream = new FileStream( path, FileMode.Open ) )
            {                
                using( var reader = new BinaryReader( stream ) )
                {
                    ddf.DescriptiveRecord = DataDescriptiveRecord.FromStream( reader );

                    ddf.DataRecords = new List<DataRecord>();
                    while( reader.PeekChar() != -1)
                    {
                        ddf.DataRecords.Add( DataRecord.FromStream( ddf.DescriptiveRecord, reader ) );
                    }
                }
                stream.Close();
            }
            return ddf;
        }
    }
}

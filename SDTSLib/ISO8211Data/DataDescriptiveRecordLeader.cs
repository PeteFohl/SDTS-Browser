using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public class DataDescriptiveRecordLeader : Leader
    {
        public char InterchangeLevel { get; set; }
        public char InlineCodeExtenstionIndicator { get; set; }
        public char VersionNumber { get; set; }
        public char AppIndicator { get; set; }
        public int FieldControlLength { get; set; }

        public static DataDescriptiveRecordLeader FromStream( BinaryReader reader )
        {
            char[] leaderData = reader.ReadChars( 24 );

            var leader = new DataDescriptiveRecordLeader();

            leader.RecordLength = int.Parse( new string( leaderData, 0, 5 ) );
            leader.InterchangeLevel = leaderData[ 5 ];
            leader.LeaderType = TypeFromChar(leaderData[ 6 ]);
            leader.InlineCodeExtenstionIndicator = leaderData[ 7 ];
            leader.VersionNumber = leaderData[ 8 ];
            leader.AppIndicator = leaderData[ 9 ];
            leader.FieldControlLength = int.Parse( new string( leaderData, 10, 2 ) );
            LoadFieldArea( leaderData, leader );
            
            return leader;
        }
    }
}

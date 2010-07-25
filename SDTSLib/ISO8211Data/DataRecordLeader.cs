using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public class DataRecordLeader : Leader
    {
        public static DataRecordLeader FromStream( BinaryReader reader )
        {
            char[] leaderData = reader.ReadChars(24);

            var leader = new DataRecordLeader();

            leader.RecordLength = int.Parse( new string( leaderData, 0, 5 ) );
            leader.LeaderType = TypeFromChar( leaderData[ 6 ] );
            
            LoadFieldArea( leaderData, leader );

            return leader;
        }
    }
}

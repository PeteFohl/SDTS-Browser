using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDTSLib.ISO8211Data
{
    public enum LeaderType { DataDescriptiveRecord, DataRecord, RepeatingDataRecord };
    public abstract class Leader
    {
        public int RecordLength { get; set; }
        public LeaderType LeaderType { get; set; }
        public int FieldAreaStart { get; set; }
        public int SizeFieldLength { get; set; }
        public int SizeFieldPos { get; set; }
        public int SizeFieldTag { get; set; }

        protected static void LoadFieldArea( char[] leaderData, Leader leader )
        {
            leader.FieldAreaStart = int.Parse( new string( leaderData, 12, 5 ) );
            leader.SizeFieldLength = int.Parse( new string( leaderData, 20, 1 ) );
            leader.SizeFieldPos = int.Parse( new string( leaderData, 21, 1 ) );
            leader.SizeFieldTag = int.Parse( new string( leaderData, 23, 1 ) );
        }

        protected static LeaderType TypeFromChar( char c )
        {
            switch( c )
            {
                case 'L':
                    return LeaderType.DataDescriptiveRecord;
                case 'D':
                    return LeaderType.DataRecord;
                case 'R':
                    return LeaderType.RepeatingDataRecord;
            }
            throw new InvalidOperationException("Unknown leader type");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDTSLib.ISO8211Data
{
    public class DirectoryEntry
    {
        public DirectoryDataType Tag { get; set; }
        public int Length { get; set; }
        public int Position { get; set; }
    }
}
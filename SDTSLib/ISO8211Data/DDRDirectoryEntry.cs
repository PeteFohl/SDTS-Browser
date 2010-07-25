using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SDTSLib.ISO8211Data
{
    public enum DDRDirectoryEntryType { FileName, RecordIdentifier, FieldList, ArrayFieldList };

    public class DDRDirectoryEntry : DirectoryEntry
    {
        public DDRDirectoryEntryType Type { get; set; }
        public char FieldTerminatorChar { get; set; }
        public char UnitTerminatorChar { get; set; }
        public List<string> Fields { get; internal set; }
        public List<DataField> SubFields { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDTSLib.ISO8211Data
{
    public enum FieldType { ASCII, Integer, Real, Binary };
    public class DataField
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public int? Length { get; set; }
        public DDRDirectoryEntry Directory { get; set; }
    }
}

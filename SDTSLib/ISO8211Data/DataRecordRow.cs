using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDTSLib.ISO8211Data
{
    public class DataRecordRow
    {
        public int? ID { get; set; }
        public List<DataRecordField> Fields { get; set; }
    }
}

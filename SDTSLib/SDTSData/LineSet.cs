using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDTSLib.ISO8211Data;
using System.Drawing;
using Projection;

namespace SDTSLib.SDTSData
{
    public class Line
    {
        public int? ID { get; private set; }
        public List<PointD> Points { get; private set; }
        public DataRecordRow DataRow { get; private set; }
        public int? PolygonIDLeft { get; private set; }
        public int? PolygonIDRight { get; private set; }
        public int? StartNodeID { get; private set; }
        public int? EndNodeID { get; private set; }
        public List<KeyValuePair<string,int>> AttributeIDs { get; private set; }

        public Line()
        {
            Points = new List<PointD>();
        }

        public Line(DataRecordRow row) : this()
        {
            ID = LoadAttributeID( row.Fields, DirectoryDataType.LINE );
            PolygonIDLeft = LoadAttributeID( row.Fields, DirectoryDataType.PIDL );
            PolygonIDRight = LoadAttributeID( row.Fields, DirectoryDataType.PIDR );
            StartNodeID = LoadAttributeID( row.Fields, DirectoryDataType.SNID );
            EndNodeID = LoadAttributeID( row.Fields, DirectoryDataType.ENID );

            AttributeIDs = new List<KeyValuePair<string, int>>();
            var attr = row.Fields.Where( field => field.FieldDefinition.Directory.Tag == DirectoryDataType.ATID );
            if( attr.Any() )
            {
                for( int i = 0; i < attr.Count(); i += 2 )
                    AttributeIDs.Add( new KeyValuePair<string,int>(attr.ElementAt( i ).AsString, attr.ElementAt( i + 1 ).AsInt) );
            }

            var firstX = row.Fields.First( field => field.FieldDefinition.Name == "X" );
            var startIndex = row.Fields.IndexOf( firstX );
            for( int i = startIndex; i < row.Fields.Count; i += 2 )
            {
                var point = new PointD { X = row.Fields[ i ].AsInt / 100.0, Y = row.Fields[ i + 1 ].AsInt / 100.0 };
                Points.Add( point );
            }

            DataRow = row;
        }

        private int? LoadAttributeID( IEnumerable<DataRecordField> fields, DirectoryDataType attribute )
        {
            fields = fields.Where( field => field.FieldDefinition.Directory.Tag == attribute );
            DataRecordField idField = null;
            if( fields.Any() )
                idField = fields.First( field => field.FieldDefinition.Name == "RCID" );
            if( idField != null )
                return idField.AsInt;
            return null;
        }
    }

    public class LineSet
    {
        public List<Line> Lines { get; private set; }

        public LineSet( DataDefinitionFile ddf )
        {
            Lines = new List<Line>();
            foreach( var record in ddf.DataRecords)
            {
                foreach (var row in record.Rows)
                {
                    Lines.Add( new Line( row ) );
                }
            }
        }
    }
}

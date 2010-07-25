using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDTSLib.ISO8211Data
{
    public class DataRecordField
    {
        public DataField FieldDefinition { get; private set; }
        public object Value { get; private set; }

        public DataRecordField( DataField definition, byte[] value )
        {
            FieldDefinition = definition;
            switch( definition.Type )
            {
                case FieldType.ASCII:
                    Value = value.AsString().Trim();
                    break;
                case FieldType.Integer:
                    Value = int.Parse( value.AsString() );
                    break;
                case FieldType.Real:
                    Value = decimal.Parse( value.AsString() );
                    break;
                case FieldType.Binary:
                    Value = ConvertBytesToBinary( value );
                    break;
                default:
                    throw new InvalidOperationException( "Unknown type" );
            }
        }


        private object ConvertBytesToBinary( byte[] bytes )
        {
            if( BitConverter.IsLittleEndian )
                Array.Reverse( bytes );

            if( bytes.Length == 2 )
                return BitConverter.ToInt16( bytes, 0 );
            else if( bytes.Length == 4 )
                return BitConverter.ToInt32( bytes, 0 );
            else if( bytes.Length == 8 )
                return BitConverter.ToInt64( bytes, 0 );
            throw new InvalidOperationException( String.Format( "Binary data must be 16, 32 or 64 bits, input was {0} bits", bytes.Length * 8 ) );
        }

        public int AsInt
        {
            get
            {
                if( FieldDefinition.Type != FieldType.Integer && FieldDefinition.Type != FieldType.Binary )
                    throw new InvalidOperationException( "Field is not of type Integer" );
                return (int)Value;
            }
        }

        public string AsString
        {
            get
            {
                return Value.ToString();
            }
        }
    }
}

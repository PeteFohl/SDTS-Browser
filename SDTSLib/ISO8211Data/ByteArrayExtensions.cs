using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDTSLib.ISO8211Data
{
    public static class ByteArrayExtensions
    {
        public static char[] AsCharArray( this byte[] bytes )
        {
            return bytes.GetCharBlock( 0, bytes.Length );
        }

        public static char[] GetCharBlock( this byte[] bytes, int startIndex, int length )
        {
            char[] chars = new char[ length ];
            for( int i = 0; i < length; i++ )
            {
                chars[ i ] = (char)bytes[ i + startIndex ];
            }
            return chars;
        }

        public static string AsString( this byte[] bytes )
        {
            return new string( bytes.AsCharArray() );
        }

        public static List<byte[]> SplitOnSeparator( this byte[] bytes, int splitSeparator )
        {
            var splits = new List<byte[]>();
            var curBlock = new List<byte>();
            for( int i = 0; i < bytes.Length; i++ )
            {
                if( bytes[ i ] == splitSeparator )
                {
                    splits.Add( curBlock.ToArray() );
                    curBlock.Clear();
                }
                else
                {
                    curBlock.Add( bytes[ i ] );
                }
            }
            if( curBlock.Count > 0 )
                splits.Add( curBlock.ToArray() );
            return splits;
        }

    }
}

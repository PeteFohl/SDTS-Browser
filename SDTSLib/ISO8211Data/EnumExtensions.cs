using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace SDTSLib.ISO8211Data
{
    public static class EnumExtensions
    {
        public static string GetDescription( this Enum en )
        {
            MemberInfo[] info = en.GetType().GetMember( en.ToString() );
            if( info != null && info.Length > 0 )
            {
                object[] attrs = info[ 0 ].GetCustomAttributes( typeof( DescriptionAttribute ), false );
                if( attrs != null && attrs.Length > 0 )
                    return ((DescriptionAttribute)attrs[ 0 ]).Description;
            }
            return en.ToString();
        }
    }
}

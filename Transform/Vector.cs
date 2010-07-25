using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Projection;

namespace Transform
{
    public class Vector
    {
        public double XMagnitude { get; private set; }
        public double YMagnitude { get; private set; }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt( (XMagnitude * XMagnitude) + (YMagnitude * YMagnitude) );
            }
        }
        public Vector( PointD a, PointD b )
        {
            XMagnitude = b.X - a.X;
            YMagnitude = b.Y - a.Y;
        }

        public static double operator *( Vector v1, Vector v2 )
        {
            return v1.XMagnitude * v2.XMagnitude + v1.YMagnitude * v2.YMagnitude;
        }

        public static double operator ^( Vector v1, Vector v2 )
        {
            return v1.XMagnitude * v2.YMagnitude - v1.YMagnitude * v2.XMagnitude;
        }
    }
}

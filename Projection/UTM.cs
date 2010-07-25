using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Projection.Ellipsoids;

namespace Projection
{
    public class UTM : IProjection
    {
        public int Zone { get; private set; }
        private Ellipsoid _ellipsoid;

        private double _k0 = 0.9996f;
        private double _M0 = 0f;
        private double _falseEasting = 500000;

        public UTM( int zone )
        {
            Zone = zone;
            _ellipsoid = new Clarke1866();
        }

        // Equation numbers reference "Map Projections - A Working Manual", John P. Snyder, 1987 USGS
        public PointD ToProjection( PointD latLong )
        {
            var lambda = latLong.X * Math.PI / 180;
            var lambda0 = GetLambda0();
            var phi = latLong.Y * Math.PI / 180;
            var eprime2 = GetEprime2();
            var N = _ellipsoid.a / Math.Sqrt( 1 - _ellipsoid.e2 * Square(Math.Sin(phi)) ); // (4-20)
            var T = Square(Math.Tan(phi)); // (8-13)
            var C = eprime2 * Square(Math.Cos(phi)); // (8-14)
            var A = Math.Cos( phi ) * (lambda - lambda0); // (8-15)
            var M = GetM( phi );
            var projectedPoint = new PointD
            {
                X = _k0 * N * (A + ((1 - T + C) * Third(A) / 6) + ((5 - (18 * T) + (72 * C) - (58 * eprime2)) * Fifth(A) / 120)) + _falseEasting, // (8-9)
                Y = _k0 * (M - _M0 + N * Math.Tan( phi ) * (Square(A) / 2 + (5 - T + 9 * C + 4 * Square(C)) * Fourth(A) / 24
                    + (61 - 58 * T + Square(T) + 600 * C - 330 * eprime2) * Sixth(A) / 720)) // (8-10)
            };
            return projectedPoint;
        }

        public PointD FromProjection( PointD projPoint )
        {
            var M = _M0 + projPoint.Y / _k0; // (8-20)
            var mu = M / (_ellipsoid.a * (1 - _ellipsoid.e2 / 4 - 3 * Square(_ellipsoid.e2) / 64 - 5 * Third(_ellipsoid.e2) / 256)); // (7-19)
            var e1 = (1 - Math.Sqrt( 1 - _ellipsoid.e2 )) / (1 + Math.Sqrt( 1 - _ellipsoid.e2 )); // (3-24)
            var phi1 = mu + (3 * e1 / 2 - 27 * Third(e1) / 32) * Math.Sin( 2 * mu ) + (21 * Square(e1) / 16 - 55 * Fourth(e1) / 32) * Math.Sin( 4 * mu )
                + 151 * Third(e1) / 96 * Math.Sin( 6 * mu ) + 1097 * Fourth(e1) / 512 * Math.Sin( 8 * mu ); // (3-26)

            var eprime2 = GetEprime2();
            var C1 = eprime2 * Square(Math.Cos(phi1)); // (8-21)
            var T1 = Square(Math.Tan(phi1)); // (8-22)
            var N1 = _ellipsoid.a / Math.Sqrt( 1 - _ellipsoid.e2 * Square(Math.Sin(phi1)) ); // (8-23)
            var R1 = _ellipsoid.a * (1 - _ellipsoid.e2) / Math.Pow( 1 - _ellipsoid.e2 * Square(Math.Sin(phi1)), 1.5 ); // (8-24)
            var D = (projPoint.X - _falseEasting) / N1 / _k0; // (8-25)

            var latLongPoint = new PointD
            {
                X = GetLambda0() * 180 / Math.PI + (D - (1 + 2 * T1 + C1) * Third(D) / 6 +
                    (5 - 2 * C1 + 28 * T1 - 3 * Square(C1) + 8 * eprime2 + 24 * Square(T1)) * Fifth(D) / 120) / Math.Cos( phi1 ) * 180 / Math.PI, // (8-17)
                Y = phi1 * 180 / Math.PI - (N1 * Math.Tan( phi1 ) / R1) * (Square(D) / 2 - (5 + 3 * T1 + 10 * C1 - 4 * Square(C1) - 9 * eprime2) * Fourth(D) / 24
                    + (61 + 90 * T1 + 298 * C1 + 45 * Square(T1) - 252 * eprime2 - 3 * Square(C1)) * Sixth(D) / 720) * 180 / Math.PI// (8-18)
            };
            return latLongPoint;
        }

        private static double GetM( double phi )
        {
            return 111132.0894 * phi * 180 / Math.PI - 16216.94 * Math.Sin( 2 * phi ) + 17.21 * Math.Sin( 4 * phi ) - 0.02 * Math.Sin( 6 * phi ); // (3-22)
        }
        private double GetEprime2()
        {
            return _ellipsoid.e2 / (1 - _ellipsoid.e2); // (8-12)
        }
        private double GetLambda0()
        {
            return (-180 + (Zone - .5) * 6) * Math.PI / 180;
        }

        // These are for speed
        private static double Square( double d )
        {
            return d * d;
        }

        private static double Third( double d )
        {
            return d * d * d;
        }

        private static double Fourth( double d )
        {
            return d * d * d * d;
        }

        private static double Fifth( double d )
        {
            return d * d * d * d * d;
        }

        private static double Sixth( double d )
        {
            return d * d * d * d * d * d;
        }

    }
}

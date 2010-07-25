using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projection.Ellipsoids
{
    public abstract class Ellipsoid
    {
        public abstract double a { get; }
        public abstract double b { get; }
        public abstract double f { get; }

        public double e2
        {
            get
            {
                return 1 - (b * b) / (a * a);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projection.Ellipsoids
{
    public class Clarke1866 : Ellipsoid
    {
        public override double a
        {
            get { return 6378206.4; }
        }

        public override double b
        {
            get { return 6356583.8; }
        }

        public override double f
        {
            get { return 1 / 294.98; }
        }
    }
}

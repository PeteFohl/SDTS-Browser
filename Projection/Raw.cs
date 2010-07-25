using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projection
{
    public class Raw : IProjection
    {
        public PointD ToProjection( PointD latLong )
        {
           return latLong;
        }

        public PointD FromProjection( PointD projPoint )
        {
            return projPoint;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projection
{
    public interface IProjection
    {
        PointD ToProjection(PointD latLong);
        PointD FromProjection( PointD projPoint );
    }
}

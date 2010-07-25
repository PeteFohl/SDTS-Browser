using System;
using NUnit.Framework;
using Projection;
using System.Drawing;

namespace UnitTests.ProjectionTests
{
    public class UTMTests
    {
        [Test]
        public void Can_transform_point()
        {
            var projection = new UTM( 18 );
            var projectedPoint = projection.ToProjection( new PointD( -73.5,40.5 ) );
            Assert.That( Math.Round(projectedPoint.X,1), Is.EqualTo( 127106.5 + 500000 ) );
            Assert.That( Math.Round(projectedPoint.Y,1), Is.EqualTo( 4484124.4 ) );
        }

        [Test]
        public void Can_inverse_transform()
        {
            var projection = new UTM( 18 );
            var latLongPoint = projection.FromProjection( new PointD( 127106.5 + 500000,4484124.4 ) );
            Assert.That( Math.Round(latLongPoint.X,6), Is.EqualTo( -73.5 ) );
            Assert.That( Math.Round(latLongPoint.Y,6), Is.EqualTo( 40.5 ) );
        }
    }
}

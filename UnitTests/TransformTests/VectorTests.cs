using System;
using NUnit.Framework;
using Transform;
using System.Drawing;
using Projection;

namespace UnitTests.TransformTests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Can_get_magnitudes()
        {
            var v = new Vector( new PointD( 0, 0 ), new PointD( 3, 4 ) );
            Assert.That( v.Magnitude, Is.EqualTo( 5 ) );
        }

        [Test]
        public void Can_compute_dot_product()
        {
            var v1 = new Vector( new PointD( 0, 0 ), new PointD( 3, 4 ) );
            var v2 = new Vector( new PointD( 0, 0 ), new PointD( 5, 6 ) );
            Assert.That( v1*v2, Is.EqualTo( 39 ) );
        }

        [Test]
        public void Can_compute_cross_product()
        {
            var v1 = new Vector( new PointD( 0, 0 ), new PointD( 3, 4 ) );
            var v2 = new Vector( new PointD( 0, 0 ), new PointD( 5, 6 ) );
            Assert.That( v1^v2, Is.EqualTo( -2 ) );
        }
    }
}

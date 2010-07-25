using System;
using NUnit.Framework;
using Projection;

namespace UnitTests.ProjectionTests
{
    [TestFixture]
    public class RawTests
    {
        [Test]
        public void Raw_does_not_change_points()
        {
            PointD orig = new PointD( 123.456, 789.012 );
            PointD proj = new Raw().ToProjection( orig );
            Assert.That(orig.X, Is.EqualTo(proj.X));
            Assert.That( orig.Y, Is.EqualTo( proj.Y) );

            orig = new Raw().FromProjection( proj );
            Assert.That( proj.X, Is.EqualTo( orig.X ) );
            Assert.That( proj.Y, Is.EqualTo( orig.Y ) );
        }
    }
}

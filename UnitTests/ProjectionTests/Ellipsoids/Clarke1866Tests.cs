using System;
using NUnit.Framework;
using Projection.Ellipsoids;

namespace UnitTests.ProjectionTests.Ellipsoids
{
    [TestFixture]
    public class Clarke1866Tests
    {
        private Clarke1866 _ellipsoid;

        [SetUp]
        public void Setup()
        {
            _ellipsoid = new Clarke1866();
        }

        [Test]
        public void EquatorialRadius_is_correct()
        {            
            Assert.That( _ellipsoid.a, Is.EqualTo( 6378206.4 ) );
        }

        [Test]
        public void PolarRadius_is_correct()
        {
            Assert.That( _ellipsoid.b, Is.EqualTo( 6356583.8) );
        }

        [Test]
        public void Flattening_is_correct()
        {
            Assert.That( _ellipsoid.f, Is.EqualTo( 1/294.98 ) );
        }

        [Test]
        public void Esquared_is_correct()
        {
            System.Diagnostics.Debug.WriteLine( _ellipsoid.e2 );
            Assert.That( _ellipsoid.e2, Is.EqualTo( 1 - (_ellipsoid.b * _ellipsoid.b) / (_ellipsoid.a * _ellipsoid.a) ) );
        }
    }
}

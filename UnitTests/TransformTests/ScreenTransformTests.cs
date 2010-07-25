using System;
using NUnit.Framework;
using Transform;
using System.Drawing;
using Projection;

namespace UnitTests.TransformTests
{
    [TestFixture]
    public class ScreenTransformTests
    {
        [Test]
        public void Can_take_bounds_in_constructor()
        {
            var transform = new ScreenTransform(0,10,20,30);
            Assert.That( transform.MinX, Is.EqualTo( 0 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 10 ) );
            Assert.That( transform.MinY, Is.EqualTo( 20 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 30 ) );
        }

        [Test]
        public void Can_zoom_in()
        {
            var transform = new ScreenTransform( 0, 100, 0, 100 );
            transform.ZoomIn( 10 );
            Assert.That( transform.MinX, Is.EqualTo( 5 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 95 ) );
            Assert.That( transform.MinY, Is.EqualTo( 5 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 95 ) );
        }

        [Test]
        public void Max_zoom_in_one_step_is_fifty()
        {
            var transform = new ScreenTransform( 0, 100, 0, 100 );
            transform.ZoomIn( 100 );
            Assert.That( transform.MinX, Is.EqualTo( 25 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 75 ) );
            Assert.That( transform.MinY, Is.EqualTo( 25 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 75 ) );
        }

        [Test]
        public void Zoom_is_relative_to_span_in_each_direction()
        {
            var transform = new ScreenTransform( 0, 100, 0, 200 );
            transform.ZoomIn( 10 );
            Assert.That( transform.MinX, Is.EqualTo( 5 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 95 ) );
            Assert.That( transform.MinY, Is.EqualTo( 10 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 190 ) );
        }

        [Test]
        public void Can_zoom_out()
        {
            var transform = new ScreenTransform( 0, 100, 0, 200 );
            transform.ZoomOut( 10 );
            Assert.That( transform.MinX, Is.EqualTo( -5 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 105 ) );
            Assert.That( transform.MinY, Is.EqualTo( -10 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 210 ) );
        }

        [Test]
        public void Can_transform_point()
        {
            var rect = new RectangleF( 0, 0, 100, 200 );
            var transform = new ScreenTransform( 1000, 2000, 1000, 2000 );
            PointF point = transform.ToScreenPoint( rect, new PointD(1100, 1200) );
            Assert.That( Math.Round(point.X,5), Is.EqualTo( 10 ) );
            Assert.That( point.Y, Is.EqualTo( 70 ) );
        }

        [Test]
        public void Can_shift_by_screen_coordinates()
        {
            var rect = new RectangleF( 0, 0, 100, 200 );
            var transform = new ScreenTransform( 1000, 2000, 1000, 2000 );
            transform.ShiftByScreenDelta( rect, -10, -10 );
            Assert.That( transform.MinX, Is.EqualTo( 900 ) );
            Assert.That( transform.MaxX, Is.EqualTo( 1900 ) );
            Assert.That( transform.MinY, Is.EqualTo( 900 ) );
            Assert.That( transform.MaxY, Is.EqualTo( 1900 ) );
        }

        [Test]
        public void Can_transform_screen_point()
        {
            var rect = new RectangleF( 0, 0, 100, 200 );
            var transform = new ScreenTransform( 1000, 2000, 1000, 2000 );
            PointD point = transform.ToCoordinatePoint( rect, new PointF(10, 70) );
            Assert.That( point.X, Is.EqualTo( 1100 ) );
            Assert.That( point.Y, Is.EqualTo( 1200 ) );
        }

        [Test]
        public void Can_get_distance_between_point_and_line()
        {
            var distance = ScreenTransform.DistanceBetweenPointAndSegment( new PointD( 3, 1 ), new PointD( 1, 1 ), new PointD( 3, 3 ) );
            Assert.That( Math.Round( distance, 10 ), Is.EqualTo( Math.Round( Math.Sqrt( 2 ), 10 ) ) );
        }
    }
}

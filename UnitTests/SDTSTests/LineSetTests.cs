using System;
using System.Linq;
using NUnit.Framework;
using SDTSLib.ISO8211Data;
using SDTSLib.SDTSData;


namespace UnitTests.SDTSTests
{
    [TestFixture]
    public class LineSetTests
    {
        [Test]
        public void can_create_from_ddf()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            var dir = new LineSet( ddf );

            Assert.That( dir.Lines, Is.Not.Null );
            Assert.That( dir.Lines.Count, Is.EqualTo( 12 ) );
            Assert.That(dir.Lines[0].DataRow, Is.Not.Null);
            Assert.That(dir.Lines[0].Points, Is.Not.Null);
            Assert.That( dir.Lines[0].Points.Count, Is.EqualTo( 2 ) );
        }

        [Test]
        public void Has_id()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            var dir = new LineSet( ddf );

            Assert.That( dir.Lines[ 0 ].ID, Is.Not.Null );            
        }

        [Test]
        public void Has_polygon_ids()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            var dir = new LineSet( ddf );

            Assert.That( dir.Lines[0].PolygonIDLeft, Is.Not.Null );
            Assert.That( dir.Lines[ 0 ].PolygonIDLeft, Is.EqualTo( 1 ) );
            Assert.That( dir.Lines[ 0 ].PolygonIDRight, Is.Not.Null );
            Assert.That( dir.Lines[ 0 ].PolygonIDRight, Is.EqualTo( 6 ) );
        }

        [Test]
        public void Has_node_ids()
        {

            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            var dir = new LineSet( ddf );

            Assert.That( dir.Lines[ 0 ].StartNodeID, Is.Not.Null );
            Assert.That( dir.Lines[ 0 ].StartNodeID, Is.EqualTo( 1 ) );
            Assert.That( dir.Lines[ 0 ].EndNodeID, Is.Not.Null );
            Assert.That( dir.Lines[ 0 ].EndNodeID, Is.EqualTo( 2 ) );
        }

        [Test]
        public void Has_coordinates_in_meters()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            var dir = new LineSet( ddf );

            Assert.That( dir.Lines[ 0 ].Points[ 0 ].X, Is.EqualTo( 272132.87 ) );
            Assert.That( dir.Lines[ 0 ].Points[ 0 ].Y, Is.EqualTo( 3762856.33 ) );
        }

        // Test commented out because it takes too long to run. Need a smaller lineset for testing
        //[Test]
        //public void has_attribute_ids()
        //{
        //    var ddf = DataDefinitionFile.FromFile( "RD01LE01.DDF" );
        //    var dir = new LineSet( ddf );  
        //  
        //    Assert.That(dir.Lines[0].AttributeIDs, Is.Not.Null);
        //    var attr = dir.Lines[ 0 ].AttributeIDs.FirstOrDefault( attribute => attribute.Key == "ARDF" );
        //    Assert.That(attr, Is.Not.Null);
        //    Assert.That( attr.Value, Is.EqualTo( 150 ) );
        //}
    }
}

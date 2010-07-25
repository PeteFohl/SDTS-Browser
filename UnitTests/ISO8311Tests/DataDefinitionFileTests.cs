using System;
using NUnit.Framework;
using SDTSLib.ISO8211Data;

namespace UnitTests
{
    [TestFixture]
    public class DataDefinitionFileTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Can_load_file_ddr()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01CATD.DDF" );
            Assert.That(ddf.DescriptiveRecord, Is.Not.Null);
        }

        [Test]
        public void Can_load_single_data_record()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01CATD.DDF" );
            Assert.That( ddf.DataRecords, Is.Not.Null );
            Assert.That( ddf.DataRecords.Count, Is.EqualTo( 1 ) );
            Assert.That( ddf.DataRecords[0].Rows.Count, Is.EqualTo( 23 ) );
        }

        [Test]
        public void Can_load_line_data()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01LE01.DDF" );
            Assert.That( ddf.DataRecords.Count, Is.GreaterThan( 1 ) );
        }

        [Test]
        public void Can_load_schema()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01DDSH.DDF" );
            Assert.That( ddf.DataRecords.Count, Is.EqualTo( 1 ) );
            Assert.That( ddf.DataRecords[ 0 ].Rows.Count, Is.EqualTo( 41 ) );
        }

        [Test]
        public void Can_load_all_files_without_error()
        {
            //TestFileLoad( "BD01ABDF.DDF" ); This file seems corrupt
            TestFileLoad( "BD01AHDR.DDF" );
            TestFileLoad( "BD01CATD.DDF" );
            TestFileLoad( "BD01CATS.DDF" );
            TestFileLoad( "BD01CATX.DDF" );
            TestFileLoad( "BD01DDSH.DDF" );
            TestFileLoad( "BD01DQAA.DDF" );
            TestFileLoad( "BD01DQCG.DDF" );
            TestFileLoad( "BD01DQHL.DDF" );
            TestFileLoad( "BD01DQLC.DDF" );
            TestFileLoad( "BD01DQPA.DDF" );
            TestFileLoad( "BD01FF01.DDF" );
            TestFileLoad( "BD01IDEN.DDF" );
            TestFileLoad( "BD01IREF.DDF" );
            TestFileLoad( "BD01LE01.DDF" );
            TestFileLoad( "BD01NA01.DDF" );
            TestFileLoad( "BD01NO01.DDF" );
            TestFileLoad( "BD01NP01.DDF" );
            TestFileLoad( "BD01PC01.DDF" );
            TestFileLoad( "BD01STAT.DDF" );
            TestFileLoad( "BD01XREF.DDF" );
        }

        private void TestFileLoad( string filename )
        {
            var ddf = DataDefinitionFile.FromFile( filename );
            Assert.That(ddf, Is.Not.Null);
        }
    }
}

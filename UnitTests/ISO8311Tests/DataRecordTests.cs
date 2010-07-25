using System;
using NUnit.Framework;
using System.IO;
using System.Linq;
using SDTSLib.ISO8211Data;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class DataRecordTests
    {
        private FileStream _stream;
        private BinaryReader _reader;
        private DataDescriptiveRecord _ddr;
        [SetUp]
        public void SetUp()
        {
            _stream = new FileStream( "BD01CATD.DDF", FileMode.Open );
            _reader = new BinaryReader( _stream );
            _ddr = DataDescriptiveRecord.FromStream( _reader );
        }

        [TearDown]
        public void TearDown()
        {
            _stream.Close();
            _stream.Dispose();
        }

        [Test]
        public void Has_directories()
        {
            DataRecord dr = DataRecord.FromStream( _ddr, _reader );
            Assert.That( dr.Directories, Is.Not.Null );
        }

        [Test]
        public void Directories_are_loaded()
        {
            DataRecord dr = DataRecord.FromStream( _ddr, _reader );
            Assert.That( dr.Directories.Count, Is.EqualTo( 2 ) );
            Assert.That( dr.Directories[0].Tag, Is.EqualTo( DirectoryDataType.DDFRecordIdentifier ) );
            Assert.That( dr.Directories[0].Length, Is.EqualTo( 7 ) );
            Assert.That( dr.Directories[ 0 ].Position, Is.EqualTo( 0 ) );
            Assert.That( dr.Directories[ 1 ].Tag, Is.EqualTo( DirectoryDataType.CATD ) );
            Assert.That( dr.Directories[ 1 ].Length, Is.EqualTo( 65 ) );
            Assert.That( dr.Directories[ 1 ].Position, Is.EqualTo( 7 ) );
        }

        [Test]
        public void Rows_are_loaded()
        {
            DataRecord dr = DataRecord.FromStream( _ddr, _reader );
            Assert.That( dr.Rows, Is.Not.Null );
            Assert.That( dr.Rows.Count, Is.EqualTo( 23 ) );
            DataRecordRow row = dr.Rows[ 0 ];
            Assert.That( row.ID, Is.EqualTo( 1 ) );
        }

        [Test]
        public void DataRecord_has_parent()
        {
            DataRecord dr = DataRecord.FromStream( _ddr, _reader );
            Assert.That( dr.DescriptiveRecord, Is.SameAs( _ddr ) );            
        }

        [Test]
        public void Rows_have_data()
        {
            DataRecord dr = DataRecord.FromStream( _ddr, _reader );
            DataRecordRow row = dr.Rows[ 0 ];
            Assert.That(row.Fields, Is.Not.Null);
            Assert.That( row.Fields.Count, Is.EqualTo( 7 ) );
            Assert.That( row.Fields[0].AsString, Is.EqualTo( "CATD" ) );
            Assert.That( row.Fields[1].AsInt, Is.EqualTo( 1 ) );
            Assert.That( row.Fields[ 2 ].AsString, Is.EqualTo( "IDEN" ) );
            Assert.That( row.Fields[ 3 ].AsString, Is.EqualTo( "Identification" ) );
            Assert.That( row.Fields[ 4 ].AsString, Is.EqualTo( "BD01IDEN.DDF" ) );
            Assert.That( row.Fields[ 5 ].AsString, Is.EqualTo( "N" ) );
            Assert.That( row.Fields[ 6 ].AsString, Is.EqualTo( "" ) );
        }

        [Test]
        public void Can_handle_specified_lengths_of_fields()
        {
            using( var stream = new FileStream( "BD01NP01.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    var ddr = DataDescriptiveRecord.FromStream( reader );
                    var dr = DataRecord.FromStream( ddr, reader );
                    Assert.That( dr.Rows.Count, Is.EqualTo( 4 ) );
                    List<DataRecordField> fields = dr.Rows[ 0 ].Fields;
                    Assert.That( fields.Count, Is.EqualTo( 5 ) );
                    Assert.That( fields[ 0 ].AsString, Is.EqualTo( "NP01" ) );
                    Assert.That( fields[ 1 ].AsInt, Is.EqualTo( 1 ) );
                    Assert.That( fields[ 2 ].AsString, Is.EqualTo( "NP" ) );
                    Assert.That( fields[ 3 ].AsInt, Is.EqualTo( 27213287 ) );
                    Assert.That( fields[ 4 ].AsInt, Is.EqualTo( 376285633 ) );
                }
                stream.Close();
            }
        }

    }
}

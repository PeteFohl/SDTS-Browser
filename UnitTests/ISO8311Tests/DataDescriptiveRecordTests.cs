using System;
using NUnit.Framework;
using System.IO;
using System.Linq;
using SDTSLib.ISO8211Data;

namespace UnitTests
{
    [TestFixture]
    public class DataDescriptiveRecordTests
    {
        private FileStream _stream;
        private BinaryReader _reader;
        [SetUp]
        public void SetUp()
        {
            _stream = new FileStream( "BD01CATD.DDF", FileMode.Open );
            _reader = new BinaryReader( _stream );
        }

        [TearDown]
        public void TearDown()
        {
            _stream.Close();
            _stream.Dispose();
        }

        [Test]
        public void DDR_has_Directories()
        {
            DataDescriptiveRecord ddr = DataDescriptiveRecord.FromStream( _reader );
            Assert.That( ddr.Directories, Is.Not.Null );
        }

        [Test]
        public void Directories_are_loaded()
        {
            DataDescriptiveRecord ddr = DataDescriptiveRecord.FromStream( _reader );
            Assert.That( ddr.Directories.Count, Is.EqualTo( 3 ) );

            DDRDirectoryEntry entry = ddr.Directories[ 0 ] as DDRDirectoryEntry;
            Assert.That( entry.Tag, Is.EqualTo( DirectoryDataType.Filename ) );
            Assert.That( entry.Length, Is.EqualTo( 15 ) );
            Assert.That( entry.Position, Is.EqualTo( 0 ) );
            Assert.That( entry.Type, Is.EqualTo( DDRDirectoryEntryType.FileName ) );
            Assert.That( entry.FieldTerminatorChar, Is.EqualTo( ';' ) );
            Assert.That( entry.UnitTerminatorChar, Is.EqualTo( '&' ) );
            Assert.That( entry.Fields.Count(), Is.EqualTo( 1 ) );
            Assert.That( entry.Fields.ElementAt( 0 ), Is.EqualTo( "BD01CATD" ) );
            Assert.That( entry.SubFields, Is.Null );

            entry = ddr.Directories[ 1 ] as DDRDirectoryEntry;
            Assert.That( entry.Tag, Is.EqualTo( DirectoryDataType.DDFRecordIdentifier ) );
            Assert.That( entry.Length, Is.EqualTo( 28 ) );
            Assert.That( entry.Position, Is.EqualTo( 15 ) );
            Assert.That( entry.Type, Is.EqualTo( DDRDirectoryEntryType.RecordIdentifier ) );
            Assert.That( entry.FieldTerminatorChar, Is.EqualTo( ';' ) );
            Assert.That( entry.UnitTerminatorChar, Is.EqualTo( '&' ) );
            Assert.That( entry.Fields.Count(), Is.EqualTo( 1 ) );
            Assert.That( entry.Fields.ElementAt( 0 ), Is.EqualTo( "DDF RECORD IDENTIFIER" ) );
            Assert.That( entry.SubFields, Is.Null );

            entry = ddr.Directories[ 2 ] as DDRDirectoryEntry;
            Assert.That( entry.Tag, Is.EqualTo( DirectoryDataType.CATD ) );
            Assert.That( entry.Length, Is.EqualTo( 68 ) );
            Assert.That( entry.Position, Is.EqualTo( 43 ) );
            Assert.That( entry.Type, Is.EqualTo( DDRDirectoryEntryType.FieldList ) );
            Assert.That( entry.FieldTerminatorChar, Is.EqualTo( ';' ) );
            Assert.That( entry.UnitTerminatorChar, Is.EqualTo( '&' ) );
            Assert.That( entry.Fields.Count(), Is.EqualTo( 3 ) );
            Assert.That( entry.Fields.ElementAt( 0 ), Is.EqualTo( "CATALOG/DIRECTORY" ) );
            Assert.That( entry.Fields.ElementAt( 1 ), Is.EqualTo( "MODN!RCID!NAME!TYPE!FILE!EXTR!MVER" ) );
            Assert.That( entry.Fields.ElementAt( 2 ), Is.EqualTo( "(A,I,5A)" ) );
            Assert.That( entry.SubFields, Is.Not.Null );
            Assert.That( entry.SubFields.Count(), Is.EqualTo( 7 ) );
            Assert.That( entry.SubFields.ElementAt( 0 ).Name, Is.EqualTo( "MODN" ) );
            Assert.That( entry.SubFields.ElementAt( 0 ).Type, Is.EqualTo( FieldType.ASCII ) );
            Assert.That( entry.SubFields.ElementAt( 1 ).Name, Is.EqualTo( "RCID" ) );
            Assert.That( entry.SubFields.ElementAt( 1 ).Type, Is.EqualTo( FieldType.Integer ) );
            Assert.That( entry.SubFields.ElementAt( 2 ).Name, Is.EqualTo( "NAME" ) );
            Assert.That( entry.SubFields.ElementAt( 2 ).Type, Is.EqualTo( FieldType.ASCII ) );
            Assert.That( entry.SubFields.ElementAt( 3 ).Name, Is.EqualTo( "TYPE" ) );
            Assert.That( entry.SubFields.ElementAt( 3 ).Type, Is.EqualTo( FieldType.ASCII ) );
            Assert.That( entry.SubFields.ElementAt( 4 ).Name, Is.EqualTo( "FILE" ) );
            Assert.That( entry.SubFields.ElementAt( 4 ).Type, Is.EqualTo( FieldType.ASCII ) );
            Assert.That( entry.SubFields.ElementAt( 5 ).Name, Is.EqualTo( "EXTR" ) );
            Assert.That( entry.SubFields.ElementAt( 5 ).Type, Is.EqualTo( FieldType.ASCII ) );
            Assert.That( entry.SubFields.ElementAt( 6 ).Name, Is.EqualTo( "MVER" ) );
            Assert.That( entry.SubFields.ElementAt( 6 ).Type, Is.EqualTo( FieldType.ASCII ) );
        }

        [Test]
        public void Can_handle_matrix_record_directories()
        {
            using( FileStream stream = new FileStream( "BD01FF01.DDF", FileMode.Open ) )
            {
                BinaryReader reader = new BinaryReader( stream );
                DataDescriptiveRecord ddr = DataDescriptiveRecord.FromStream( reader );
                Assert.That( ddr.Directories.Count, Is.EqualTo( 5 ) );
                DDRDirectoryEntry entry = ddr.Directories[ 3 ] as DDRDirectoryEntry;
                Assert.That( entry.Type, Is.EqualTo( DDRDirectoryEntryType.ArrayFieldList ) );
                Assert.That( entry.SubFields, Is.Not.Null );
                Assert.That( entry.SubFields.Count(), Is.EqualTo( 2 ) );
                Assert.That( entry.SubFields.ElementAt( 0 ).Name, Is.EqualTo( "MODN" ) );
                Assert.That( entry.SubFields.ElementAt( 1 ).Name, Is.EqualTo( "RCID" ) );
                stream.Close();                
            }
        }

        [Test]
        public void Filename_loads()
        {
            DataDescriptiveRecord ddr = DataDescriptiveRecord.FromStream( _reader );
            Assert.That( ddr.Filename, Is.EqualTo( "BD01CATD" ) );            
        }

        [Test]
        public void Attributes_have_lengths_if_specified()
        {
            using( var stream = new FileStream( "BD01NP01.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    var ddr = DataDescriptiveRecord.FromStream( reader );
                    var entry = ddr.Directories[ 2 ] as DDRDirectoryEntry;
                    Assert.That( entry.SubFields[0].Length, Is.EqualTo( 4 ) );
                    Assert.That( entry.SubFields[ 1 ].Length, Is.EqualTo(6 ) );
                    Assert.That( entry.SubFields[ 2 ].Length, Is.EqualTo( 2 ) );

                }
                stream.Close();
            }
        }

        [Test]
        public void Binary_attribute_lengths_are_converted_to_number_of_bytes()
        {
            using( var stream = new FileStream( "BD01NP01.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    var ddr = DataDescriptiveRecord.FromStream( reader );
                    var entry = ddr.Directories[ 3 ] as DDRDirectoryEntry;
                    Assert.That( entry.SubFields[ 0 ].Type, Is.EqualTo( FieldType.Binary ) );
                    Assert.That( entry.SubFields[ 0 ].Length, Is.EqualTo( 4 ) );
                    Assert.That( entry.SubFields[ 1 ].Type, Is.EqualTo( FieldType.Binary ) );
                    Assert.That( entry.SubFields[ 1 ].Length, Is.EqualTo( 4 ) );
                }
                stream.Close();
            }
        }
    }
}

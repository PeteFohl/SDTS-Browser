using System;
using System.Linq;
using NUnit.Framework;
using SDTSLib.ISO8211Data;
using SDTSLib.SDTSData;
using Projection;

namespace UnitTests.SDTSTests
{
    [TestFixture]
    public class CatalogDirectoryTests
    {
        [Test]
        public void Can_create_catalog_directory_file()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01CATD.DDF" );
            var dir = new CatalogDirectory( ddf );

            Assert.That( dir.Entries, Is.Not.Null );
            Assert.That( dir.Entries.Count, Is.EqualTo( 23 ) );
            CatalogDirectoryEntry entry = dir.Entries.FirstOrDefault( e => e.FileName == "BD01CATD.DDF" );
            Assert.That(entry, Is.Not.Null);
            Assert.That( entry.Contents, Is.EqualTo( "Catalog/Directory" ) );
            Assert.That( entry.Type, Is.EqualTo( "CATD" ) );
        }

        [Test]
        public void Can_load_entry_files()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01CATD.DDF" );
            var dir = new CatalogDirectory( ddf );
            Assert.That(dir.Entries[0].File, Is.Not.Null);
        }

        [Test]
        public void Projection_is_determined_if_utm()
        {
            var ddf = DataDefinitionFile.FromFile( "BD01CATD.DDF" );
            var dir = new CatalogDirectory( ddf );
            Assert.That(dir.Projection, Is.Not.Null);
            Assert.That( dir.Projection, Is.TypeOf( typeof( UTM ) ) );
            Assert.That( (dir.Projection as UTM).Zone, Is.EqualTo( 11 ) );
        }
    }
}

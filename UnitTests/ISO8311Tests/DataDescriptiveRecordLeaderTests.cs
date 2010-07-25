using System;
using NUnit.Framework;
using System.IO;
using SDTSLib.ISO8211Data;

namespace UnitTests
{
    [TestFixture]
    public class DataDescriptiveRecordLeaderTests
    {
        private DataDescriptiveRecordLeader _leader;

        [SetUp]
        public void SetUp()
        {
            using( FileStream stream = new FileStream( "BD01CATD.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    _leader = DataDescriptiveRecordLeader.FromStream( reader );
                }
                stream.Close();
            }
        }

        [Test]
        public void Attributes_are_correct()
        {
            Assert.That( _leader.RecordLength, Is.EqualTo( 160 ) );
            Assert.That( _leader.InterchangeLevel, Is.EqualTo( '2' ) );
            Assert.That( _leader.LeaderType, Is.EqualTo( LeaderType.DataDescriptiveRecord) );
            Assert.That( _leader.InlineCodeExtenstionIndicator, Is.EqualTo( ' ' ) );
            Assert.That( _leader.VersionNumber, Is.EqualTo( ' ' ) );
            Assert.That( _leader.AppIndicator, Is.EqualTo( ' ' ) );
            Assert.That( _leader.FieldControlLength, Is.EqualTo( 6 ) );
            Assert.That( _leader.FieldAreaStart, Is.EqualTo( 49 ) );
            Assert.That( _leader.SizeFieldLength, Is.EqualTo( 2 ) );
            Assert.That( _leader.SizeFieldPos, Is.EqualTo( 2 ) );
            Assert.That( _leader.SizeFieldTag, Is.EqualTo( 4 ) );
        }
    }
}

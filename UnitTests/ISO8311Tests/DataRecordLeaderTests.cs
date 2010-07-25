using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDTSLib.ISO8211Data;
using System.IO;
using NUnit.Framework;

namespace UnitTests
{
    public class DataRecordLeaderTests
    {
        private DataRecordLeader _leader;

        [SetUp]
        public void SetUp()
        {
            using( FileStream stream = new FileStream( "BD01CATD.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    // Consume the DDR first
                    var ddr = DataDescriptiveRecord.FromStream( reader );
                    _leader = DataRecordLeader.FromStream( reader );
                }
                stream.Close();
            }
        }

        [Test]
        public void Can_get_record_length()
        {
            Assert.That( _leader.RecordLength, Is.EqualTo( 111) );
        }

        [Test]
        public void Can_get_repeating_leader_type()
        {
            Assert.That( _leader.LeaderType, Is.EqualTo( LeaderType.RepeatingDataRecord ) );
        }

        [Test]
        public void Can_get_non_repeating_leader_type()
        {
            using( FileStream stream = new FileStream( "BD01IREF.DDF", FileMode.Open ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    // Consume the DDR first
                    var ddr = DataDescriptiveRecord.FromStream( reader );
                    var leader = DataRecordLeader.FromStream( reader );
                    Assert.That( leader.LeaderType, Is.EqualTo( LeaderType.DataRecord ) );
                }
                stream.Close();
            }
        }

        [Test]
        public void Can_get_field_area_info()
        {
            Assert.That( _leader.FieldAreaStart, Is.EqualTo( 39 ) );
            Assert.That( _leader.SizeFieldLength, Is.EqualTo( 2 ) );
            Assert.That( _leader.SizeFieldPos, Is.EqualTo( 1 ) );
            Assert.That( _leader.SizeFieldTag, Is.EqualTo( 4 ) );
        }
    }
}

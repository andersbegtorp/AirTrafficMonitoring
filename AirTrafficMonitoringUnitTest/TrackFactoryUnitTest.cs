using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    public class TrackFactoryUnitTest
    {
        private TrackFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackFactory();
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789", "ATR423")]
        [TestCase("CPR423;39345;13432;14023;20151006213456789", "CPR423")]
        public void CreateTrack_CreatesTrackWithTag_TagIsCorrect(string trackInfo, string expectedTag)
        {
            Assert.That(_uut.CreateTrack(trackInfo).Tag, Is.EqualTo(expectedTag));
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789", 39045)]
        [TestCase("ATR423;23045;12932;14000;20151006213456789", 23045)]
        public void CreateTrack_CreatesTrackWithXCoordinate_XCoordinateIsCorrect(string trackInfo, int expectedCoordinate)
        {
            Assert.That(_uut.CreateTrack(trackInfo).XCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [TestCase("ATR423;23045;12932;14000;20151006213456789", 12932)]
        [TestCase("ATR423;23045;16843;14000;20151006213456789", 16843)]
        [TestCase("ATR423;23045;16850;14000;20151006213456789", 16850)]
        public void CreateTrack_CreatesTrackWithYCoodinate_YCoodinateIsCorrect(string trackInfo, int expectedCooridnate)
        {
            Assert.That(_uut.CreateTrack(trackInfo).YCoordinate, Is.EqualTo(expectedCooridnate));
        }

    }
}

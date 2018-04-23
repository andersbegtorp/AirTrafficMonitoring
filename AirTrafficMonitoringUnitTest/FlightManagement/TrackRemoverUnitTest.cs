using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightManagement;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.FlightManagement
{
    [TestFixture]
    public class TrackRemoverUnitTest
    {
        private TrackRemover _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackRemover();
        }

        [Test]
        public void RemoveTrack_RemovesTrack_CorrectTrackWasRemoved()
        {
            List<Track> tracks = new List<Track>();
            Track trackToBeRemoved = new Track()
                {Tag = "Remove"};

            for (int i = 0; i < 5; i++)
            {
                tracks.Add(new Track(){Tag = "FlightNumber" + i}) ;
            }

            tracks.Add(trackToBeRemoved);

            // ACT
            _uut.RemoveTrack(tracks,trackToBeRemoved);

            // ASSERT
            Assert.That(tracks.Count(p => p.Tag == trackToBeRemoved.Tag), Is.EqualTo(0));
        }
    }
}

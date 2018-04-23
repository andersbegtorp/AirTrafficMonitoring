using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightManagement;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightManagement
{
    [TestFixture()]
    public class IT13_TrackManagement_FlightManagement
    {
        private ITrackRemover _trackRemover;
        private ITrackManagement _fakeTrackManagement;
        private IAirspaceController _fakeAirspaceController;
        private IFlightManagement _flightManagement;

        [SetUp]
        public void SetUp()
        {
            _trackRemover = new TrackRemover();
            _fakeTrackManagement = new TrackManagement();
            _fakeAirspaceController = Substitute.For<IAirspaceController>();
            _flightManagement = new AirTrafficMonitoring.FlightManagement.FlightManagement(_fakeAirspaceController, _trackRemover, _fakeTrackManagement);
        }

        [Test]
        public void HandleTrackInsideAirspace_TrackDoesNotExistInNewestList_TrackWasAddedToNewestList()
        {
            Track track = new Track() { Tag = "FlightNo 0" };

            TrackEventArgs arg = new TrackEventArgs(track);

            List<Track> newestTracks = new List<Track>();
            _flightManagement.FlightDataReady += (sender, args) => newestTracks = args.NewestTracks;

            //Act
            _flightManagement.HandleTrackInsideAirspace(this, arg);


            Assert.That(newestTracks.Count(p => p.Tag == track.Tag), Is.EqualTo(1));

        }

        [Test]
        public void HandleTrackInsideAirspace_TrackDoesNotExistInOldestList_TrackWasAddedToOldestList()
        {
            Track track = new Track() { Tag = "FlightNo 0" };

            TrackEventArgs arg = new TrackEventArgs(track);

            List<Track> oldestTracks = new List<Track>();
            _flightManagement.FlightDataReady += (sender, args) => oldestTracks = args.OldestTracks;

            //Act
            _flightManagement.HandleTrackInsideAirspace(this, arg);
            _flightManagement.HandleTrackInsideAirspace(this, arg);

            Assert.That(oldestTracks.Count(p => p.Tag == track.Tag), Is.EqualTo(1));

        }

    }
}

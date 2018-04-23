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
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightManagement
{
    [TestFixture()]
    public class IT12_TrackRemover_FlightManagement
    {
        private ITrackRemover _trackRemover;
        private ITrackManagement _fakeTrackManagement;
        private IAirspaceController _fakeAirspaceController;
        private IFlightManagement _flightManagement;

        [SetUp]
        public void SetUp()
        {
            _trackRemover = new TrackRemover();
            _fakeTrackManagement = Substitute.For<ITrackManagement>();
            _fakeAirspaceController = Substitute.For<IAirspaceController>();
            _flightManagement = new AirTrafficMonitoring.FlightManagement.FlightManagement(_fakeAirspaceController, _trackRemover, _fakeTrackManagement);
        }
        
        [Test]
        public void HandleTrackOutsideAirspace_TrackExistsInNewestTracks_TrackWasRemoved()
        {
            //Arrange
            //Adding elements to private lists inside flightmanagement
            for (int i = 0; i < 10; i++)
            {
                TrackEventArgs e = new TrackEventArgs(new Track() { Tag = "FlightNo " + i });
                _flightManagement.HandleTrackInsideAirspace(this, e);
            }

            //Creating the track that should be removed:
            Track track = new Track() { Tag = "FlightNo 0" };
            TrackEventArgs arg = new TrackEventArgs(track);
            //Creating a list that will be copied upon calling HandleTrackInsideAirspace. 
            List<Track> newestTracks = new List<Track>();
            _flightManagement.FlightDataReady += (sender, args) => newestTracks = args.NewestTracks;
            
            //Act
            _flightManagement.HandleTrackOutsideAirspace(this, arg);

            //Assert
            //Copying private list with newests tracks that will be asserted on
            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(new Track()));

            Assert.That(newestTracks.Count(p => p.Tag == track.Tag), Is.EqualTo(0));

        }

        [Test]
        public void HandleTrackOutsideAirspace_TrackExistsInOldestTracks_TrackWasRemoved()
        {
            //Arrange
            //Adding elements to private lists inside flightmanagement
            for (int i = 0; i < 10; i++)
            {
                TrackEventArgs e = new TrackEventArgs(new Track() { Tag = "FlightNo " + i });
                _flightManagement.HandleTrackInsideAirspace(this, e);
            }
            for (int i = 0; i < 10; i++)
            {
                TrackEventArgs e = new TrackEventArgs(new Track() { Tag = "FlightNo " + i });
                _flightManagement.HandleTrackInsideAirspace(this, e);
            }

            //Creating the track that should be removed:
            Track track = new Track() { Tag = "FlightNo 0" };
            TrackEventArgs arg = new TrackEventArgs(track);
            //Creating a list that will be copied upon calling HandleTrackInsideAirspace. 
            List<Track> oldestTracks = new List<Track>();
            _flightManagement.FlightDataReady += (sender, args) => oldestTracks = args.OldestTracks;

            //Act
            _flightManagement.HandleTrackOutsideAirspace(this, arg);

            //Assert
            //Copying private list with oldest tracks that will be asserted on
            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(new Track()));

            Assert.That(oldestTracks.Count(p => p.Tag == track.Tag), Is.EqualTo(0));

        }

    }
}

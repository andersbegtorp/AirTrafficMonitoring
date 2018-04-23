using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    public class FlightManagementUnitTest
    {
        private ITrackRemover _fakeTrackRemover;
        private ITrackManagement _fakeTrackManagement;
        private IAirspaceController _fakeAirspaceController;
        private IFlightManagement _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeTrackRemover = Substitute.For<ITrackRemover>();
            _fakeAirspaceController = Substitute.For<IAirspaceController>();
        }

        [Test]
        public void HandleTrackOutsideAirspace_TrackOutsideAirspaceGetsRemoved_TrackRemoverRecivesCallWithCorrectTrack()
        {   
            // ARRANGE
            List<Track> newTracks = new List<Track>();
            List<Track> oldTracks = new List<Track>();

            for (int i = 0; i < 5; i++)
            {
                Track track = new Track()
                {
                    Tag = "FlightNo" + i
                };
                newTracks.Add(track);
            }

            // Creating mock and uut
            _fakeTrackManagement = new TrackManagementMock(newTracks, oldTracks);
            
            _uut = new FlightManagement(_fakeAirspaceController, _fakeTrackRemover, _fakeTrackManagement);

            // Adding tracks to newestTracks list via HandleTracksInsideAirspace
            Track trackToBeRemoved = new Track()
            {
                Tag = "FlightNo0"
            };
            TrackEventArgs eventArgs = new TrackEventArgs(trackToBeRemoved);
            _fakeAirspaceController.TrackInAirspace += Raise.EventWith(eventArgs);

            // ACT
            _fakeAirspaceController.TrackOutsideAirspace += Raise.EventWith(eventArgs);

            // ASSERT
            _fakeTrackRemover.Received(1).RemoveTrack(Arg.Any<List<Track>>(), trackToBeRemoved);
        }

        [TestCase(10, 5)]
        [TestCase(12, 1)]
        [TestCase(1, 1)]
        [TestCase(5, 0)]
        public void HandleTrackOutsideAirspace_TracksOutsideAirspaceGetsRemoved_TrackRemoverRecievesCorrectNumberOfCalls(int totalTracks, int numberOfTracksToBeRemoved)
        {
            //Arrange
            //Creating list that will be added to flightmanagements private lists. 
            List<Track> newTracks = new List<Track>();
            List<Track> oldTracks = new List<Track>();

            for (int i = 0; i < totalTracks; i++)
            {
                Track track = new Track() { Tag = "FlightNo" + i };
                newTracks.Add(track);
            }

            //Creating mock and uut:
            _fakeTrackManagement = new TrackManagementMock(newTracks, oldTracks); 
            _uut = new FlightManagement(_fakeAirspaceController, _fakeTrackRemover, _fakeTrackManagement);

            //Adding tracks to newestTracks list via HandleTracksInsideAirspace

            _fakeAirspaceController.TrackInAirspace += Raise.EventWith(new TrackEventArgs(new Track()));

            //Act
            for (int i = 0; i < numberOfTracksToBeRemoved; i++)
            {
                Track trackToBeRemoved = new Track() { Tag = "FlightNo" + i };
                TrackEventArgs arg = new TrackEventArgs(trackToBeRemoved);
                _fakeAirspaceController.TrackOutsideAirspace += Raise.EventWith(arg);
            }

            //Assert
            _fakeTrackRemover.Received(numberOfTracksToBeRemoved).RemoveTrack(Arg.Any<List<Track>>(), Arg.Any<Track>());
        }

        [Test]
        public void HandleTrackInsideAirspace_HandlesTrack_EventWasRaised()
        {
            _fakeTrackManagement = Substitute.For<ITrackManagement>();
            _uut = new FlightManagement(_fakeAirspaceController,_fakeTrackRemover,_fakeTrackManagement);

            bool wasRaised = false;
            _uut.FlightDataReady += (sender, args) => wasRaised = true;
            TrackEventArgs e = new TrackEventArgs(new Track());

            // ACT
            _fakeAirspaceController.TrackInAirspace += Raise.EventWith<TrackEventArgs>(e);

            // ASSERT
            Assert.That(wasRaised, Is.EqualTo(true));
        }
    }
}



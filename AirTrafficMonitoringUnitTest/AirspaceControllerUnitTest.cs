using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    class AirspaceControllerUnitTest
    {
        private AirspaceController _uut;
        private IAirspaceTrackChecker _fakeAirspaceTrackChecker;
        private ITransponderDataReciever _fakeTransponderDataReciever;
       
        [SetUp]
        public void SetUp()
        {

            
            _fakeAirspaceTrackChecker = Substitute.For<IAirspaceTrackChecker>();
            _fakeTransponderDataReciever = Substitute.For<ITransponderDataReciever>();
            _uut = new AirspaceController(_fakeTransponderDataReciever, _fakeAirspaceTrackChecker);

        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void HandleTracks_TracksInAirspace_CorrectEventWasRaised(int NumberOfTracks)
        {
            //Arrange
            int NumberOfEventRaised = 0;
            _uut.TrackInAirspace += (sender, args) => NumberOfEventRaised++;
            _fakeAirspaceTrackChecker.CheckTrack(Arg.Any<Track>()).Returns(true);
            List<Track> tracks = new List<Track>();

            for (int i = 0; i < NumberOfTracks; i++)
            {
                tracks.Add(new Track());
            }


            //Act
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);
            _fakeTransponderDataReciever.TrackDataReady += Raise.EventWith<TracksDataEventArgs>(arg);


            //Assert
            Assert.That(NumberOfEventRaised, Is.EqualTo(NumberOfTracks));


        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void HandleTracks_TracksOutSideAirspace_CorrectEventWasRaised(int NumberOfTracks)
        {
            //Arrange
            int NumberOfEventRaised = 0;
            _uut.TrackOutsideAirspace += (sender, args) => NumberOfEventRaised++;
            _fakeAirspaceTrackChecker.CheckTrack(Arg.Any<Track>()).Returns(false);
            List<Track> tracks = new List<Track>();

            for (int i = 0; i < NumberOfTracks; i++)
            {
                tracks.Add(new Track());
            }


            //Act
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);
            _fakeTransponderDataReciever.TrackDataReady += Raise.EventWith<TracksDataEventArgs>(arg);


            //Assert
            Assert.That(NumberOfEventRaised, Is.EqualTo(NumberOfTracks));


        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void HandleTracks_TracksInAirspace_OutsideEventWasNotRaised(int NumberOfTracks)
        {
            //Arrange
            int NumberOfEventRaised = 0;
            _uut.TrackOutsideAirspace += (sender, args) => NumberOfEventRaised++;
            _fakeAirspaceTrackChecker.CheckTrack(Arg.Any<Track>()).Returns(true);
            List<Track> tracks = new List<Track>();

            for (int i = 0; i < NumberOfTracks; i++)
            {
                tracks.Add(new Track());
            }


            //Act
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);
            _fakeTransponderDataReciever.TrackDataReady += Raise.EventWith<TracksDataEventArgs>(arg);


            //Assert
            Assert.That(NumberOfEventRaised, Is.EqualTo(0));


        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void HandleTracks_TracksOutsideAirspace_insideEventWasNotRaised(int NumberOfTracks)
        {
            //Arrange
            int NumberOfEventRaised = 0;
            _uut.TrackInAirspace += (sender, args) => NumberOfEventRaised++;
            _fakeAirspaceTrackChecker.CheckTrack(Arg.Any<Track>()).Returns(false);
            List<Track> tracks = new List<Track>();

            for (int i = 0; i < NumberOfTracks; i++)
            {
                tracks.Add(new Track());
            }


            //Act
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);
            _fakeTransponderDataReciever.TrackDataReady += Raise.EventWith<TracksDataEventArgs>(arg);


            //Assert
            Assert.That(NumberOfEventRaised, Is.EqualTo(0));


        }

    }

}

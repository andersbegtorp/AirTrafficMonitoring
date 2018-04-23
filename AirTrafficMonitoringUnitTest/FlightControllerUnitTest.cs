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
    public class FlightControllerUnitTest
    {
        private ICollisionAnalyzer _fakeCollisionAnalyzer;
        private ISeparationStringBuilder _fakeSeparationStringBuilder;
        private IFlightManagement _fakeFlightManagement;
        private FlightController _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeFlightManagement = Substitute.For<IFlightManagement>();
            _fakeCollisionAnalyzer = Substitute.For<ICollisionAnalyzer>();
            _fakeSeparationStringBuilder = Substitute.For<ISeparationStringBuilder>();
            _uut = new FlightController(_fakeFlightManagement, _fakeCollisionAnalyzer, _fakeSeparationStringBuilder);
        }

        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 3)]
        [TestCase(4, 6)]
        [TestCase(5, 10)]
        [TestCase(6, 15)]

        public void HandleFlightsInAirspace_RaisesSeparationEvent_EventIsRaisedCorrectNumberOfTimes(int numberOfFlightsConflicting, int numberOfRaises)
        {
            int raises = 0;
            _uut.SeperationEvent += (sender, args) => raises++;

            List<Track> tracks = new List<Track>();
            for (int i = 0; i < numberOfFlightsConflicting; i++)
            {
                tracks.Add(new Track());
            }

            _fakeCollisionAnalyzer.AnalyzeCollision(Arg.Any<Track>(), Arg.Any<Track>()).Returns(true);
            _fakeSeparationStringBuilder.BuildSeperationNote(Arg.Any<Track>(), Arg.Any<Track>()).Returns("");

            FlightMovementEventArgs arg = new FlightMovementEventArgs(tracks, tracks);

            _fakeFlightManagement.FlightDataReady += Raise.EventWith<FlightMovementEventArgs>(arg);

            Assert.That(raises, Is.EqualTo(numberOfRaises));
        }

        [Test]
        public void HandleFlightsInAirspace_RaisesSeparationEvent_TracksAreCorrect()
        {
            bool defaultHandler = false;
            _uut.SeperationEvent += (sender, args) => defaultHandler = true;
            Track conflictingTrackOne = new Track() {Tag = "CT1"};
            Track conflictingTrackTwo = new Track() {Tag = "CT2"};

            List<Track> tracks = new List<Track>();
            tracks.Add(conflictingTrackOne);
            tracks.Add(conflictingTrackTwo);

            for (int i = 0; i < 10; i++)
            {
                tracks.Add(new Track());
            }


            _fakeCollisionAnalyzer.AnalyzeCollision(Arg.Is(conflictingTrackOne), Arg.Is(conflictingTrackTwo)).Returns(true);
            FlightMovementEventArgs arg = new FlightMovementEventArgs(tracks, tracks);

            _fakeFlightManagement.FlightDataReady += Raise.EventWith<FlightMovementEventArgs>(arg);

            _fakeSeparationStringBuilder.Received(1).BuildSeperationNote(conflictingTrackOne, conflictingTrackTwo);
        }

        [Test]
        public void HandleFlightsInAirspace_NoConflictingFlights_NoEventRaises()
        {
            int raises = 0;
            _uut.SeperationEvent += (sender, args) => raises++;
            List<Track> tracks = new List<Track>();
            for (int i = 0; i < 10; i++)
            {
                tracks.Add(new Track());
            }


            _fakeCollisionAnalyzer.AnalyzeCollision(Arg.Any<Track>(), Arg.Any<Track>()).Returns(false);
            FlightMovementEventArgs arg = new FlightMovementEventArgs(tracks, tracks);

            _fakeFlightManagement.FlightDataReady += Raise.EventWith<FlightMovementEventArgs>(arg);

            _fakeSeparationStringBuilder.DidNotReceive().BuildSeperationNote(Arg.Any<Track>(), Arg.Any<Track>());
        }
    }
}

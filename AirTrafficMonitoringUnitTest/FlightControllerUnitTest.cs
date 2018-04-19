using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    public class FlightControllerUnitTest
    {
        private TransponderDataReceiver _uut;
        private ITransponderReceiver _fakeTransponderReceiver;
        private ITrackFactory _fakeTrackFactory;

        [SetUp]
        public void SetUp()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _fakeTrackFactory = Substitute.For<ITrackFactory>();
            _uut = new TransponderDataReceiver(_fakeTransponderReceiver, _fakeTrackFactory);
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(14)]
        public void HandleTransponderData_CreatesNumberOfTracks_CorrectNumberOfTracks(int numberOfTracks)
        {
            var list = new List<string>();
            for (int i = 0; i < numberOfTracks; i++)
            {
                list.Add("");
            }
            RawTransponderDataEventArgs e = new RawTransponderDataEventArgs(list);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(e);

            _fakeTrackFactory.Received(numberOfTracks).CreateTrack(Arg.Any<string>());

        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(22)]
        public void HandleTransponderData_RaisesEventWithTracks_EventIsRaisedWithTracks(int numberOfTracks)
        {
            List<Track> tracks = new List<Track>();
            //Arrange
            var list = new List<string>();
            for (int i = 0; i < numberOfTracks; i++)
            {
                list.Add("");
            }

            RawTransponderDataEventArgs e = new RawTransponderDataEventArgs(list);

            Track trackToBeReturned = new Track();
            _fakeTrackFactory.CreateTrack(Arg.Any<string>()).Returns(trackToBeReturned);

            _uut.TrackDataReady += (sender, args) => tracks = args.Tracks;

            //Act
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(e);

            //Assert
            Assert.That(tracks.Count, Is.EqualTo(numberOfTracks));
        }

        [Test]
        public void HandleTransponderData_ZeroTracksFromTransponder_EventIsNotRaised()
        {
            var list = new List<string>();
            RawTransponderDataEventArgs e = new RawTransponderDataEventArgs(list);

            Track trackToBeReturned = new Track();
            _fakeTrackFactory.CreateTrack(Arg.Any<string>()).Returns(trackToBeReturned);

            bool wasRaised = false;
            _uut.TrackDataReady += (sender, args) => wasRaised = true;

            //Act
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(e);

            //Assert
            Assert.That(wasRaised, Is.False);
        }
    }
}

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
        private FlightController _uut;
        private ITransponderReceiver _fakeTransponderReceiver;
        private ITrackFactory _fakeTrackFactory;
        private IDisplay _fakeDisplay;

        [SetUp]
        public void SetUp()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _fakeTrackFactory = Substitute.For<ITrackFactory>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _uut = new FlightController(_fakeTransponderReceiver, _fakeTrackFactory, _fakeDisplay);
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(14)]
        public void HandleTransponder_CreatesNumberOfTracks_CorrectNumberOfTracks(int numberOfTracks)
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
        [TestCase(4)]
        [TestCase(9)]
        public void HandleTransponder_DisplayRecievesCalls_CorrectNumberOfCalls(int numberOfTracks)
        {
            var list = new List<string>();
            for (int i = 0; i < numberOfTracks; i++)
            {
                list.Add("");
            }
            _fakeTrackFactory.CreateTrack(Arg.Any<string>()).Returns(new Track());
            RawTransponderDataEventArgs e = new RawTransponderDataEventArgs(list);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(e);
            _fakeDisplay.Received(numberOfTracks).DisplayTrack(Arg.Any<Track>());
        }
    }
}

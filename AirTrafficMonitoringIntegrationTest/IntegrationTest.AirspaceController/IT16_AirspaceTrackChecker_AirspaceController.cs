using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.AirspaceController;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.Transponder;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.AirspaceController
{
    [TestFixture()]
    public class IT16_AirspaceTrackChecker_AirspaceController
    {
        private IAirspaceTrackChecker _airspaceTrackChecker;
        private IAirspaceController _airspaceController;
        private ITransponderDataReciever _fakeTransponderDataReciever;
        private Airspace _airspace;

        [SetUp]
        public void SetUp()
        {
            _airspace = new Airspace();
            _airspaceTrackChecker = new AirspaceTrackChecker(_airspace);
            _fakeTransponderDataReciever = Substitute.For<ITransponderDataReciever>();
            _airspaceController = new AirTrafficMonitoring.AirspaceController.AirspaceController(_fakeTransponderDataReciever, _airspaceTrackChecker);
        }

        [Test]
        public void HandleTrack_TrackIsInsideAirspace_EventIsRaised()
        {
            _airspace.HighestAltitude = 3000;
            _airspace.LowestAltitude = 0;
            _airspace.NorthEastXCoordinate = 40000;
            _airspace.NorthEastYCoordinate = 40000;
            _airspace.SouthWestXCoordinate = 0;
            _airspace.SouthWestYCoordinate = 0;

            List<Track> tracks = new List<Track>();
            Track track = new Track() {Altitude = 100, XCoordinate = 200, YCoordinate = 300, Tag = "CP8"};
            tracks.Add(track);
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);

            Track trackFromEvent = new Track();

            _airspaceController.TrackInAirspace += (sender, args) => trackFromEvent = args.Track;

            _airspaceController.HandleTracks(this, arg);

            Assert.That(trackFromEvent, Is.EqualTo(track));

        }

        [Test]
        public void HandleTrack_TrackIsOutideAirspace_EventIsRaised()
        {
            _airspace.HighestAltitude = 3000;
            _airspace.LowestAltitude = 0;
            _airspace.NorthEastXCoordinate = 40000;
            _airspace.NorthEastYCoordinate = 40000;
            _airspace.SouthWestXCoordinate = 0;
            _airspace.SouthWestYCoordinate = 0;

            List<Track> tracks = new List<Track>();
            Track track = new Track() { Altitude = 100, XCoordinate = 50000, YCoordinate = 300, Tag = "CP8" };
            tracks.Add(track);
            TracksDataEventArgs arg = new TracksDataEventArgs(tracks);

            Track trackFromEvent = new Track();

            _airspaceController.TrackOutsideAirspace += (sender, args) => trackFromEvent = args.Track;

            _airspaceController.HandleTracks(this, arg);

            Assert.That(trackFromEvent, Is.EqualTo(track));

        }
    }
}

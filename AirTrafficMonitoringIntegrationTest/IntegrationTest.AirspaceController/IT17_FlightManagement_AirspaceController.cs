using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.AirspaceController;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.FlightManagement;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using AirTrafficMonitoring.Interfaces.Logger;
using AirTrafficMonitoring.Interfaces.Transponder;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.AirspaceController
{
    [TestFixture()]
    public class IT17_FlightManagement_AirspaceController
    {
        private IAirspaceTrackChecker _airspaceTrackChecker;
        private IAirspaceController _airspaceController;
        private ITransponderDataReciever _fakeTransponderDataReciever;
        private Airspace _airspace;
        private ITrackRemover _trackRemover;
        private ITrackManagement _trackManagement;
        private IFlightManagement _flightManagement;
        private ICompassCalculator _compassCalculator;
        private ICourseAnalyzer _courseAnalyzer;
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;
        private IVelocityCalculator _velocityCalculator;
        private IVelocityAnalyzer _velocityAnalyzer;
        private IFlightAnalyzer _flightAnalyzer;
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
        private ICollisionAnalyzer _collisionAnalyzer;
        private ISeparationStringBuilder _separationStringBuilder;
        private ISeparationEventLogger _logger;
        private IFileWriter _fakeFileWriter;
        private ICollisionController _collisionController;


        [SetUp]
        public void SetUp()
        {

            _trackRemover = new TrackRemover();
            _trackManagement = new TrackManagement();
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
            _velocityAnalyzer = new VelocityAnalyzer(_velocityCalculator);
            _compassCalculator = new CompassCalculator();
            _courseAnalyzer = new CourseAnalyzer(_compassCalculator);

            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
            _separationStringBuilder = new SeparationStringBuilder();
            _fakeFileWriter = Substitute.For<IFileWriter>();

            _airspace = new Airspace();
            _airspaceTrackChecker = new AirspaceTrackChecker(_airspace);
            _fakeTransponderDataReciever = Substitute.For<ITransponderDataReciever>();
            _airspaceController = new AirTrafficMonitoring.AirspaceController.AirspaceController(_fakeTransponderDataReciever, _airspaceTrackChecker);

            _flightManagement = new AirTrafficMonitoring.FlightManagement.FlightManagement(_airspaceController, _trackRemover, _trackManagement);
            _flightAnalyzer = new AirTrafficMonitoring.FlightAnalyzer.FlightAnalyzer(_flightManagement, _courseAnalyzer, _velocityAnalyzer);
            _collisionController = new AirTrafficMonitoring.CollisionController.CollisionController(_flightManagement, _collisionAnalyzer, _separationStringBuilder);
            _logger = new AirTrafficMonitoring.CollisionController.Logger(_collisionController, "", _fakeFileWriter);

        }

        [TestCase(10, 0, 180, 0, 180)]
        [TestCase(1, 0, 1800, 0, 2400)]
        public void HandleTracks_TrackIsInsideAirspace_TracksGetReported(int secondsBetweenTimestamps, double x1, double x2, double y1, double y2)
        {
            DateTime firstTimeStamp = DateTime.Now;
            DateTime secondTimeStamp = firstTimeStamp.AddSeconds(secondsBetweenTimestamps);

            Track firstTrack = new Track()
            {
                Tag = "ASE2018",
                Altitude = 300,
                XCoordinate = x1,
                YCoordinate = y1,
                TimeStamp = firstTimeStamp
            };

            Track secondTrack = new Track()
            {
                Tag = "ASE2018",
                Altitude = 300,
                XCoordinate = x2,
                YCoordinate = y2,
                TimeStamp = secondTimeStamp
            };

            string log = "";
            _flightAnalyzer.TracksAnalyzedEvent += (sender, args) => log = args.Log;

            List<Track> firstListWithTracks = new List<Track>();
            firstListWithTracks.Add(firstTrack);
            List<Track> secondListWithTracks = new List<Track>();
            secondListWithTracks.Add(secondTrack);

            _airspaceController.HandleTracks(this, new TracksDataEventArgs(firstListWithTracks));
            _airspaceController.HandleTracks(this, new TracksDataEventArgs(secondListWithTracks));


            StringAssert.Contains(log, secondTrack.ToString());
            
        }

        [TestCase(100, 300, 100, 200, 200, 300)]
        public void HandleTracks_TrackIsInsideAirspaceConflicts_TracksGetsLogged(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {
            Track trackOne = new Track() { Tag = "ASE2018", Altitude = altitude1, XCoordinate = x1, YCoordinate = y2 };
            Track trackTwo = new Track() { Tag = "CAP0805", Altitude = altitude1, XCoordinate = x1, YCoordinate = y2 };



            List<Track> firstListWithTracks = new List<Track>();
            firstListWithTracks.Add(trackOne);
            List<Track> secondListWithTracks = new List<Track>();
            secondListWithTracks.Add(trackTwo);

            _airspaceController.HandleTracks(this, new TracksDataEventArgs(firstListWithTracks));
            _airspaceController.HandleTracks(this, new TracksDataEventArgs(secondListWithTracks));

            _fakeFileWriter.WriteToFile("", Arg.Is<string>(s => s.Contains("ASE2018") && s.Contains("CAP0805")));
        }
    }
}

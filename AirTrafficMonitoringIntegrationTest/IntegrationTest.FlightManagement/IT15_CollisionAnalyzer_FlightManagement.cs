using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
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
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightManagement
{
    [TestFixture()]
    public class IT15_CollisionController_FlightManagement
    {
        private ITrackRemover _trackRemover;
        private ITrackManagement _trackManagement;
        private IAirspaceController _fakeAirspaceController;
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
            _fakeAirspaceController = Substitute.For<IAirspaceController>();
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
            _velocityAnalyzer = new VelocityAnalyzer(_velocityCalculator);
            _compassCalculator = new CompassCalculator();
            _courseAnalyzer = new CourseAnalyzer(_compassCalculator);
            _flightManagement = new AirTrafficMonitoring.FlightManagement.FlightManagement(_fakeAirspaceController, _trackRemover, _trackManagement);
            _flightAnalyzer = new AirTrafficMonitoring.FlightAnalyzer.FlightAnalyzer(_flightManagement, _courseAnalyzer, _velocityAnalyzer);

            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
            _separationStringBuilder = new SeparationStringBuilder();
            _fakeFileWriter = Substitute.For<IFileWriter>();
            _collisionController = new AirTrafficMonitoring.CollisionController.CollisionController(_flightManagement, _collisionAnalyzer, _separationStringBuilder);
            _logger = new AirTrafficMonitoring.CollisionController.Logger(_collisionController, "", _fakeFileWriter);
        }

        [TestCase(100, 300, 100, 200, 200, 300)]
        public void HandleTrackInsideAirpsace_TracksAreColliding_TracksAreLogged(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {
            Track trackOne = new Track() { Tag = "ASE2018", Altitude = altitude1, XCoordinate = x1, YCoordinate = y2 };
            Track trackTwo = new Track() { Tag = "CAP0805", Altitude = altitude1, XCoordinate = x1, YCoordinate = y2 };

            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(trackOne));
            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(trackTwo));

            _fakeFileWriter.WriteToFile("", Arg.Is<string>(s => s.Contains("ASE2018") && s.Contains("CAP0805")));
        }
    }
}

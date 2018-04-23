using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.FlightManagement;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightManagement
{
    public class IT14_FlightAnalyzer_FlightManagement
    {
        private ITrackRemover _trackRemover;
        private ITrackManagement _fakeTrackManagement;
        private IAirspaceController _fakeAirspaceController;
        private IFlightManagement _flightManagement;
        private ICompassCalculator _compassCalculator;
        private ICourseAnalyzer _courseAnalyzer;
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;
        private IVelocityCalculator _velocityCalculator;
        private IVelocityAnalyzer _velocityAnalyzer;
        private IFlightAnalyzer _flightAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _trackRemover = new TrackRemover();
            _fakeTrackManagement = new TrackManagement();
            _fakeAirspaceController = Substitute.For<IAirspaceController>();
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
            _velocityAnalyzer = new VelocityAnalyzer(_velocityCalculator);
            _compassCalculator = new CompassCalculator();
            _courseAnalyzer = new CourseAnalyzer(_compassCalculator);
            _flightManagement = new AirTrafficMonitoring.FlightManagement.FlightManagement(_fakeAirspaceController, _trackRemover, _fakeTrackManagement);
            _flightAnalyzer = new AirTrafficMonitoring.FlightAnalyzer.FlightAnalyzer(_flightManagement, _courseAnalyzer, _velocityAnalyzer);
        }

        [TestCase(10, 0, 180, 0, 180, 25.46, 45)]
        [TestCase(1, 0, 1800, 0, 2400, 3000, 36.9)]
        public void HandleTracksInsideAirspace_TracksGetAnalyzed_TrackDataIsCorrect(int secondsBetweenTimestamps, double x1, double x2, double y1, double y2, double expectedVelocity, double expectedCourse)
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

            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(firstTrack));
            _flightManagement.HandleTrackInsideAirspace(this, new TrackEventArgs(secondTrack));

            string expectedString = "Tag: " + secondTrack.Tag + " X: " + secondTrack.XCoordinate + " Y: " +
                                    secondTrack.YCoordinate +
                                    " Altitude: " + secondTrack.Altitude + " Velocity: " + expectedVelocity +
                                    " Course: " + expectedCourse +
                                    " Time stamp: " + secondTimeStamp.ToString();
            Assert.That(log, Is.EqualTo(expectedString));

        }

    }
}

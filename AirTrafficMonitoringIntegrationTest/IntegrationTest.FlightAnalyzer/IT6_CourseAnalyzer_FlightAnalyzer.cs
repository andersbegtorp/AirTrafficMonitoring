using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightAnalyzer
{
    [TestFixture()]
    public class IT6_CourseAnalyzer_FlightAnalyzer
    {
        private ICompassCalculator _compassCalculator;
        private ICourseAnalyzer _courseAnalyzer;
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;
        private IVelocityCalculator _velocityCalculator;
        private IVelocityAnalyzer _velocityAnalyzer;
        private IFlightManagement _fakeFlightManagement;
        private IFlightAnalyzer _flightAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
            _velocityAnalyzer = new VelocityAnalyzer(_velocityCalculator);
            _fakeFlightManagement = Substitute.For<IFlightManagement>();
            _compassCalculator = new CompassCalculator();
            _courseAnalyzer = new CourseAnalyzer(_compassCalculator);
            _flightAnalyzer = new AirTrafficMonitoring.FlightAnalyzer.FlightAnalyzer(_fakeFlightManagement, _courseAnalyzer, _velocityAnalyzer);
        }

        [TestCase(22, 0, -17.4524, 0, 999.848, 359)]
        [TestCase(1, 0, 0, 0, 10, 0)]
        [TestCase(3, 0, 17.4524, 0, 999.848, 1)]
        public void AnalyzeCourse_AnalyzesCourseForMatchingTrack_CourseIsCorrect(int numberOfFlightsInNewestTracks, double x1, double x2, double y1, double y2, double expected)
        {
            List<Track> OldestTracks = new List<Track>();
            List<Track> NewestTracks = new List<Track>();

            Track trackToBeCompared = new Track() { Tag = "ASE2018", XCoordinate = x2, YCoordinate = y2 };
            NewestTracks.Add(trackToBeCompared);

            for (int i = 0; i < numberOfFlightsInNewestTracks - 1; i++)
            {
                Track track = new Track() { Tag = "FlightNo " + i };
                NewestTracks.Add(track);
            }

            Track trackOne = new Track() { Tag = "ASE2018", XCoordinate = x1, YCoordinate = y1 };

            OldestTracks.Add(trackOne);


            FlightMovementEventArgs e = new FlightMovementEventArgs(OldestTracks, NewestTracks);

            _flightAnalyzer.HandleFlightsInAirspace(this, e);

            Assert.That(NewestTracks.Find(x => x.Tag == trackOne.Tag).CompassCourse, Is.EqualTo(expected).Within(0.5));
        }

    }
}

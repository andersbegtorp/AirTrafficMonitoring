using System;
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
    public class IT4_VelocityAnalyzer_FlightAnalyzer
    {

        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;
        private IVelocityCalculator _velocityCalculator;
        private IVelocityAnalyzer _velocityAnalyzer;
        private ICourseAnalyzer _fakeCourseAnalyzer;
        private IFlightManagement _fakeFlightManagement;
        private IFlightAnalyzer _flightAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
            _velocityAnalyzer = new VelocityAnalyzer(_velocityCalculator);
            _fakeCourseAnalyzer = Substitute.For<ICourseAnalyzer>();
            _fakeFlightManagement = Substitute.For<IFlightManagement>();
            _flightAnalyzer = new AirTrafficMonitoring.FlightAnalyzer.FlightAnalyzer(_fakeFlightManagement, _fakeCourseAnalyzer, _velocityAnalyzer);
        }

        [TestCase(2, 1, 11000, 12000, 13000, 14000, 1414.214)]
        [TestCase(4, 100, 45000, 30000, 15000, 25000, 180.27)]
        [TestCase(1, 12, 0, 0, 0, 0, 0)]
        [TestCase(12, 2, 25000, 25005, 13000, 13010, 5.59)]
        public void HandleFlightsInAirspace_AnalyzesVelocityForOneMatchingTrack_VelocityIsCorrect(int numberOfFlightsInNewestTracks, int secondsBetweenTimestamps, double firstXCoordinate, double secondXCoordinate, double firstYCoordinate, double secondYCoordinate, double expectedVelocity)
        {
            List<Track> OldestTracks = new List<Track>();
            List<Track> NewestTracks = new List<Track>();
            DateTime firstDateTime = DateTime.Now;
            DateTime secondDateTime = firstDateTime.AddSeconds(secondsBetweenTimestamps);

            Track trackToBeCompared = new Track() { Tag = "ASE2018", TimeStamp = secondDateTime, XCoordinate = firstXCoordinate, YCoordinate = firstYCoordinate };
            NewestTracks.Add(trackToBeCompared);
            for (int i = 0; i < numberOfFlightsInNewestTracks - 1; i++)
            {
                Track track = new Track() { Tag = "FlightNo " + i };
                NewestTracks.Add(track);
            }

            Track trackOne = new Track() { Tag = "ASE2018", TimeStamp = firstDateTime, XCoordinate = secondXCoordinate, YCoordinate = secondYCoordinate };

            OldestTracks.Add(trackOne);

            FlightMovementEventArgs e = new FlightMovementEventArgs(OldestTracks, NewestTracks);

            _flightAnalyzer.HandleFlightsInAirspace(this, e);

            Assert.That(NewestTracks.Find(x => x.Tag == trackOne.Tag).HorizontalVelocity, Is.EqualTo(expectedVelocity).Within(0.5));
        }



        [TestCase(2, 1, 2, 11000, 12000, 13000, 14000, 1414.214, 707.1)]
        [TestCase(4, 100, 50, 45000, 30000, 15000, 25000, 180.27, 360.54)]
        [TestCase(1, 12, 14, 0, 0, 0, 0, 0, 0)]
        [TestCase(12, 2, 8, 25000, 25005, 13000, 13010, 5.59, 1.39)]
        public void AnalyzeVelocity_AnalyzesVelocityForTwoTracks_EachVelocityIsCorrect(int numberOfFlightsInNewestTracks, int secondsBetween1, int secondsBetween2, int firstXCoordinate, int secondXCoordinate, int firstYCoordinate, int secondYCoordinate, double expectedVelocity1, double expectedVelocity2)
        {
            //Arrange
            List<Track> OldestTracks = new List<Track>();
            List<Track> NewestTracks = new List<Track>();
            DateTime firstDateTime = DateTime.Now;
            DateTime secondDateTime1 = firstDateTime.AddSeconds(secondsBetween1);
            DateTime secondDateTime2 = firstDateTime.AddSeconds(secondsBetween2);

            Track trackToBeCompared1 = new Track() { Tag = "ASE2018", TimeStamp = secondDateTime1, XCoordinate = firstXCoordinate, YCoordinate = firstYCoordinate };
            Track trackToBeCompared2 = new Track() { Tag = "ASE2017", TimeStamp = secondDateTime2, XCoordinate = firstXCoordinate, YCoordinate = firstYCoordinate };

            NewestTracks.Add(trackToBeCompared1);
            NewestTracks.Add(trackToBeCompared2);

            for (int i = 0; i < numberOfFlightsInNewestTracks - 2; i++)
            {
                Track track = new Track() { Tag = "FlightNo " + i };
                NewestTracks.Add(track);
            }

            Track trackOne = new Track() { Tag = "ASE2018", TimeStamp = firstDateTime, XCoordinate = secondXCoordinate, YCoordinate = secondYCoordinate };
            Track trackTwo = new Track() { Tag = "ASE2017", TimeStamp = firstDateTime, XCoordinate = secondXCoordinate, YCoordinate = secondYCoordinate };

            OldestTracks.Add(trackOne);
            OldestTracks.Add(trackTwo);

            FlightMovementEventArgs e = new FlightMovementEventArgs(OldestTracks, NewestTracks);

            _flightAnalyzer.HandleFlightsInAirspace(this, e);

            Assert.That(NewestTracks.Find(x => x.Tag == trackOne.Tag).HorizontalVelocity, Is.EqualTo(expectedVelocity1).Within(0.5));
            Assert.That(NewestTracks.Find(x => x.Tag == trackTwo.Tag).HorizontalVelocity, Is.EqualTo(expectedVelocity2).Within(0.5));

        }
    }
}

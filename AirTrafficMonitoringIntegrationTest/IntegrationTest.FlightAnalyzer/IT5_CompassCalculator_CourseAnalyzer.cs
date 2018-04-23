using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightAnalyzer
{
    [TestFixture()]
    public class IT5_CompassCalculator_CourseAnalyzer
    {
        private ICompassCalculator _compassCalculator;
        private ICourseAnalyzer _courseAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _compassCalculator = new CompassCalculator();
            _courseAnalyzer = new CourseAnalyzer(_compassCalculator);
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


            _courseAnalyzer.AnalyzeCourse(OldestTracks, NewestTracks);

            Assert.That(NewestTracks.Find(x => x.Tag == trackOne.Tag).CompassCourse, Is.EqualTo(expected).Within(0.5));
        }


        [TestCase(2, 0, 1.74, 0, -99.98, 179, 359)]
        [TestCase(3, 0, 0, 0, -10, 180, 0)]
        [TestCase(22, 0, -1.74, 0, -99.98, 181, 1)]
        public void AnalyzeVelocity_AnalyzesVelocityForTwoTracks_EachVelocityIsCorrect(int numberOfFlightsInNewestTracks, double x1, double x2, double y1, double y2, double expected1, double expected2)
        {
            //Arrange
            List<Track> OldestTracks = new List<Track>();
            List<Track> NewestTracks = new List<Track>();


            Track trackToBeCompared1 = new Track() { Tag = "ASE2018", XCoordinate = x2, YCoordinate = y2 };
            Track trackToBeCompared2 = new Track() { Tag = "ASE2017", XCoordinate = x1, YCoordinate = y1 };

            NewestTracks.Add(trackToBeCompared1);
            NewestTracks.Add(trackToBeCompared2);

            for (int i = 0; i < numberOfFlightsInNewestTracks - 2; i++)
            {
                Track track = new Track() { Tag = "FlightNo " + i };
                NewestTracks.Add(track);
            }

            Track trackOne = new Track() { Tag = "ASE2018", XCoordinate = x1, YCoordinate = y1 };
            Track trackTwo = new Track() { Tag = "ASE2017", XCoordinate = x2, YCoordinate = y2 };

            OldestTracks.Add(trackOne);
            OldestTracks.Add(trackTwo);



            _courseAnalyzer.AnalyzeCourse(OldestTracks, NewestTracks);

            Assert.That(NewestTracks.Find(x => x.Tag == trackOne.Tag).CompassCourse, Is.EqualTo(expected1).Within(0.5));
            Assert.That(NewestTracks.Find(x => x.Tag == trackTwo.Tag).CompassCourse, Is.EqualTo(expected2).Within(0.5));
        }
    }
}

using System;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightAnalyzer
{
    [TestFixture()]
    public class IT2_DistanceCalculator_VelocityCalculator
    {
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;
        private IVelocityCalculator _velocityCalculator;

        [SetUp]
        public void SetUp()
        {
            _timeSpanCalculator = new TimeSpanCalculator();
            _distanceCalculator = new DistanceCalculator();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _distanceCalculator);
        }

        [TestCase(1, 11000, 12000, 13000, 14000, 1414.214)]
        [TestCase(100, 45000, 30000, 15000, 25000, 180.27)]
        [TestCase(12, 0, 0, 0, 0, 0)]
        [TestCase(2, 25000, 25005, 13000, 13010, 5.59)]
        [TestCase(33,12345, 23456, 34567, 45678, 476.16)]
        public void CalculateVelocity_CalculatesVelocity_VelocityIsCorrect(int secondsBetweenTimestamps, int firstXCoordinate, int secondXCoordinate, int firstYCoordinate, int secondYCoordinate, double expectedVelocity)
        {
            DateTime firstDateTime = DateTime.Now;
            DateTime secondDateTime = firstDateTime.AddSeconds(secondsBetweenTimestamps);
            Track trackOne = new Track() { TimeStamp = firstDateTime, XCoordinate = firstXCoordinate, YCoordinate = firstYCoordinate};
            Track trackTwo = new Track() { TimeStamp = secondDateTime, XCoordinate = secondXCoordinate, YCoordinate = secondYCoordinate};

            _velocityCalculator.CalculateVelocity(trackOne, trackTwo);

            Assert.That(trackTwo.HorizontalVelocity, Is.EqualTo(expectedVelocity).Within(0.5));
        }
    }
}

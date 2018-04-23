using System;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.FlightAnalyzer
{
    [TestFixture()]
    public class IT1_TimeSpanCalculator_DistanceCalculator
    {
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _fakeDistanceCalculator;
        private IVelocityCalculator _velocityCalculator;

        [SetUp]
        public void SetUp()
        {
            _timeSpanCalculator = new TimeSpanCalculator();
            _fakeDistanceCalculator = Substitute.For<IDistanceCalculator>();
            _velocityCalculator = new VelocityCalculator(_timeSpanCalculator, _fakeDistanceCalculator);
        }

        [TestCase(100, 1, 100)]
        [TestCase(1, 1, 1)]
        [TestCase(0, 1, 0)]
        [TestCase(100, 2, 50)]
        public void CalculateVelocity_CalculatesVelocity_VelocityIsCorrect(int distance, int secondsBetweenTimestamps, double expectedVelocity)
        {
            _fakeDistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(distance);
            DateTime firstDateTime = DateTime.Now;
            DateTime secondDateTime = firstDateTime.AddSeconds(secondsBetweenTimestamps);
            Track trackOne = new Track() {TimeStamp = firstDateTime};
            Track trackTwo = new Track() {TimeStamp = secondDateTime};

            _velocityCalculator.CalculateVelocity(trackOne, trackTwo);

            Assert.That(trackTwo.HorizontalVelocity, Is.EqualTo(expectedVelocity).Within(0.5));
        }
    }
}

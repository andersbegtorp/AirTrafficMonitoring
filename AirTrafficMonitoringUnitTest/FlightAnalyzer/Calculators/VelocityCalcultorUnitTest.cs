using System;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.FlightAnalyzer.Calculators
{
    class VelocityCalcultorUnitTest
    {
        private ITimeSpanCalculator _fakeTimeSpanCalculator;
        private IDistanceCalculator _fakeDistanceCalculator;
        private VelocityCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeTimeSpanCalculator = Substitute.For<ITimeSpanCalculator>();
            _fakeDistanceCalculator = Substitute.For<IDistanceCalculator>();
            _uut = new VelocityCalculator(_fakeTimeSpanCalculator, _fakeDistanceCalculator);
        }

        [TestCase(10,100,10)]
        [TestCase(1000, 5000, 5)]
        [TestCase(1, 100, 100)]
        [TestCase(20000, 30000, 1.5)]
        [TestCase(10, 2, 0.2)]

        public void CalculateVelocity_CalculatingVelocity_VelocityIsCorrect(int seconds, int meters, double expectedVelocity)
        {
            var timeSpan = new TimeSpan(0,0,seconds);
            _fakeTimeSpanCalculator.CalculateTimeDifference(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(timeSpan);
            _fakeDistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(meters);
            Track newTrack = new Track();
            Track oldTrack = new Track();
            
            _uut.CalculateVelocity(oldTrack, newTrack);
            Assert.That(newTrack.HorizontalVelocity,Is.EqualTo(expectedVelocity));
        }        
    }
}

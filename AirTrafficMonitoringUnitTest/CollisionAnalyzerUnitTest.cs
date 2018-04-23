using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    class CollisionAnalyzerUnitTest
    {
        private CollisionAnalyzer _uut;
        private IDistanceCalculator _fakeDistanceCalculator;
        private IAltitudeDistanceCalculator _fakeAltitudeDistanceCalculator;

        [SetUp]
        public void SetUp()
        {
            _fakeDistanceCalculator = Substitute.For<IDistanceCalculator>();
            _fakeAltitudeDistanceCalculator = Substitute.For<IAltitudeDistanceCalculator>();
            _uut = new CollisionAnalyzer(_fakeDistanceCalculator,_fakeAltitudeDistanceCalculator);
        }

        [TestCase(4999, 299)]
        [TestCase(-1, 299)]
        [TestCase(4999, -1)]
        public void AnalyzeCollision_AnalyzesCollisionBetweenTwoFlights_ReturnsTrue(int distance, int altitude)
        {

            _fakeDistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(distance);
            _fakeAltitudeDistanceCalculator.CalculateAltitudeDistance(Arg.Any<int>(), Arg.Any<int>()).Returns(altitude);
                
                //Assert
            Assert.That(_uut.AnalyzeCollision(new Track(), new Track()), Is.EqualTo(true));
        }

        [TestCase(5000, 299)]
        [TestCase(4999, 300)]
        [TestCase(-1, 300)]
        [TestCase(5000, -1)]

        public void AnalyzeCollision_AnalyzesCollisionBetweenTwoFlights_ReturnsFalse(int distance, int altitude)
        {

            _fakeDistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(distance);
            _fakeAltitudeDistanceCalculator.CalculateAltitudeDistance(Arg.Any<int>(), Arg.Any<int>()).Returns(altitude);

            //Assert
            Assert.That(_uut.AnalyzeCollision(new Track(), new Track()), Is.EqualTo(false));
        }

    }
}

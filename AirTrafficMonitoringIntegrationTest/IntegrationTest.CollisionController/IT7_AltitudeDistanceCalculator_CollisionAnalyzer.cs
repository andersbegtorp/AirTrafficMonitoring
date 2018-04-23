using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.CollisionController
{
    [TestFixture()]
    public class IT7_AltitudeDistanceCalculator_CollisionAnalyzer
    {
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
        private IDistanceCalculator _fakedistanceCalculator;
        private ICollisionAnalyzer _collisionAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _fakedistanceCalculator = Substitute.For<IDistanceCalculator>();
            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_fakedistanceCalculator, _altitudeDistanceCalculator);
        }

        [TestCase(4999, 500, 300)]
        [TestCase(4999, 500, 201)]
        [TestCase(4999, 700, 500)]
        public void AnalyzeCollision_PossibleCollision_ReturnsTrue(int distance, int altitude1, int altitude2)
        {
            _fakedistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(distance);

            Track trackOne = new Track() {Altitude = altitude1};
            Track trackTwo = new Track() {Altitude = altitude2};

            Assert.That(_collisionAnalyzer.AnalyzeCollision(trackOne, trackTwo), Is.EqualTo(true));
        }

        [TestCase(4999, 500, 100)]
        [TestCase(4999, 500, 40)]
        [TestCase(4999, 700, 399)]
        public void AnalyzeCollision_PossibleCollision_ReturnsFalse(int distance, int altitude1, int altitude2)
        {
            _fakedistanceCalculator
                .CalculateDistance(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .Returns(distance);

            Track trackOne = new Track() { Altitude = altitude1 };
            Track trackTwo = new Track() { Altitude = altitude2 };

            Assert.That(_collisionAnalyzer.AnalyzeCollision(trackOne, trackTwo), Is.EqualTo(false));
        }
    }
}

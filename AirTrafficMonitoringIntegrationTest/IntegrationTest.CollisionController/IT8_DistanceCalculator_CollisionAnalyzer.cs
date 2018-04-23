using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.CollisionController
{
    [TestFixture()]
    public class IT8_DistanceCalculator_CollisionAnalyzer
    {
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
        private IDistanceCalculator _distanceCalculator;
        private ICollisionAnalyzer _collisionAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = new DistanceCalculator();
            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
        }

        [TestCase(500, 300, 11000, 12000, 13000, 14000)]
        [TestCase(500, 300, 0, 2000, 0, 4581)]
        [TestCase(500, 300, 0, 1801, 0, 4664)]
        public void AnalyzeCollision_PossibleCollision_ReturnsTrue(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {

            Track trackOne = new Track() { Altitude = altitude1,  XCoordinate = x1, YCoordinate =  y1,};
            Track trackTwo = new Track() { Altitude = altitude2 , XCoordinate = x2, YCoordinate = y2};

            Assert.That(_collisionAnalyzer.AnalyzeCollision(trackOne, trackTwo), Is.EqualTo(true));
        }


        [TestCase(500, 300, 110000, 12000, 13000, 14000)]
        [TestCase(500, 0, 0, 20000, 0, 4581)]
        [TestCase(500, 300, 0, 18061, 0, 46664)]
        public void AnalyzeCollision_PossibleCollision_ReturnsFalse(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {

            Track trackOne = new Track() { Altitude = altitude1, XCoordinate = x1, YCoordinate = y1, };
            Track trackTwo = new Track() { Altitude = altitude2, XCoordinate = x2, YCoordinate = y2 };

            Assert.That(_collisionAnalyzer.AnalyzeCollision(trackOne, trackTwo), Is.EqualTo(false));
        }

    }
}

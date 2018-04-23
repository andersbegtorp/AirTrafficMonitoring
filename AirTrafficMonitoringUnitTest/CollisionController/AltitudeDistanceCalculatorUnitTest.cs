using AirTrafficMonitoring.CollisionController;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.CollisionController
{
    [TestFixture]
    class AltitudeDistanceCalculatorUnitTest
    {
        private AltitudeDistanceCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new AltitudeDistanceCalculator();
        }

        [TestCase(2000,1000,1000)]
        [TestCase(2000, 2000, 0)]
        [TestCase(0, 2000, 2000)]
        [TestCase(2000, 0, 2000)]
        public void CalculateAltitudeDistance_CalculatingAltitudeDistance_DistanceIsCorrect(int altitude1,
            int altitude2, int expextedDistance)
        {
            Assert.That(_uut.CalculateAltitudeDistance(altitude1, altitude2), Is.EqualTo(expextedDistance));
        }
    }
}

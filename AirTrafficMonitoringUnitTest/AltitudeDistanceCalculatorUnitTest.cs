using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
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

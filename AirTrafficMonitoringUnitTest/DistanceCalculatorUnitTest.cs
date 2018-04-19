using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    class DistanceCalculatorUnitTest
    {
        private DistanceCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DistanceCalculator();
        }

        [TestCase(11000, 12000, 13000, 14000, 1414.214)]
        [TestCase(45000, 30000, 15000, 25000, 18027.756)]
        [TestCase(10000, 90000, 10000, 90000, 113137.085)]
        [TestCase(25000, 25005, 13000, 13010, 11.18)]
        [TestCase(12345, 23456, 34567, 45678, 15713.327)]
        public void CalculateDistance_CalculatingDistance_DistanceIsCorrect(double x1, double x2, double y1, double y2, double distance)
        {
            Assert.That(_uut.CalculateDistance(x1, x2, y1, y2), Is.EqualTo(distance).Within(0.5));
        }
    }
}

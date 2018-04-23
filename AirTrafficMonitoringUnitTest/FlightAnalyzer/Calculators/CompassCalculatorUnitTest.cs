using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.FlightAnalyzer.Calculators
{
    [TestFixture]
    class CompassCalculatorUnitTest
    {
        private CompassCalculator _uut;
        [SetUp]
        public void SetUp()
        {
            _uut = new CompassCalculator();
        }

        //BVA for Nord
        [TestCase(0, -17.4524, 0, 999.848, 359)]
        [TestCase(0, 0, 0, 10, 0)]
        [TestCase(0, 17.4524, 0, 999.848, 1)]
        //BVA for Øst
        [TestCase(0, 99.98, 0, 1.74, 89)]
        [TestCase(0, 100, 0, 0, 90)]
        [TestCase(0, 99.98, 0, -1.74, 91)]
        //BVA for Syd
        [TestCase(0, 1.74, 0, -99.98, 179)]
        [TestCase(0, 0, 0, -10, 180)]
        [TestCase(0, -1.74, 0, -99.98, 181)]
        //BVA for Vest
        [TestCase(0, -99.98, 0, -1.74, 269)]
        [TestCase(0, -100, 0, 0, 270)]
        [TestCase(0, -99.98, 0, 1.74, 271)]
        public void CalculateCourse_CalculatesCourse_CourseIsCorrect(double x1,double x2, double y1,double y2,double expected)
        {
            Assert.That(_uut.CalculateCourse(x1,x2,y1,y2),Is.EqualTo(expected).Within(0.5));
        }
    }
}

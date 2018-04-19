using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    class TimeSpanCalculatorUnitTest
    {
        private TimeSpanCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TimeSpanCalculator();
        }

        [TestCase(2015,10,06,21,34,56,789, 39)]
        [TestCase(2015, 11, 06, 21, 34, 56, 789, 39)]
        [TestCase(2015, 11, 07, 21, 34, 56, 789, 20)]
        [TestCase(2015, 11, 07, 22, 34, 56, 789, 12)]
        [TestCase(2015, 11, 07, 23, 35, 58, 345, 35)]
        public void CalculateTimeSpan_CalculateDifferenceInSeconds_TimeDifferenceIsCorrect(int year, int month, int day, int hour, int minutes, int seconds, int miliseconds, int timeDifferenceInSeconds)
        {
            DateTime t1 = new DateTime(year, month, day, hour, minutes, seconds, miliseconds);
            DateTime t2 = new DateTime();
            t2 = t1.AddSeconds(timeDifferenceInSeconds);
            Assert.That(_uut.CalculateTimeDifference(t1, t2).TotalSeconds,Is.EqualTo(timeDifferenceInSeconds));
        }
    }
}

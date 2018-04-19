using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class TimeSpanCalculator : ITimeSpanCalculator
    {
        public TimeSpan CalculateTimeDifference(DateTime t1, DateTime t2)
        {
            TimeSpan timeSpan = t2 - t1;
            return timeSpan;
        }
    }
}
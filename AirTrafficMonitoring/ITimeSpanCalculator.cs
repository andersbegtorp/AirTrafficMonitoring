using System;

namespace AirTrafficMonitoring
{
    public interface ITimeSpanCalculator
    {
        TimeSpan CalculateTimeDifference(DateTime t1, DateTime t2);
    }
}
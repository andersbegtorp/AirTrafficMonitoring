using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface ITimeSpanCalculator
    {
        TimeSpan CalculateTimeDifference(DateTime t1, DateTime t2);
    }
}
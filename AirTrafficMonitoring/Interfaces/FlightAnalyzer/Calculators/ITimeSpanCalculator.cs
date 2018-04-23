using System;

namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators
{
    public interface ITimeSpanCalculator
    {
        TimeSpan CalculateTimeDifference(DateTime t1, DateTime t2);
    }
}
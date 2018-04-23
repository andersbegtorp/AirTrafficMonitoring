﻿namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators
{
    public interface ICompassCalculator
    {
        double CalculateCourse(double x1, double x2, double y1, double y2);
    }
}

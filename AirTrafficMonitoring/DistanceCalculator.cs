using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double CalculateDistance(double x1, double x2, double y1, double y2)
        {
            var deltaX = x2 - x1;
            var deltaY = y2 - y1;

            return Math.Sqrt(Math.Pow(deltaY, 2) + Math.Pow(deltaX, 2));
        }
    }
}
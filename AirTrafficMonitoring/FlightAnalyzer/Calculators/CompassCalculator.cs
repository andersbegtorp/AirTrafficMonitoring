﻿using System;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;

namespace AirTrafficMonitoring.FlightAnalyzer.Calculators
{
    public class CompassCalculator : ICompassCalculator
    {
        public double CalculateCourse(double x1, double x2, double y1, double y2)
        {
            var deltaX = x2 - x1;
            var deltaY = y2 - y1;

            var theta = Math.Atan2(deltaX, deltaY);

            var angle = ((theta * 180 / Math.PI) + 360 % 360);

            if (angle < 0)
                angle = 360 + angle;
            return Math.Round(angle,1);
        }
    }
}

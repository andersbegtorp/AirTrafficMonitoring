using System;
using AirTrafficMonitoring.Interfaces.CollisionController;

namespace AirTrafficMonitoring.CollisionController
{
    public class AltitudeDistanceCalculator : IAltitudeDistanceCalculator
    {
        public int CalculateAltitudeDistance(int altitude1, int altitude2)
        {
            return Math.Abs(altitude1 - altitude2);
        }
    }
}
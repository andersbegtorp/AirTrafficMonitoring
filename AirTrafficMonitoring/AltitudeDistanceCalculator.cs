using System;

namespace AirTrafficMonitoring
{
    public class AltitudeDistanceCalculator : IAltitudeDistanceCalculator
    {
        public int CalculateAltitudeDistance(int altitude1, int altitude2)
        {
            return Math.Abs(altitude1 - altitude2);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class VelocityCalculator : IVelocityCalculator

    {
        private ITimeSpanCalculator _timeSpanCalculator;
        private IDistanceCalculator _distanceCalculator;

        public VelocityCalculator(ITimeSpanCalculator timeSpanCalculator, IDistanceCalculator distanceCalculator)
        {
            _timeSpanCalculator = timeSpanCalculator;
            _distanceCalculator = distanceCalculator;

        }

        public void CalculateVelocity(Track oldTrack, Track newTrack)
        {
            newTrack.HorizontalVelocity = CalculateVelocity(_distanceCalculator.CalculateDistance(oldTrack.XCoordinate, newTrack.XCoordinate, oldTrack.YCoordinate, newTrack.YCoordinate), _timeSpanCalculator.CalculateTimeDifference(oldTrack.TimeStamp, newTrack.TimeStamp));

        }


        private double CalculateVelocity(double distance, TimeSpan time)
        {
            return distance / time.TotalSeconds;
        }
    }

    
}

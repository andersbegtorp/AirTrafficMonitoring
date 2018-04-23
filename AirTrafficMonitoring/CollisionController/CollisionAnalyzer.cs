using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;

namespace AirTrafficMonitoring
{
    public class CollisionAnalyzer : ICollisionAnalyzer
    {
        private IDistanceCalculator _horizontaDistanceCalculator;
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;

        public CollisionAnalyzer(IDistanceCalculator horizontaDistanceCalculator, IAltitudeDistanceCalculator altitudeDistanceCalculator)
        {
            _horizontaDistanceCalculator = horizontaDistanceCalculator;
            _altitudeDistanceCalculator = altitudeDistanceCalculator;
        }

        public bool AnalyzeCollision(Track flight1, Track flight2)
        {

            if (_horizontaDistanceCalculator.CalculateDistance(flight1.XCoordinate,flight2.XCoordinate,
                   flight1.YCoordinate, flight2.YCoordinate) < 5000 &&
                _altitudeDistanceCalculator.CalculateAltitudeDistance(flight1.Altitude, flight2.Altitude) < 300)
            {
                return true;
            }
            return false;
        }
    }
}

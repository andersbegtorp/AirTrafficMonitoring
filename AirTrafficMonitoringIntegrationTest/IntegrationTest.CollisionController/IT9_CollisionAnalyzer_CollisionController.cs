using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.CollisionController
{
    [TestFixture()]
    public class IT9_CollisionAnalyzer_CollisionController
    {
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
        private IDistanceCalculator _distanceCalculator;
        private ICollisionAnalyzer _collisionAnalyzer;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = new DistanceCalculator();
            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
        }
    }
}

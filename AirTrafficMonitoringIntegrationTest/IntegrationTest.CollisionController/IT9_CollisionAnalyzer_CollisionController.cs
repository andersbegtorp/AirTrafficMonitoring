using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using AirTrafficMonitoring.Interfaces.Logger;
using NSubstitute;
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
        private ISeparationStringBuilder _fakeSeparationStringBuilder;
        private IFlightManagement _fakeFlightManagement;
        private ICollisionController _collisionController;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = new DistanceCalculator();
            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
            _fakeSeparationStringBuilder = Substitute.For<ISeparationStringBuilder>();
            _fakeFlightManagement = Substitute.For<IFlightManagement>();
            _collisionController = new AirTrafficMonitoring.CollisionController.CollisionController(_fakeFlightManagement, _collisionAnalyzer, _fakeSeparationStringBuilder);
            
        }


        [TestCase(500, 300, 11000, 12000, 13000, 14000)]
        [TestCase(500, 300, 0, 2000, 0, 4581)]
        [TestCase(500, 300, 0, 1801, 0, 4664)]
        public void HandleFlightsInAirspace_HandlesFlights_EventWasRaised(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {

            Track trackOne = new Track() { Altitude = altitude1, XCoordinate = x1, YCoordinate = y1, };
            Track trackTwo = new Track() { Altitude = altitude2, XCoordinate = x2, YCoordinate = y2 };

            List<Track> NewestTracks = new List<Track>();
            NewestTracks.Add(trackOne);
            NewestTracks.Add(trackTwo);

            int raises = 0;
            _collisionController.SeperationEvent += (sender, args) => raises++;

            _collisionController.HandleFlightsInAirspace(this, new FlightMovementEventArgs(new List<Track>(), NewestTracks ));

            _fakeSeparationStringBuilder.Received(1).BuildSeperationNote(trackOne, trackTwo);

        }

        [TestCase(500, 300, 110000, 12000, 13000, 14000)]
        [TestCase(500, 0, 0, 20000, 0, 4581)]
        [TestCase(500, 300, 0, 18061, 0, 46664)]
        public void HandleFlightsInAirspace_HandlesFlights_EventWasNotRaised(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {

            Track trackOne = new Track() { Altitude = altitude1, XCoordinate = x1, YCoordinate = y1, };
            Track trackTwo = new Track() { Altitude = altitude2, XCoordinate = x2, YCoordinate = y2 };

            List<Track> NewestTracks = new List<Track>();
            NewestTracks.Add(trackOne);
            NewestTracks.Add(trackTwo);

            int raises = 0;
            _collisionController.SeperationEvent += (sender, args) => raises++;

            _collisionController.HandleFlightsInAirspace(this, new FlightMovementEventArgs(new List<Track>(), NewestTracks));

            _fakeSeparationStringBuilder.DidNotReceive().BuildSeperationNote(trackOne, trackTwo);

        }
    }
}

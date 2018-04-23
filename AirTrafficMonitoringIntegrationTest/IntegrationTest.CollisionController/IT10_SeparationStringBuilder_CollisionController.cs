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

namespace AirTrafficMonitoringIntegrationTest.IntegrationTest.CollisionController
{
    public class IT10_SeparationStringBuilder_CollisionController
    {

        [TestFixture()]
        public class IT9_CollisionAnalyzer_CollisionController
        {
            private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
            private IDistanceCalculator _distanceCalculator;
            private ICollisionAnalyzer _collisionAnalyzer;
            private ISeparationStringBuilder _separationStringBuilder;
            private IFlightManagement _fakeFlightManagement;
            private ICollisionController _collisionController;

            [SetUp]
            public void SetUp()
            {
                _distanceCalculator = new DistanceCalculator();
                _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
                _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
                _separationStringBuilder = new SeparationStringBuilder();
                _fakeFlightManagement = Substitute.For<IFlightManagement>();
                _collisionController = new AirTrafficMonitoring.CollisionController.CollisionController(_fakeFlightManagement, _collisionAnalyzer, _separationStringBuilder);

            }


            [TestCase(500, 300, 11000, 12000, 13000, 14000)]
            [TestCase(500, 300, 0, 2000, 0, 4581)]
            [TestCase(500, 300, 0, 1801, 0, 4664)]
            public void HandleFlightsInAirspace_HandlesFlights_EventWasRaised(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
            {

                DateTime timeStamp = DateTime.Now;
                Track trackOne = new Track() { Altitude = altitude1, XCoordinate = x1, YCoordinate = y1, TimeStamp = timeStamp, Tag = "Test1"};
                Track trackTwo = new Track() { Altitude = altitude2, XCoordinate = x2, YCoordinate = y2, TimeStamp = timeStamp, Tag = "Test2"};
               

                List<Track> NewestTracks = new List<Track>();
                NewestTracks.Add(trackOne);
                NewestTracks.Add(trackTwo);

                string expectedNote = "Timestamp: " + timeStamp.ToString() + " Flight: "
                                      + trackOne.Tag + " is on collision with flight: " + trackTwo.Tag; ;
                string actualNote = "";
                _collisionController.SeperationEvent += (sender, args) => actualNote = args.SeperationNote;

                _collisionController.HandleFlightsInAirspace(this, new FlightMovementEventArgs(new List<Track>(), NewestTracks));

                StringAssert.IsMatch(expectedNote, actualNote);

            }

        }
    }
}

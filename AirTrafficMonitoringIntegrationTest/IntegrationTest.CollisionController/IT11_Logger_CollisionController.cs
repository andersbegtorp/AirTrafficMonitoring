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

    [TestFixture()]
    public class IT11_Logger_CollisionController
    {
        private IAltitudeDistanceCalculator _altitudeDistanceCalculator;
        private IDistanceCalculator _distanceCalculator;
        private ICollisionAnalyzer _collisionAnalyzer;
        private ISeparationStringBuilder _separationStringBuilder;
        private IFlightManagement _fakeFlightManagement;
        private ISeparationEventLogger _logger;
        private IFileWriter _fakeFileWriter;
        private ICollisionController _collisionController;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = new DistanceCalculator();
            _altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            _collisionAnalyzer = new CollisionAnalyzer(_distanceCalculator, _altitudeDistanceCalculator);
            _separationStringBuilder = new SeparationStringBuilder();
            _fakeFlightManagement = Substitute.For<IFlightManagement>();   
            _fakeFileWriter = Substitute.For<IFileWriter>();
            _collisionController = new AirTrafficMonitoring.CollisionController.CollisionController(_fakeFlightManagement, _collisionAnalyzer, _separationStringBuilder);
            _logger = new Logger(_collisionController, "", _fakeFileWriter);
        }

        [TestCase(500, 300, 11000, 12000, 13000, 14000)]
        [TestCase(500, 300, 0, 2000, 0, 4581)]
        [TestCase(500, 300, 0, 1801, 0, 4664)]
        public void HandleFlightsInAirSpace_SeparationEventGetsLogged_EventWasLogged(int altitude1, int altitude2, double x1, double x2, double y1, double y2)
        {
            DateTime timeStamp = DateTime.Now;
            Track trackOne = new Track() { Altitude = altitude1, XCoordinate = x1, YCoordinate = y1, TimeStamp = timeStamp, Tag = "Test1" };
            Track trackTwo = new Track() { Altitude = altitude2, XCoordinate = x2, YCoordinate = y2, TimeStamp = timeStamp, Tag = "Test2" };


            List<Track> NewestTracks = new List<Track>();
            NewestTracks.Add(trackOne);
            NewestTracks.Add(trackTwo);

            string expectedNote = "Timestamp: " + timeStamp.ToString() + " Flight: "
                                  + trackOne.Tag + " is on collision with flight: " + trackTwo.Tag; 

            _collisionController.HandleFlightsInAirspace(this, new FlightMovementEventArgs(new List<Track>(), NewestTracks));

            _fakeFileWriter.Received(1).WriteToFile("", expectedNote);
        }
    }
}

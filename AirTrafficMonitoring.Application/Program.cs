using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.AirspaceController;
using AirTrafficMonitoring.CollisionController;
using AirTrafficMonitoring.Configuration.AirspaceConfiguration;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Display;
using AirTrafficMonitoring.Factory;
using AirTrafficMonitoring.FlightAnalyzer;
using AirTrafficMonitoring.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.FlightManagement;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.Factory;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;
using AirTrafficMonitoring.Interfaces.FlightManagement;
using AirTrafficMonitoring.Interfaces.Logger;
using AirTrafficMonitoring.Interfaces.Transponder;
using AirTrafficMonitoring.Transponder;
using TransponderReceiver;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {

            var AirspaceConfiguration = XMLAirspaceConfiguration.LoadAirspace(@"../../../AirspaceConfiguration.xml");

            Airspace airspace = new Airspace();
            airspace.HighestAltitude = AirspaceConfiguration.HighestAltitude;
            airspace.LowestAltitude = AirspaceConfiguration.LowestAltitude;
            airspace.NorthEastXCoordinate = AirspaceConfiguration.NorthEastXCoordinate;
            airspace.NorthEastYCoordinate = AirspaceConfiguration.NorthEastYCoordinate;
            airspace.SouthWestXCoordinate = AirspaceConfiguration.SouthWestXCoordinate;
            airspace.SouthWestYCoordinate = AirspaceConfiguration.SouthWestYCoordinate;

            ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            ITrackFactory trackFactory = new TrackFactory();
            ITransponderDataReciever transponderDataReciever = new TransponderDataReceiver(transponderReceiver,trackFactory);
            IAirspaceTrackChecker airspaceTrackChecker = new AirspaceTrackChecker(airspace);
            IAirspaceController airspaceController = new AirspaceController.AirspaceController(transponderDataReciever,airspaceTrackChecker);
            ITrackRemover trackRemover = new TrackRemover();
            ITrackManagement trackManagement = new TrackManagement();
            IFlightManagement flightManagement = new FlightManagement.FlightManagement(airspaceController,trackRemover,trackManagement);
            ICompassCalculator compassCalculator = new CompassCalculator();
            ICourseAnalyzer courseAnalyzer = new CourseAnalyzer(compassCalculator);
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            ITimeSpanCalculator timeSpanCalculator = new TimeSpanCalculator();
            IVelocityCalculator velocityCalculator = new VelocityCalculator(timeSpanCalculator,distanceCalculator);
            IVelocityAnalyzer velocityAnalyzer = new VelocityAnalyzer(velocityCalculator);
            IFlightAnalyzer flightAnalyzer = new FlightAnalyzer.FlightAnalyzer(flightManagement,courseAnalyzer,velocityAnalyzer);
            IAltitudeDistanceCalculator altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            ICollisionAnalyzer collisionAnalyzer = new CollisionAnalyzer(distanceCalculator,altitudeDistanceCalculator);
            ISeparationStringBuilder separationStringBuilder = new SeparationStringBuilder();
            ICollisionController collisionController = new CollisionController.CollisionController(flightManagement,collisionAnalyzer,separationStringBuilder );
            IDisplay display = new ConsoleDisplay(flightAnalyzer, collisionController);
            IFileWriter fileWriter = new FileWriter();
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, "SeparationLog.txt");
            ISeparationEventLogger logger = new CollisionController.Logger(collisionController, path, fileWriter );
            
            Console.ReadLine();

        }
    }
}

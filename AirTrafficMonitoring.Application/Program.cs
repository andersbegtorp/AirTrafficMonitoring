using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Configuration.AirspaceConfiguration;
using AirTrafficMonitoring.Interfaces;
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
            IAirspaceController airspaceController = new AirspaceController(transponderDataReciever,airspaceTrackChecker);
            ITrackRemover trackRemover = new TrackRemover();
            ITrackManagement trackManagement = new TrackManagement();
            IFlightManagement flightManagement = new FlightManagement(airspaceController,trackRemover,trackManagement);
            ICompassCalculator compassCalculator = new CompassCalculator();
            ICourseAnalyzer courseAnalyzer = new CourseAnalyzer(compassCalculator);
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            ITimeSpanCalculator timeSpanCalculator = new TimeSpanCalculator();
            IVelocityCalculator velocityCalculator = new VelocityCalculator(timeSpanCalculator,distanceCalculator);
            IVelocityAnalyzer velocityAnalyzer = new VelocityAnalyzer(velocityCalculator);
            IFlightAnalyzer flightAnalyzer = new FlightAnalyzer(flightManagement,courseAnalyzer,velocityAnalyzer);
            IAltitudeDistanceCalculator altitudeDistanceCalculator = new AltitudeDistanceCalculator();
            ICollisionAnalyzer collisionAnalyzer = new CollisionAnalyzer(distanceCalculator,altitudeDistanceCalculator);
            IFlightController flightController = new FlightController(flightManagement,collisionAnalyzer);
            IDisplay display = new ConsoleDisplay(flightAnalyzer);

            
            Console.ReadLine();

        }
    }
}

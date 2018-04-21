using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightController : IFlightController
    {
        private ICollisionAnalyzer _collisionAnalyzer;

        public FlightController(IFlightManagement flightManagement, ICollisionAnalyzer collisionAnalyzer)
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
            _collisionAnalyzer = collisionAnalyzer;
        }
        public void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg)
        {
            _collisionAnalyzer.AnalyzeCollision(arg.NewestTracks);
        }
    }
}
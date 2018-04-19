using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightController
    {
        public FlightController(IFlightManagement flightManagement)
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
        }

        public void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg)
        {
        }
    }
}
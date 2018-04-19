using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightAnalyzer
    {
        public FlightAnalyzer(IFlightManagement flightManagement)
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
        }

        public void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg)
        {
        }
    }
}
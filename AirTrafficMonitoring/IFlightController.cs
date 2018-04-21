namespace AirTrafficMonitoring
{
    public interface IFlightController
    {
        void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg);
    }
}
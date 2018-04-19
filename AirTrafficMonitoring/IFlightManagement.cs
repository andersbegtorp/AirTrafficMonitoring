using System;

namespace AirTrafficMonitoring
{
    public interface IFlightManagement
    {
        event EventHandler<FlightMovementEventArgs> FlightDataReady;
    }
}
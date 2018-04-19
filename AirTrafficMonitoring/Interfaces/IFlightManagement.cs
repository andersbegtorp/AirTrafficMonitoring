using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightManagement
    {
        event EventHandler<FlightMovementEventArgs> FlightDataReady;
    }
}
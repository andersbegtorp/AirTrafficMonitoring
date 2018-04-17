using System;

namespace AirTrafficMonitoring
{
    public interface IFlightController
    {
        event EventHandler<FlightMovementEventArgs> FlightDataReady;
    }
}
using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.FlightManagement
{
    public interface IFlightManagement
    {
        event EventHandler<FlightMovementEventArgs> FlightDataReady;
    }
}
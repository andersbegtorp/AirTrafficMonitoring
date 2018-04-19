using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public interface IFlightController
    {
        void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg);
        event EventHandler<SeperationEventArgs> SeperationEvent;
    }
}
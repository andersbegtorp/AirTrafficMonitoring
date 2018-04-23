using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.CollisionController

{
    public interface ICollisionController
    {
        void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg);
        event EventHandler<SeparationEventArgs> SeperationEvent;

    }
}
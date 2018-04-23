using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.FlightManagement
{
    public interface IFlightManagement
    {
        event EventHandler<FlightMovementEventArgs> FlightDataReady;

        void HandleTrackOutsideAirspace(object sender, TrackEventArgs arg);

        void HandleTrackInsideAirspace(object sender, TrackEventArgs arg);
    }
}
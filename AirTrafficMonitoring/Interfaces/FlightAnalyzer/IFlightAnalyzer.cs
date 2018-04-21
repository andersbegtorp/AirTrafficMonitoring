using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public interface IFlightAnalyzer
    {
        void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg);
        event EventHandler<TrackLogEventArgs> TracksAnalyzedEvent;
    }
}
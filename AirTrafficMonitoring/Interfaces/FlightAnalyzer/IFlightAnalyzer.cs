using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer
{
    public interface IFlightAnalyzer
    {
        void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg);
        event EventHandler<TrackLogEventArgs> TracksAnalyzedEvent;
    }
}
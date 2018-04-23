using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.AirspaceController
{
    public interface IAirspaceController
    {
        event EventHandler<TrackEventArgs> TrackInAirspace;
        event EventHandler<TrackEventArgs> TrackOutsideAirspace;
        void HandleTracks(object o, TracksDataEventArgs arg);
    }
}
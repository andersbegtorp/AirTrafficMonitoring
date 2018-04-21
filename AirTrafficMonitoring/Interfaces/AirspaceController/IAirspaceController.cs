using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IAirspaceController
    {
        event EventHandler<TrackEventArgs> TrackInAirspace;
        event EventHandler<TrackEventArgs> TrackOutsideAirspace;
        void HandleTracks(object o, TracksDataEventArgs arg);
    }
}
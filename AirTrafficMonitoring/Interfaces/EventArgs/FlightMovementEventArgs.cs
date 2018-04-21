using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public class FlightMovementEventArgs : EventArgs
    {
        public List<Track> OldestTracks { get; }
        public List<Track> NewestTracks { get; }
        public FlightMovementEventArgs(List<Track> oldestTracks, List<Track> newestTracks)
        {
            OldestTracks = oldestTracks;
            NewestTracks = newestTracks;
        }
    }
}
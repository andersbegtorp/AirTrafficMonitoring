using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.EventArgs
{
    public class FlightMovementEventArgs : System.EventArgs
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
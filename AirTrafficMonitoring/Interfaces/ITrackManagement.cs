using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface ITrackManagement
    {
        void ManageTrack(List<Track> newestTracks, List<Track> oldestTracks, Track newTrack);
    }
}
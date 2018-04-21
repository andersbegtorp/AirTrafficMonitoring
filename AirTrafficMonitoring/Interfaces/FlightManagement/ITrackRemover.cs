using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface ITrackRemover
    {
        void RemoveTrack(List<Track> tracks, Track trackToBeRemoved);
    }
}
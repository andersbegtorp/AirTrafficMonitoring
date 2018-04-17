using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public interface ITrackRemover
    {
        void RemoveTrack(List<Track> tracks, Track trackToBeRemoved);
    }
}
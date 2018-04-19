using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class TrackRemover : ITrackRemover
    {
        public void RemoveTrack(List<Track> tracks, Track track)
        {
            var trackToBeRemoved = tracks.First(x => x.Tag == track.Tag);
            var index = tracks.IndexOf(trackToBeRemoved);
            tracks.RemoveAt(index);
        }
    }
}
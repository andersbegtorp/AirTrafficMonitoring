using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.FlightManagement;

namespace AirTrafficMonitoring.FlightManagement
{
    public class TrackManagement : ITrackManagement
    {
        public void ManageTrack(List<Track> newestTracks, List<Track> oldestTracks, Track newTrack)
        {
            if (newestTracks.Exists(x => x.Tag == newTrack.Tag))
            {
                var newOldTrack = newestTracks.First(x => x.Tag == newTrack.Tag);

                if (oldestTracks.Exists(x => x.Tag == newTrack.Tag))
                {
                    var oldTrack = oldestTracks.First(x => x.Tag == newOldTrack.Tag);
                    var indexInOldTracks = oldestTracks.IndexOf(oldTrack);
                    oldestTracks[indexInOldTracks] = newOldTrack;
                }
                else
                {
                    oldestTracks.Add(newOldTrack);
                }

                var indexInNewTracks = newestTracks.IndexOf(newOldTrack);
                newestTracks[indexInNewTracks] = newTrack;
            }
            else
            {
                newestTracks.Add(newTrack);
            }
        }
    }
}
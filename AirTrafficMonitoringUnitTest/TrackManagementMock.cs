using System.Collections.Generic;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoringUnitTest
{
    public class TrackManagementMock : ITrackManagement
    {
        
        public TrackManagementMock(List<Track> newTracks, List<Track> oldTracks)
        {
            NewestTracks = newTracks;
            OldestTracks = oldTracks;
        }

        public List<Track> NewestTracks { get; set; }
        public List<Track> OldestTracks { get; set; }

        public void ManageTrack(List<Track> newestTracks, List<Track> oldestTracks, Track newTrack)
        {
            foreach (var track in NewestTracks)
            {
                newestTracks.Add(track);
            }
            foreach (var track in oldestTracks)
            {
                oldestTracks.Add(track);
            }
        }
    }
}
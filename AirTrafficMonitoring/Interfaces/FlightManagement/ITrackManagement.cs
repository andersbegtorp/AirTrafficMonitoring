using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.FlightManagement
{
    public interface ITrackManagement
    {
        void ManageTrack(List<Track> newestTracks, List<Track> oldestTracks, Track newTrack);
    }
}
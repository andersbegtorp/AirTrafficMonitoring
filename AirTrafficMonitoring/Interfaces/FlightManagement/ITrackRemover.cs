using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.FlightManagement
{
    public interface ITrackRemover
    {
        void RemoveTrack(List<Track> tracks, Track trackToBeRemoved);
    }
}
using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer
{
    public interface IVelocityAnalyzer
    {
        void AnalyzeVelocity(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

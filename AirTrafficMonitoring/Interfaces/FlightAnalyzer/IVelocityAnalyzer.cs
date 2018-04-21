using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IVelocityAnalyzer
    {
        void AnalyzeVelocity(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer
{
    public interface ICourseAnalyzer
    {
        void AnalyzeCourse(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

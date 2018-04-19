using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface ICourseAnalyzer
    {
        void AnalyzeCourse(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

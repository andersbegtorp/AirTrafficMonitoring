using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public interface ICourseAnalyzer
    {
        void AnalyzeCourse(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface IVelocityAnalyzer
    {
        void AnalyzeVelocity(List<Track> OldestTracks, List<Track> NewestTracks);
    }
}

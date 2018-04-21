using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public interface ICollisionAnalyzer
    {
        bool AnalyzeCollision(Track flight1, Track flight2);
    }
}
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.CollisionController
{
    public interface ICollisionAnalyzer
    {
        bool AnalyzeCollision(Track flight1, Track flight2);
    }
}
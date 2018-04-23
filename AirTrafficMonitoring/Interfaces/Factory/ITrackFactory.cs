using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.Factory
{
    public interface ITrackFactory
    {
        Track CreateTrack(string trackInfo);
    }
}
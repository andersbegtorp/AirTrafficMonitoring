namespace AirTrafficMonitoring
{
    public interface ITrackFactory
    {
        Track CreateTrack(string trackInfo);
    }
}
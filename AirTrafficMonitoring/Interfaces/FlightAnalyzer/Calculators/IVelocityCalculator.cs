namespace AirTrafficMonitoring.Interfaces
{
    public interface IVelocityCalculator
    {
        void CalculateVelocity(Track oldTrack, Track newTrack);
    }
}

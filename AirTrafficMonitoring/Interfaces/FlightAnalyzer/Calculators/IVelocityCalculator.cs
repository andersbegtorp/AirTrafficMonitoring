using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators
{
    public interface IVelocityCalculator
    {
        void CalculateVelocity(Track oldTrack, Track newTrack);
    }
}

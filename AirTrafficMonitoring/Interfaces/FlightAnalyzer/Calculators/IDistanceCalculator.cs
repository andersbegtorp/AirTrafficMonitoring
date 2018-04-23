namespace AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators
{
    public interface IDistanceCalculator
    {
        double CalculateDistance(double x1, double x2, double y1, double y2);
    }
}
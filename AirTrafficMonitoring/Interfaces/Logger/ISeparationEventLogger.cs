namespace AirTrafficMonitoring.Interfaces
{
    public interface ISeparationEventLogger
    {
        void LogSeparationEvent(object o, SeparationEventArgs arg);
    }
}
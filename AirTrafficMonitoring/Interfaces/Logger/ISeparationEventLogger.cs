using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.Logger
{
    public interface ISeparationEventLogger
    {
        void LogSeparationEvent(object o, SeparationEventArgs arg);
    }
}
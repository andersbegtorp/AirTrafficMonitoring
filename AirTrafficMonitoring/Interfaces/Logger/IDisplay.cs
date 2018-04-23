using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.Logger
{
    public interface IDisplay
    {
        void DisplayTracks(object o, TrackLogEventArgs arg);
    }
}

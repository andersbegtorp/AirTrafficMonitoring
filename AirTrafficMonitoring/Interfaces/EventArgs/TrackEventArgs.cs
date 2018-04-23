using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.EventArgs
{
    public class TrackEventArgs : System.EventArgs
    {
        public Track Track { get; }

        public TrackEventArgs(Track track)
        {
            Track = track;
        }
    }
}
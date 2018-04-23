using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.EventArgs
{
    public class TracksDataEventArgs : System.EventArgs
    {
        public List<Track> Tracks { get; }
        public TracksDataEventArgs(List<Track> tracks)
        {
            Tracks = tracks;
        }
    }
}
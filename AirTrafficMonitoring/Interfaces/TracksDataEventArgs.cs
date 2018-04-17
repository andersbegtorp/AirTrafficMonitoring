using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public class TracksDataEventArgs : EventArgs
    {
        public List<Track> Tracks { get; }
        public TracksDataEventArgs(List<Track> tracks)
        {
            Tracks = tracks;
        }
    }
}
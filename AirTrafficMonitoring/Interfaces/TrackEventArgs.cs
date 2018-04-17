using System;

namespace AirTrafficMonitoring
{
    public class TrackEventArgs : EventArgs
    {
        public Track Track { get; }

        public TrackEventArgs(Track track)
        {
            Track = track;
        }
    }
}
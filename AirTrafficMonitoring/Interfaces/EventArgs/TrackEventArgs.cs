using System;

namespace AirTrafficMonitoring.Interfaces
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
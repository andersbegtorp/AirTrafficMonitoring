using System;

namespace AirTrafficMonitoring.Interfaces
{
    public class TrackLogEventArgs : EventArgs
    {
        public string Log { get; }

        public TrackLogEventArgs(string log)
        {
            Log = log;
        }
    }
}
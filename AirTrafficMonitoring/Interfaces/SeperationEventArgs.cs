using System;

namespace AirTrafficMonitoring
{
    public class SeperationEventArgs : EventArgs
    {
        public string SeperationNote { get; }

        public SeperationEventArgs(string seperationNote)
        {
            SeperationNote = seperationNote;
        }
    }
}
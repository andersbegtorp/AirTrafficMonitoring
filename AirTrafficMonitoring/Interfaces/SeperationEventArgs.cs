using System;

namespace AirTrafficMonitoring.Interfaces
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
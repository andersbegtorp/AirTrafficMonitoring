using System;

namespace AirTrafficMonitoring.Interfaces
{
    public class SeparationEventArgs : EventArgs
    {
        public string SeperationNote { get; }

        public SeparationEventArgs(string seperationNote)
        {
            SeperationNote = seperationNote;
        }
    }
}
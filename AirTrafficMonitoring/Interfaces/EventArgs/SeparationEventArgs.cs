namespace AirTrafficMonitoring.Interfaces.EventArgs
{
    public class SeparationEventArgs : System.EventArgs
    {
        public string SeperationNote { get; }

        public SeparationEventArgs(string seperationNote)
        {
            SeperationNote = seperationNote;
        }
    }
}
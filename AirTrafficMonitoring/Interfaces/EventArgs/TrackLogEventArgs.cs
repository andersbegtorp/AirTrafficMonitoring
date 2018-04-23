namespace AirTrafficMonitoring.Interfaces.EventArgs
{
    public class TrackLogEventArgs : System.EventArgs
    {
        public string Log { get; }

        public TrackLogEventArgs(string log)
        {
            Log = log;
        }
    }
}
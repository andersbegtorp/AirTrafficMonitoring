using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class ConsoleDisplay : IDisplay
    {

        public ConsoleDisplay(IFlightAnalyzer flightAnalyzer)
        {
            flightAnalyzer.TracksAnalyzedEvent += DisplayTracks;
        }
        public void DisplayTracks(object o, TracksDataEventArgs arg)
        {
            foreach (var track in arg.Tracks)
            {
                Console.WriteLine(track.ToString());
            }
        }
    }
}
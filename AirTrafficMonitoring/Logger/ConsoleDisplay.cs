using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class ConsoleDisplay : IDisplay, ISeparationEventLogger
    {

        public ConsoleDisplay(IFlightAnalyzer flightAnalyzer, IFlightController flightController)
        {
            flightAnalyzer.TracksAnalyzedEvent += DisplayTracks;
            flightController.SeperationEvent += LogSeparationEvent;
        }
        public void DisplayTracks(object o, TrackLogEventArgs arg)
        {
            Console.WriteLine(arg.Log);
        }

        public void LogSeparationEvent(object o, SeparationEventArgs arg)
        {
            Console.WriteLine(arg.SeperationNote);
        }
    }
}
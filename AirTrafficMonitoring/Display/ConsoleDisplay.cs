using System;
using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.Logger;

namespace AirTrafficMonitoring.Display
{
    public class ConsoleDisplay : IDisplay, ISeparationEventLogger
    {

        public ConsoleDisplay(IFlightAnalyzer flightAnalyzer, ICollisionController collisionController)
        {
            flightAnalyzer.TracksAnalyzedEvent += DisplayTracks;
            collisionController.SeperationEvent += LogSeparationEvent;
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
using System;

namespace AirTrafficMonitoring
{
    public class ConsoleDisplay : IDisplay
    {
        public void DisplayTrack(Track track)
        {
            Console.WriteLine("Tag: " + track.Tag + " X: " + track.XCoordinate + " Y: " + track.YCoordinate +
                              " Altitude: " + track.Altitude + " Time stamp: " + track.TimeStamp);

        }
    }
}
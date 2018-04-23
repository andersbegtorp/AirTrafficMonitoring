using System;

namespace AirTrafficMonitoring.DataTransferObjects
{
    public class Track
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public double HorizontalVelocity { get; set; }
        public double CompassCourse { get; set; }
        public DateTime TimeStamp { get; set; }

        public Track()
        {
            Tag = "Default";
            XCoordinate = 0;
            YCoordinate = 0;
            Altitude = 0;
            HorizontalVelocity = 0;
            CompassCourse = 0;
            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            return "Tag: " + Tag + " X: " + XCoordinate + " Y: " + YCoordinate +
                   " Altitude: " + Altitude + " Velocity: " + HorizontalVelocity + " Course: " + CompassCourse +
                   " Time stamp: " + TimeStamp;
        }
    }
}

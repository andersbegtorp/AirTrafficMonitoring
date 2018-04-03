using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
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
    }
}

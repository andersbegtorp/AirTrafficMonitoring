using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Configuration.AirspaceConfiguration
{
    public class AirspaceConfiguration
    {
        public int SouthWestXCoordinate { get; set; }
        public int SouthWestYCoordinate { get; set; }
        public int NorthEastXCoordinate { get; set; }
        public int NorthEastYCoordinate { get; set; }
        public int LowestAltitude { get; set; }
        public int HighestAltitude { get; set; }
    }
}

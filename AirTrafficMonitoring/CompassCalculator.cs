using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class CompassCalculator
    {
        public double Calculate(int x1, int x2, int y1, int y2)
        {
            var deltaX = x2 - x1;
            var deltaY = y2 - y1;
            var rad = Math.Atan2(deltaY, deltaX);

            var deg = rad * (180 / Math.PI);

            return deg;

        }
    }
}

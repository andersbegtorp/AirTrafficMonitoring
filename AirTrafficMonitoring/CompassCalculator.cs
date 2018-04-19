using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class CompassCalculator : ICompassCalculator
    {
        public double CalculateCourse(double x1, double x2, double y1, double y2)
        {
            var deltaX = x2 - x1;
            var deltaY = y2 - y1;

            var atan = Math.Atan(deltaY / deltaX) / Math.PI * 180;
            if (deltaX < 0 || deltaY < 0)
                atan += 180;
            if (deltaX > 0 && deltaY < 0)
                atan -= 180;
            if (atan < 0)
                atan += 360;

            return atan % 360;
        }
    }
}

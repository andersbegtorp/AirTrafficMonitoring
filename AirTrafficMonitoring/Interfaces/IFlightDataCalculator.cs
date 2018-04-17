using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface IFlightDataCalculator
    {
        double Calculate(int x1, int x2, int y1, int y2);
    }
}

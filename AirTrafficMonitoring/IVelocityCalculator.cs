using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface IVelocityCalculator
    {
        void CalculateVelocity(Track oldTrack, Track newTrack);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class VelocityAnalyzer : IVelocityAnalyzer

    {
        private IVelocityCalculator _velocityCalculator;

        public VelocityAnalyzer(IVelocityCalculator velocityCalculator)
        {
            _velocityCalculator = velocityCalculator;
        }

        public void AnalyzeVelocity(List<Track> OldestTracks, List<Track> NewestTracks)
        {
            foreach (var OldTrack in OldestTracks)
            {
                foreach (var NewTrack in NewestTracks)
                {
                    if (NewTrack.Tag == OldTrack.Tag)
                    {
                        _velocityCalculator.CalculateVelocity(OldTrack, NewTrack);
                    }

                }

            }

        }
    }
}

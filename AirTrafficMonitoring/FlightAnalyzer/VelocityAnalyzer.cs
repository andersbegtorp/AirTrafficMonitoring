using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;

namespace AirTrafficMonitoring.FlightAnalyzer
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

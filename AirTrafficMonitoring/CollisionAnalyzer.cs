using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class CollisionAnalyzer : ICollisionAnalyzer
    {
        private IDistanceCalculator _distanceCalculator;
        public event EventHandler<SeperationEventArgs> SeperationEvent;

        public CollisionAnalyzer(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }
        public void AnalyzeCollision(List<Track> tracks)
        {
            for (int i = 0; i < tracks.Count-1; i++)
            {
                for (int j = i+1; j < tracks.Count; j++)
                {
                    if (_distanceCalculator.CalculateDistance(tracks[i].XCoordinate, tracks[j].XCoordinate,
                            tracks[i].YCoordinate, tracks[j].YCoordinate) < 5000 && Math.Abs(tracks[i].Altitude - tracks[j].Altitude) <300)
                    {
                        var Handler = SeperationEvent;
                        Handler?.Invoke(this,new SeperationEventArgs("Timestamp: " + tracks[i].TimeStamp.ToString() + "Flight: " + tracks[i].Tag + " is on collision with flight: " + tracks[j].Tag));
                    }

                }
            }
        }
    }

    public interface ICollisionAnalyzer
    {
    }
}

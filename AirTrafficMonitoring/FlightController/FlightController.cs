
using System;
using AirTrafficMonitoring.Interfaces;


namespace AirTrafficMonitoring
{
    public class FlightController : IFlightController
    {
        private ICollisionAnalyzer _collisionAnalyzer;

        public event EventHandler<SeperationEventArgs> SeperationEvent;

        public FlightController(IFlightManagement flightManagement, ICollisionAnalyzer collisionAnalyzer)
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
            _collisionAnalyzer = collisionAnalyzer;
        }
        public void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg)
        {
            for (int i = 0; i < arg.NewestTracks.Count - 1; i++)
            {
                for (int j = i + 1; j < arg.NewestTracks.Count; j++)
                {
                    if (_collisionAnalyzer.AnalyzeCollision(arg.NewestTracks[i], arg.NewestTracks[j]))
                    {
                        var Handler = SeperationEvent;
                        Handler?.Invoke(this, new SeperationEventArgs("Timestamp: " + arg.NewestTracks[i].TimeStamp.ToString() + "Flight: "
                                                                      + arg.NewestTracks[i].Tag + " is on collision with flight: " + arg.NewestTracks[j].Tag));
                    }
                }
            }

        }

    }
}
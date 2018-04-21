
using System;
using AirTrafficMonitoring.Interfaces;


namespace AirTrafficMonitoring
{
    public class FlightController : IFlightController
    {
        private ICollisionAnalyzer _collisionAnalyzer;
        private ISeparationStringBuilder _separationStringBuilder;

        public event EventHandler<SeparationEventArgs> SeperationEvent;

        public FlightController(IFlightManagement flightManagement, ICollisionAnalyzer collisionAnalyzer, ISeparationStringBuilder separationStringBuilder )
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
            _collisionAnalyzer = collisionAnalyzer;
            _separationStringBuilder = separationStringBuilder;
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
                        Handler?.Invoke(this,
                            new SeparationEventArgs(
                                _separationStringBuilder.BuildSeperationNote(arg.NewestTracks[i], arg.NewestTracks[j])));
                    }
                }
            }

        }

    }
}
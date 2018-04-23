using System;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightManagement;

namespace AirTrafficMonitoring.FlightAnalyzer
{
    public class FlightAnalyzer : IFlightAnalyzer
    {
        private ICourseAnalyzer _courseAnalyzer;
        private IVelocityAnalyzer _velocityAnalyzer;
        public event EventHandler<TrackLogEventArgs> TracksAnalyzedEvent;

        public FlightAnalyzer(IFlightManagement flightManagement, ICourseAnalyzer courseAnalyzer, IVelocityAnalyzer velocityAnalyzer)
        {
            flightManagement.FlightDataReady += HandleFlightsInAirspace;
            _courseAnalyzer = courseAnalyzer;
            _velocityAnalyzer = velocityAnalyzer;
        }

        public void HandleFlightsInAirspace(object sender, FlightMovementEventArgs arg)
        {
            _courseAnalyzer.AnalyzeCourse(arg.OldestTracks, arg.NewestTracks);
            _velocityAnalyzer.AnalyzeVelocity(arg.OldestTracks,arg.NewestTracks);

            foreach (var track in arg.NewestTracks)
            {
                var Handler = TracksAnalyzedEvent;
                Handler?.Invoke(this, new TrackLogEventArgs(track.ToString()));
            }
        }
    }
}
﻿using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightAnalyzer : IFlightAnalyzer
    {
        private ICourseAnalyzer _courseAnalyzer;
        private IVelocityAnalyzer _velocityAnalyzer;
        public event EventHandler<TracksDataEventArgs> TracksAnalyzedEvent;

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
            var Handler = TracksAnalyzedEvent;
            Handler?.Invoke(this,new TracksDataEventArgs(arg.NewestTracks));
        }
    }
}
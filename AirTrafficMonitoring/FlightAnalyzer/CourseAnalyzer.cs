﻿using System.Collections.Generic;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer;
using AirTrafficMonitoring.Interfaces.FlightAnalyzer.Calculators;

namespace AirTrafficMonitoring.FlightAnalyzer
{
    public class CourseAnalyzer : ICourseAnalyzer
    {
        private ICompassCalculator _compassCalculator;
        public CourseAnalyzer(ICompassCalculator compassCalculator)
        {
            _compassCalculator = compassCalculator;
        }

        public void AnalyzeCourse(List<Track> OldestTracks, List<Track> NewestTracks)
        {
            foreach (var OldTrack in OldestTracks)
            {
                foreach (var NewTrack in NewestTracks)
                {
                    if (NewTrack.Tag == OldTrack.Tag)
                    {
                        NewTrack.CompassCourse = _compassCalculator.CalculateCourse(OldTrack.XCoordinate,
                            NewTrack.XCoordinate, OldTrack.YCoordinate, NewTrack.YCoordinate);
                    }

                }

            }

        }
    }
}

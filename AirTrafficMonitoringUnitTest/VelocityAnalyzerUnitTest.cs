using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    class VelocityAnalyzerUnitTest
    {
        private VelocityAnalyzer _uut;
        private IVelocityCalculator _fakeVelocityCalculator;

        [SetUp]
        public void SetUp()
        {
            _fakeVelocityCalculator = Substitute.For<IVelocityCalculator>();
            _uut = new VelocityAnalyzer(_fakeVelocityCalculator);
        }

        [Test]
        public void AnalyzeVelocity_AnalyzeVelocityForOneMatchingTrack_TracksWereMatched()
        {
            // ARRANGE
            List<Track> oldList = new List<Track>();
            List<Track> newList = new List<Track>();
            Track matchingTrack = new Track()
            {
                CompassCourse = 0,
                Tag = "ATR433",
                XCoordinate = 0,
                YCoordinate = 0
            };

            oldList.Add(matchingTrack);
            oldList.Add(new Track()
            {
                CompassCourse = 0,
                Tag = "ATR444",
                XCoordinate = 0,
                YCoordinate = 0
            });

            oldList.Add(new Track()
            {
                CompassCourse = 0,
                Tag = "ATR555",
                XCoordinate = 0,
                YCoordinate = 0
            });

            newList.Add(matchingTrack);
            newList.Add(new Track()
            {
                CompassCourse = 0,
                Tag = "ATR111",
                XCoordinate = 0,
                YCoordinate = 0
            });

            newList.Add(new Track()
            {
                CompassCourse = 0,
                Tag = "ATR222",
                XCoordinate = 0,
                YCoordinate = 0
            });
            
        
        }
    }
}

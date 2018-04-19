using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    public class CourseAnalyserUnitTest
    {
        private CourseAnalyzer _uut;
        private ICompassCalculator _fakeCompassCalculator;

        [SetUp]
        public void SetUp()
        {
            _fakeCompassCalculator = Substitute.For<ICompassCalculator>();
            _uut = new CourseAnalyzer(_fakeCompassCalculator);
        }

        [Test]
        public void AnalyzeCourse_AnalyzeCoursesForOneMatchingTrack_TracksWereMatched()
        {
            //Arrange
            List<Track> Oldlist = new List<Track>();
            List<Track> Newlist = new List<Track>();
            Track matchingTrack = new Track() { CompassCourse = 0, Tag = "ATR423", XCoordinate = 0, YCoordinate = 0 };

            Oldlist.Add(matchingTrack);
            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR424", XCoordinate = 0, YCoordinate = 0 });
            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR425", XCoordinate = 0, YCoordinate = 0 });

            Newlist.Add(matchingTrack);
            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR426", XCoordinate = 0, YCoordinate = 0 });
            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR427", XCoordinate = 0, YCoordinate = 0 });

            _fakeCompassCalculator.CalculateCourse(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(),
                Arg.Any<double>()).Returns(10);
            //Act
            _uut.AnalyzeCourse(Oldlist, Newlist);

            Assert.That(matchingTrack.CompassCourse, Is.EqualTo(10));
            
        }

        [Test]
        public void AnalyzeCourse_AnalyzeCoursesForTwoMatchingTrack_TracksWereMatched()
        {
            //Arrange
            List<Track> Oldlist = new List<Track>();
            List<Track> Newlist = new List<Track>();
            Track matchingTrack1 = new Track() { CompassCourse = 0, Tag = "ATR423", XCoordinate = 0, YCoordinate = 0 };
            Track matchingTrack2 = new Track() { CompassCourse = 0, Tag = "ATR428", XCoordinate = 0, YCoordinate = 0 };


            Oldlist.Add(matchingTrack1);
            Oldlist.Add(matchingTrack2);
            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR424", XCoordinate = 0, YCoordinate = 0 });
            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR425", XCoordinate = 0, YCoordinate = 0 });

            Newlist.Add(matchingTrack1);
            Newlist.Add(matchingTrack2);
            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR426", XCoordinate = 0, YCoordinate = 0 });
            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR427", XCoordinate = 0, YCoordinate = 0 });

            _fakeCompassCalculator.CalculateCourse(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(),
                Arg.Any<double>()).Returns(10);
            //Act
            _uut.AnalyzeCourse(Oldlist, Newlist);

            Assert.That(matchingTrack1.CompassCourse, Is.EqualTo(10));
            Assert.That(matchingTrack2.CompassCourse, Is.EqualTo(10));

        }

        [Test]
        public void AnalyzeCourse_AnalyzeCourseForNONEMatchingTrack_TracksWerentMatched()
        {
            //Arrange
            List<Track> Oldlist = new List<Track>();
            List<Track> Newlist = new List<Track>();

            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR424", XCoordinate = 0, YCoordinate = 0 });
            Oldlist.Add(new Track() { CompassCourse = 0, Tag = "ATR425", XCoordinate = 0, YCoordinate = 0 });


            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR426", XCoordinate = 0, YCoordinate = 0 });
            Newlist.Add(new Track() { CompassCourse = 0, Tag = "ATR427", XCoordinate = 0, YCoordinate = 0 });


            //Act
            _uut.AnalyzeCourse(Oldlist, Newlist);
            _fakeCompassCalculator.DidNotReceive().CalculateCourse(Arg.Any<double>(), Arg.Any<double>(),
                           Arg.Any<double>(),
                           Arg.Any<double>());

        }



    }
}
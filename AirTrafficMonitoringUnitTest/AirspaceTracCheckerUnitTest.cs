using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    class AirspaceTracCheckerUnitTest
    {
        private AirspaceTrackChecker _uut;
        //BVA for the limits of our flightarea
        [TestCase(100, 50, 10, 10, -10, -10, 99, 9, -9)]
        [TestCase(100, 50, 10, 10, -10, -10, 51, 0, 0)]
        public void CheckTrack_TrackIsInsideAirspace_ReturnTrue(int highest, int lowest, int nex, int ney, int swx, int swy, int alt, int x, int y)
        {
            //Arrange
            Airspace _airspace = new Airspace() { HighestAltitude = highest, LowestAltitude = lowest, NorthEastXCoordinate = nex, NorthEastYCoordinate = ney, SouthWestXCoordinate = swx, SouthWestYCoordinate = swy };
            _uut = new AirspaceTrackChecker(_airspace);
            Track track = new Track() { Altitude = alt, XCoordinate = x, YCoordinate = y };

            //Assert
            Assert.That(_uut.CheckTrack(track), Is.EqualTo(true));
        }

        [TestCase(100, 50, 10, 10, -10, -10, 100, 10, -10)]
        [TestCase(100, 50, 10, 10, -10, -10, 49, 9, -9)]
        public void CheckTrack_TrackIsOutsideAirspace_ReturnFalse(int highest, int lowest, int nex, int ney, int swx, int swy, int alt, int x, int y)
        {
            //Arrange
            Airspace _airspace = new Airspace() { HighestAltitude = highest, LowestAltitude = lowest, NorthEastXCoordinate = nex, NorthEastYCoordinate = ney, SouthWestXCoordinate = swx, SouthWestYCoordinate = swy };
            _uut = new AirspaceTrackChecker(_airspace);
            Track track = new Track() { Altitude = alt, XCoordinate = x, YCoordinate = y };

            //Assert
            Assert.That(_uut.CheckTrack(track), Is.EqualTo(false));
        }


    }
}

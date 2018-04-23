using System;
using System.Collections.Generic;
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
    class CollisionAnalyzerUnitTest
    {
        private CollisionAnalyzer _uut;
        private IDistanceCalculator _fakeDistanceCalculator;
        private IAltitudeDistanceCalculator _fakeAltitudeDistanceCalculator;

        [SetUp]
        public void SetUp()
        {
            _fakeDistanceCalculator = Substitute.For<IDistanceCalculator>();
            _fakeAltitudeDistanceCalculator = Substitute.For<IAltitudeDistanceCalculator>();
            _uut = new CollisionAnalyzer(_fakeDistanceCalculator,_fakeAltitudeDistanceCalculator);
        }

        [TestCase(, , , , , )]   // Virker ikke helt 
        public void AnalyzeCollision_AnalyzesCollisionBetweenTwoFlights_ReturnsTrue(int x1, int x2, int y1, int y2,
            int altitude1, int altitude2)
        {
            //Arrange
            Track flight1 = new Track() {Altitude = altitude1,  XCoordinate = x1, YCoordinate = y1};
            Track flight2 = new Track() {Altitude = altitude2, XCoordinate = x2, YCoordinate = y2};

            //Assert
            Assert.That(_uut.AnalyzeCollision(flight1,flight2), Is.EqualTo(true));
        }

        [TestCase(1000,2000 , 1000, 2000 , 800 ,100 )] //Virker ikke helt 
        public void AnalyzeCollision_AnalyzesCollisionBetweenTwoFlights_ReturnsFalse(double x1, double x2, double y1, double y2,
            int altitude1, int altitude2)
        {
            //Arrange
            var flight1 = new Track();
            var flight2 = new Track();

            flight1.XCoordinate = Convert.ToInt32(x1);
            flight1.YCoordinate = Convert.ToInt32(y1);
            flight2.XCoordinate = Convert.ToInt32(x2);
            flight2.YCoordinate = Convert.ToInt32(y2);
            flight1.Altitude = altitude1;
            flight2.Altitude = altitude2;

        //    _fakeDistanceCalculator.CalculateDistance(flight1.XCoordinate, flight1.YCoordinate, flight2.XCoordinate,
              //  flight2.YCoordinate);
          //  _fakeAltitudeDistanceCalculator.CalculateAltitudeDistance(flight1.Altitude, flight2.Altitude);
            //_uut.AnalyzeCollision(flight1, flight2);

            //Assert
            Assert.That(_uut.AnalyzeCollision(flight1, flight2), Is.EqualTo(false));
        }


    }
}

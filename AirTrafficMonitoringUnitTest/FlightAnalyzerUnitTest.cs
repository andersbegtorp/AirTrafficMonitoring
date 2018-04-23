using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    class FlightAnalyzerUnitTest
    {
        private FlightAnalyzer _uut;
        private ICourseAnalyzer _fakeCourseAnalyzer;
        private IVelocityAnalyzer _fakeVelocityAnalyzer;
        private IFlightManagement _fakelFlightManagement;
         

        [SetUp]
        public void SetUp()
        {
            _fakeCourseAnalyzer = Substitute.For<ICourseAnalyzer>();
            _fakeVelocityAnalyzer = Substitute.For<IVelocityAnalyzer>();
            _fakelFlightManagement = Substitute.For<IFlightManagement>();
            _uut = new FlightAnalyzer(_fakelFlightManagement, _fakeCourseAnalyzer,_fakeVelocityAnalyzer);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void HandleFlightsInAirspace_Dododod_RecieveEqualAmount(int NumberOfExpectedRaises)
        {
            List<Track> Oldlist = new List<Track>();
            List<Track> Newlist = new List<Track>();

            for (int i = 0; i < NumberOfExpectedRaises; i++)
            {
                Newlist.Add(new Track());
            }

            FlightMovementEventArgs arg = new FlightMovementEventArgs(Oldlist, Newlist);

            int raises = 0;

            _uut.TracksAnalyzedEvent += (sender, args) => raises++;

            _fakelFlightManagement.FlightDataReady += Raise.EventWith(arg);

            //Assert
            Assert.That(raises,Is.EqualTo(NumberOfExpectedRaises));
        


        }

    }
}

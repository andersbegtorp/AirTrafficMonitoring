using System;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Logger;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.Logger
{
    [TestFixture]
    public class SeparationStringBuilderUnitTest
    {
        private SeparationStringBuilder _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new SeparationStringBuilder();
        }

        [TestCase(2018, 8, 22, 23,34, 55, 212, "Tag1", "Tag2")]
        [TestCase(1918, 12, 2, 1, 4, 5, 12, "ARG1", "RGR2")]
        public void BuildSeperationNote_BuildsNote_NoteIsCorrect(int year, int month, int day, int hour, int minutes, int seconds, int milliseconds, string tagOne, string tagTwo)
        {
            DateTime timeStamp = new DateTime(year, month, day, hour, minutes, seconds, milliseconds);
            Track trackOne = new Track()
            {
                Tag = tagOne,
                TimeStamp = timeStamp
            };

            Track trackTwo = new Track()
            {
                Tag = tagTwo
            };

            string expectedString = "Timestamp: " + timeStamp.ToString() + " Flight: "
                + trackOne.Tag + " is on collision with flight: " + trackTwo.Tag;

            
            StringAssert.AreEqualIgnoringCase(expectedString, _uut.BuildSeperationNote(trackOne, trackTwo));

        }
    }
}

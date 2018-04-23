using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces.Logger;

namespace AirTrafficMonitoring.Logger
{
    public class SeparationStringBuilder : ISeparationStringBuilder
    {
        public string BuildSeperationNote(Track trackOne, Track trackTwo)
        {
            return "Timestamp: " + trackOne.TimeStamp.ToString() + " Flight: "
                   + trackOne.Tag + " is on collision with flight: " + trackTwo.Tag;
        }
    }
}
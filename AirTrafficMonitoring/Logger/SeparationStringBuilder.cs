namespace AirTrafficMonitoring
{
    public class SeparationStringBuilder : ISeparationStringBuilder
    {
        public string BuildSeperationNote(Track trackOne, Track trackTwo)
        {
            return "Timestamp: " + trackOne.TimeStamp.ToString() + "Flight: "
                   + trackOne.Tag + " is on collision with flight: " + trackTwo.Tag;
        }
    }
}
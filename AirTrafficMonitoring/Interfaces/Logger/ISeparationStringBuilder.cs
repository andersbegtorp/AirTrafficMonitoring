using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.Logger
{
    public interface ISeparationStringBuilder
    {
        string BuildSeperationNote(Track trackOne, Track trackTwo);
    }
}
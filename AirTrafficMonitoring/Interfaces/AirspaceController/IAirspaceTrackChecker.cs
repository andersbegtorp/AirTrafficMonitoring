using AirTrafficMonitoring.DataTransferObjects;

namespace AirTrafficMonitoring.Interfaces.AirspaceController
{
    public interface IAirspaceTrackChecker
    {
        bool CheckTrack(Track track);
    }
}
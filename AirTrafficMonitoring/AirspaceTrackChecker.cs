using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class AirspaceTrackChecker : IAirspaceTrackChecker
    {
        private Airspace _currentAirspace;
        public AirspaceTrackChecker(Airspace currentAirspace)
        {
            _currentAirspace = currentAirspace;
        }
        public bool CheckTrack(Track track)
        {
            if (track.Altitude <= _currentAirspace.LowestAltitude || track.Altitude >= _currentAirspace.HighestAltitude)
                return false;
            if (track.XCoordinate <= _currentAirspace.SouthWestXCoordinate ||
                track.XCoordinate >= _currentAirspace.NorthEastXCoordinate)
                return false;
            if (track.YCoordinate <= _currentAirspace.SouthWestYCoordinate ||
                track.YCoordinate >= _currentAirspace.NorthEastYCoordinate)
                return false;
            else return true;
        }
    }
}
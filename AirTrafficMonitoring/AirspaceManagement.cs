namespace AirTrafficMonitoring
{
    public class AirspaceManagement : IAirspaceManagement
    {
        private Airspace _currentAirspace;
        public AirspaceManagement(Airspace currentAirspace)
        {
            _currentAirspace = currentAirspace;
        }
        public bool AirSpaceTrackChecker(Track track)
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
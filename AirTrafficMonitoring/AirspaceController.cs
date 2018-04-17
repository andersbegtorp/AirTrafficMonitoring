using System;

namespace AirTrafficMonitoring
{
    public class AirspaceController : IAirspaceController
    {
        private IAirspaceManagement _airspaceManagement;
        public event EventHandler<TrackEventArgs> TrackInAirspace;
        public event EventHandler<TrackEventArgs> TrackOutsideAirspace;
        public AirspaceController(ITransponderDataReciever transponderDataReciever, IAirspaceManagement airspaceManagement)
        {
            transponderDataReciever.TrackDataReady += HandleTracks;
            _airspaceManagement = airspaceManagement;
        }

        public void HandleTracks(object o, TracksDataEventArgs arg)
        {
            foreach (var track in arg.Tracks)
            {
                if (_airspaceManagement.AirSpaceTrackChecker(track))
                {
                    var handler = TrackInAirspace;
                    handler?.Invoke(this, new TrackEventArgs(track));
                }
                else
                {
                    var handler = TrackOutsideAirspace;
                    handler?.Invoke(this, new TrackEventArgs(track));;
                }
            }
        }
    }
}
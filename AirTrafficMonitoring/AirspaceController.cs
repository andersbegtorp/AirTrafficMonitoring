using System;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class AirspaceController : IAirspaceController
    {
        private IAirspaceTrackChecker _airspaceTrackChecker;
        public event EventHandler<TrackEventArgs> TrackInAirspace;
        public event EventHandler<TrackEventArgs> TrackOutsideAirspace;
        public AirspaceController(ITransponderDataReciever transponderDataReciever, IAirspaceTrackChecker airspaceTrackChecker)
        {
            transponderDataReciever.TrackDataReady += HandleTracks;
            _airspaceTrackChecker = airspaceTrackChecker;
        }

        public void HandleTracks(object o, TracksDataEventArgs arg)
        {
            foreach (var track in arg.Tracks)
            {
                if (_airspaceTrackChecker.CheckTrack(track))
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
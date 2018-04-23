using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.AirspaceController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.FlightManagement;

namespace AirTrafficMonitoring.FlightManagement
{
    public class FlightManagement : IFlightManagement
    {
        private List<Track> _oldestTracks;
        private List<Track> _newestTracks;
        private ITrackRemover _trackRemover;
        private ITrackManagement _trackManagement;
        public event EventHandler<FlightMovementEventArgs> FlightDataReady;

        public FlightManagement(IAirspaceController airspaceController, ITrackRemover trackRemover,
            ITrackManagement trackManagement)
        {
            airspaceController.TrackOutsideAirspace += HandleTrackOutsideAirspace;
            airspaceController.TrackInAirspace += HandleTrackInsideAirspace;
            _trackRemover = trackRemover;
            _trackManagement = trackManagement;
            _newestTracks = new List<Track>();
            _oldestTracks = new List<Track>();
        }

        public void HandleTrackOutsideAirspace(object sender, TrackEventArgs arg)
        {
            if (_newestTracks.Any(x => x.Tag == arg.Track.Tag))
            {
                _trackRemover.RemoveTrack(_newestTracks, arg.Track);
            }

            if (_oldestTracks.Any(x => x.Tag == arg.Track.Tag))
            {
                _trackRemover.RemoveTrack(_oldestTracks, arg.Track);
            }
        }

        public void HandleTrackInsideAirspace(object sender, TrackEventArgs arg)
        {
            _trackManagement.ManageTrack(_newestTracks, _oldestTracks, arg.Track);

            var handler = FlightDataReady;
            handler?.Invoke(this, new FlightMovementEventArgs(_oldestTracks, _newestTracks));
        }
    }
}
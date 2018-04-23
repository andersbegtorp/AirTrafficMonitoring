using System;
using System.Linq;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.Factory;
using AirTrafficMonitoring.Interfaces.Transponder;
using TransponderReceiver;

namespace AirTrafficMonitoring.Transponder
{

    public class TransponderDataReceiver : ITransponderDataReciever
    {
        private ITrackFactory _trackFactory;

        public event EventHandler<TracksDataEventArgs> TrackDataReady;

        public TransponderDataReceiver(ITransponderReceiver transponderReceiver, ITrackFactory trackFactory)
        {
            _trackFactory = trackFactory;
            transponderReceiver.TransponderDataReady += HandleTransponderData;

        }

        public void HandleTransponderData(object o, RawTransponderDataEventArgs arg)
        {
            var list = arg.TransponderData.Select(track => _trackFactory.CreateTrack(track)).ToList();

            if (list.Count != 0)
            {
                var handler = TrackDataReady;
                handler?.Invoke(this, new TracksDataEventArgs(list));
            }
        }
    }
}

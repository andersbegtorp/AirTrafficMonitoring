using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class TrackProducer : ITrackProdcuer
    {
        private ConcurrentQueue<Track> _dataQueue;
        private ITrackFactory _trackFactory;

        public TrackProducer(ConcurrentQueue<Track> dataQueue, ITrackFactory trackFactory)
        {
            _dataQueue = dataQueue;
            _trackFactory = trackFactory;
        }

        public void TrackProducerRun()
        {
            while (true)
            {
                Track track = new Track();
                _dataQueue.Enqueue(track);
                Thread.Sleep(0);
             
            }
        }
    }
}

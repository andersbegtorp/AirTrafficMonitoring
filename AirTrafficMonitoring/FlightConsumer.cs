using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class FlightConsumer : IFlightConsumer
    {
        private ConcurrentQueue<Track> _dataQueue;
        private IFlightDataCalculator _flightDataCalculator;

        public FlightConsumer(ConcurrentQueue<Track> dataQueue, IFlightDataCalculator flightDataCalculator)
        {
            _dataQueue = dataQueue;
            _flightDataCalculator = flightDataCalculator;
        }

        public void FlightConsumerRun()
        {
            
            while (true)
            {
                Track track;
                while (!_dataQueue.TryDequeue(out track))
                {
                    Thread.Sleep(0);
                }
                //var compassCourse = track.CompassCourse;
                //var calculatedCourse = _flightDataCalculator.Calculate(track.XCoordinate, track.XCoordinate,
                //    track.YCoordinate, track.YCoordinate);  Ved godt, det ikke er rigtigt 

            }
        }
    }
}

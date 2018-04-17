using System;

namespace AirTrafficMonitoring
{
    public interface ITransponderDataReciever
    {
        event EventHandler<TracksDataEventArgs> TrackDataReady;
    }
}
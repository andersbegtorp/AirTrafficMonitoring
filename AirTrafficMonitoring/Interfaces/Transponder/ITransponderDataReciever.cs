using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface ITransponderDataReciever
    {
        event EventHandler<TracksDataEventArgs> TrackDataReady;
    }
}
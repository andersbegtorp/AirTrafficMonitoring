using System;
using AirTrafficMonitoring.Interfaces.EventArgs;

namespace AirTrafficMonitoring.Interfaces.Transponder
{
    public interface ITransponderDataReciever
    {
        event EventHandler<TracksDataEventArgs> TrackDataReady;
    }
}
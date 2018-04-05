using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{

    public class FlightController
    {
        private ITrackFactory _trackFactory;
        private IDisplay _display;

        public FlightController(ITransponderReceiver transponderReceiver, ITrackFactory trackFactory, IDisplay display)
        {
            _trackFactory = trackFactory;
            _display = display;
            transponderReceiver.TransponderDataReady += HandleTransponder;
        }

        public void HandleTransponder(object o, RawTransponderDataEventArgs arg)
        {
            var list = arg.TransponderData.Select(track => _trackFactory.CreateTrack(track)).ToList();

            foreach (var track in list)
            {
                _display.DisplayTrack(track);
            }

        }
    }
}

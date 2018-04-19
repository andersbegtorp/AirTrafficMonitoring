using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Configuration.AirspaceConfiguration;
using AirTrafficMonitoring.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {

            XMLAirspaceConfiguration.LoadAirspace(@"../../../AirspaceConfiguration.xml");

            ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            ITrackFactory tf = new TrackFactory();
            IDisplay display = new ConsoleDisplay();
            //TransponderDataReceiver fc = new TransponderDataReceiver(transponderReceiver,tf,display);
            
            Console.ReadLine();

        }
    }
}

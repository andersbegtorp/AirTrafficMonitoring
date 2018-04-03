using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Configuration.AirspaceConfiguration;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        static void Main(string[] args)
        {

            XMLAirspaceConfiguration.LoadAirspace(@"../../../AirspaceConfiguration.xml");
        }
    }
}

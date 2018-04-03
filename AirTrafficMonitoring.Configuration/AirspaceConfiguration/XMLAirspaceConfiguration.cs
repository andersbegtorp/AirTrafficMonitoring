using System.IO;
using System.Xml.Serialization;

namespace AirTrafficMonitoring.Configuration.AirspaceConfiguration
{
    public class XMLAirspaceConfiguration
    {
        public static void SaveAirspace(AirspaceConfiguration config, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(AirspaceConfiguration));
            serializer.Serialize(fs, config);
            fs.Close();
        }

        public static AirspaceConfiguration LoadAirspace(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(AirspaceConfiguration));
            AirspaceConfiguration config = (AirspaceConfiguration)serializer.Deserialize(fs);
            return config;
        }
    }
}
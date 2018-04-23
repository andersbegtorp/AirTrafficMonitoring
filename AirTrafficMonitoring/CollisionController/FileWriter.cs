using System.IO;
using AirTrafficMonitoring.Interfaces.Logger;

namespace AirTrafficMonitoring.CollisionController
{
    public class FileWriter : IFileWriter
    {
        public void WriteToFile(string path, string line)
        {
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }

            using (var sw = new StreamWriter(path, true))
            {
                sw.WriteLine(line);
            }
        }
    }
}
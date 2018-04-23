namespace AirTrafficMonitoring.Interfaces.Logger
{
    public interface IFileWriter
    {
        void WriteToFile(string path, string line);
    }
}
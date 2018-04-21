namespace AirTrafficMonitoring
{
    public interface IFileWriter
    {
        void WriteToFile(string path, string line);
    }
}
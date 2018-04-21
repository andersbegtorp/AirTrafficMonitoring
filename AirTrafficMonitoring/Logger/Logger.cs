using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class Logger : ISeparationEventLogger
    {
        private IFileWriter _fileWriter;
        private readonly string _loggingPath;
        public Logger(IFlightController flightController, string loggingPath, IFileWriter fileWriter)
        {
            flightController.SeperationEvent += LogSeparationEvent;
            _fileWriter = fileWriter;
            _loggingPath = loggingPath;
        }
        public void LogSeparationEvent(object o, SeparationEventArgs arg)
        {
            _fileWriter.WriteToFile(_loggingPath, arg.SeperationNote);
        }
    }
}
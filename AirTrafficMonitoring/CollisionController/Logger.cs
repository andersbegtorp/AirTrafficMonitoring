using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.Logger;

namespace AirTrafficMonitoring.CollisionController
{
    public class Logger : ISeparationEventLogger
    {
        private IFileWriter _fileWriter;
        private readonly string _loggingPath;
        public Logger(ICollisionController collisionController, string loggingPath, IFileWriter fileWriter)
        {
            collisionController.SeperationEvent += LogSeparationEvent;
            _fileWriter = fileWriter;
            _loggingPath = loggingPath;
        }
        public void LogSeparationEvent(object o, SeparationEventArgs arg)
        {
            _fileWriter.WriteToFile(_loggingPath, arg.SeperationNote);
        }
    }
}
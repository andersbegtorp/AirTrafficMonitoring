using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture]
    public class LoggerUnitTest
    {
        private IFileWriter _fakeFileWriter;
        private IFlightController _fakeFlightController;
        private Logger _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeFlightController = Substitute.For<IFlightController>();
            _fakeFileWriter = Substitute.For<IFileWriter>();
        }

        [TestCase("directory")]
        [TestCase("path/path/pathpath")]

        public void LogSeparationEvent_CallsFileWriter_PathIsCorrect(string path)
        {
            _uut = new Logger(_fakeFlightController, path, _fakeFileWriter);

            _fakeFlightController.SeperationEvent += Raise.EventWith(new SeparationEventArgs(""));

            _fakeFileWriter.Received(1).WriteToFile(path, Arg.Any<string>());
        }


        [TestCase("Note note")]
        public void LogSeparationEvent_CallsFileWriter_NoteIsCorrect(string note)
        {
            _uut = new Logger(_fakeFlightController, "", _fakeFileWriter);

            _fakeFlightController.SeperationEvent += Raise.EventWith(new SeparationEventArgs(note));

            _fakeFileWriter.Received(1).WriteToFile(Arg.Any<string>(), note);

        }


    }
}
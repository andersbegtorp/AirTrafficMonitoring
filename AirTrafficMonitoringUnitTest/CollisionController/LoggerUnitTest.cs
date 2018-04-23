using AirTrafficMonitoring.Interfaces.CollisionController;
using AirTrafficMonitoring.Interfaces.EventArgs;
using AirTrafficMonitoring.Interfaces.Logger;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoringUnitTest.CollisionController
{
    [TestFixture]
    public class LoggerUnitTest
    {
        private IFileWriter _fakeFileWriter;
        private ICollisionController _fakeCollisionController;
        private AirTrafficMonitoring.CollisionController.Logger _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeCollisionController = Substitute.For<ICollisionController>();
            _fakeFileWriter = Substitute.For<IFileWriter>();
        }

        [TestCase("directory")]
        [TestCase("path/path/pathpath")]

        public void LogSeparationEvent_CallsFileWriter_PathIsCorrect(string path)
        {
            _uut = new AirTrafficMonitoring.CollisionController.Logger(_fakeCollisionController, path, _fakeFileWriter);

            _fakeCollisionController.SeperationEvent += Raise.EventWith(new SeparationEventArgs(""));

            _fakeFileWriter.Received(1).WriteToFile(path, Arg.Any<string>());
        }


        [TestCase("Note note")]
        public void LogSeparationEvent_CallsFileWriter_NoteIsCorrect(string note)
        {
            _uut = new AirTrafficMonitoring.CollisionController.Logger(_fakeCollisionController, "", _fakeFileWriter);

            _fakeCollisionController.SeperationEvent += Raise.EventWith(new SeparationEventArgs(note));

            _fakeFileWriter.Received(1).WriteToFile(Arg.Any<string>(), note);

        }


    }
}
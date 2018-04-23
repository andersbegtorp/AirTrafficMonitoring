using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoringUnitTest
{
    [TestFixture()]
    public class TrackManagementUnitTest
    {
        private TrackManagement _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackManagement();
        }

        [Test]
        public void ManageTrack_FlightDoesntExist_FlightWasAddedToNewestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track(){Tag = "FlightNo" + i});
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }

            Track trackToBeAdded = new Track() {Tag = "ASE2018"};

            _uut.ManageTrack(newesTracks,oldesTracks,trackToBeAdded);

            Assert.That(newesTracks.Count(p => p.Tag == trackToBeAdded.Tag), Is.EqualTo(1));

        }


        [Test]
        public void ManageTrack_FlightDoesntExist_FlightWasNotAddedToOldestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }

            Track trackToBeAdded = new Track() { Tag = "ASE2018" };

            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(oldesTracks.Count(p => p.Tag == trackToBeAdded.Tag), Is.EqualTo(0));

        }


        [Test]
        public void ManageTrack_FlightExistInNewestTracks_FlightWasAddedToOldestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100};

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50};
            newesTracks.Add(trackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(oldesTracks.Count(p => p.Tag == trackWasAdded.Tag && p.Altitude == trackWasAdded.Altitude), Is.EqualTo(1));

        }



        [Test]
        public void ManageTrack_FlightExistInNewestTracks_OldTrackWasRemovedFromNewestTracks()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(newesTracks.Count(p => p.Tag == trackWasAdded.Tag && p.Altitude == trackWasAdded.Altitude), Is.EqualTo(0));

        }

        [Test]
        public void ManageTrack_FlightExistInNewestTracks_NewTrackWasAddedToNewestTracks()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(newesTracks.Count(p => p.Tag == trackToBeAdded.Tag && p.Altitude == trackToBeAdded.Altitude), Is.EqualTo(1));

        }

        [Test]
        public void ManageTrack_FlightExistInNewestTracksAndOldestTracks_NewTrackWasAddedToNewestTracks()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track oldTrackWasAdded = new Track() {Tag = "ASE2018", Altitude = 200};

            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            oldesTracks.Add(oldTrackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(newesTracks.Count(p => p.Tag == trackToBeAdded.Tag && p.Altitude == trackToBeAdded.Altitude), Is.EqualTo(1));

        }

        [Test]
        public void ManageTrack_FlightExistInNewestTracksAndOldestTracks_CorrectTrackWasRemovedFromNewestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track oldTrackWasAdded = new Track() { Tag = "ASE2018", Altitude = 200 };

            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            oldesTracks.Add(oldTrackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(newesTracks.Count(p => p.Tag == trackWasAdded.Tag && p.Altitude == trackWasAdded.Altitude), Is.EqualTo(0));

        }

        [Test]
        public void ManageTrack_FlightExistInNewestTracksAndOldestTracks_CorrectTrackWasRemovedFromOldestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track oldTrackWasAdded = new Track() { Tag = "ASE2018", Altitude = 200 };

            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            oldesTracks.Add(oldTrackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(oldesTracks.Count(p => p.Tag == oldTrackWasAdded.Tag && p.Altitude == oldTrackWasAdded.Altitude), Is.EqualTo(0));

        }


        [Test]
        public void ManageTrack_FlightExistInNewestTracksAndOldestTracks_CorrectTrackWasAddedToOldestTrack()
        {
            List<Track> newesTracks = new List<Track>();
            List<Track> oldesTracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                newesTracks.Add(new Track() { Tag = "FlightNo" + i });
                oldesTracks.Add(new Track() { Tag = "FlightNo" + i });
            }
            Track oldTrackWasAdded = new Track() { Tag = "ASE2018", Altitude = 200 };

            Track trackWasAdded = new Track() { Tag = "ASE2018", Altitude = 100 };

            Track trackToBeAdded = new Track() { Tag = "ASE2018", Altitude = 50 };
            newesTracks.Add(trackWasAdded);
            oldesTracks.Add(oldTrackWasAdded);
            _uut.ManageTrack(newesTracks, oldesTracks, trackToBeAdded);

            Assert.That(oldesTracks.Count(p => p.Tag == trackWasAdded.Tag && p.Altitude == trackWasAdded.Altitude), Is.EqualTo(1));

        }
    }
}

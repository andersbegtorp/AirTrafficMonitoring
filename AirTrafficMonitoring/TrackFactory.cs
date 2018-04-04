using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class TrackFactory
    {
        public Track CreateTrack(string trackInfo)
        {
            Track track = new Track();
            //DateTime dt = new DateTime();

            string[] array = trackInfo.Split(';');

            track.Tag = array[0];
            track.XCoordinate = Convert.ToInt32(array[1]);
            track.YCoordinate = Convert.ToInt32(array[2]);
            //track.TimeStamp = array[3];

            return track;
        }        
    }
}

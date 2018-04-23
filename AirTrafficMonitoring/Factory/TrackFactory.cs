using System;
using AirTrafficMonitoring.DataTransferObjects;
using AirTrafficMonitoring.Interfaces;
using AirTrafficMonitoring.Interfaces.Factory;

namespace AirTrafficMonitoring.Factory
{
    public class TrackFactory : ITrackFactory
    {
        public Track CreateTrack(string trackInfo)
        {
            Track track = new Track();

            string[] array = trackInfo.Split(';');

            track.Tag = array[0];
            track.XCoordinate = Convert.ToInt32(array[1]);
            track.YCoordinate = Convert.ToInt32(array[2]);
            track.Altitude = Convert.ToInt32(array[3]);
            track.TimeStamp = CreateDateTime(array[4]);
           

            return track;
        }

        private DateTime CreateDateTime(string trackInfo)
        {
            DateTime parsedDate = DateTime.ParseExact(trackInfo, "yyyyMMddHHmmssfff",System.Globalization.CultureInfo.InvariantCulture);
            return parsedDate;
        }


    }


}

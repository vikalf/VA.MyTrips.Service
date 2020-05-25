using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VA.MyTrips.Business.Components.Definition;
using VA.MyTrips.Business.Models;

namespace VA.MyTrips.Business.Components.Implementation
{
    public class TripComponent : ITripComponent
    {
        public async Task<TripModel> GetTrip(string tripId)
        {
            return new TripModel
            {
                Name = "test",
                Destination = "test",
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(-10),
                TripId = Guid.NewGuid().ToString(),
                GeoLocation = "-1000.222222,1522",
                Photos = new List<PhotoModel>
                {
                    new PhotoModel
                    {
                        PhotoId = Guid.NewGuid().ToString(),
                        TripId = Guid.NewGuid().ToString(),
                        Url = "http://tempUri.com/photo.png"
                    }
                }
            };
        }

        public async Task<List<TripModel>> GetTrips()
        {
            return new List<TripModel>
            {
                new TripModel
                {
                    Name = "test",
                    Destination = "test",
                    StartDate = DateTime.Now.AddDays(-30),
                    EndDate = DateTime.Now.AddDays(-10),
                    TripId = Guid.NewGuid().ToString(),
                    GeoLocation = "-1000.222222,1522",
                    Photos = new List<PhotoModel>
                    {
                        new PhotoModel
                        {
                            PhotoId = Guid.NewGuid().ToString(),
                            TripId = Guid.NewGuid().ToString(),
                            Url = "http://tempUri.com/photo.png"
                        }
                    }
                }
            };
        }

        public async Task<TripModel> CreateTrip(TripModel newTrip)
        {
            return new TripModel
            {
                Name = "testNew",
                Destination = "testNew",
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(-10),
                TripId = Guid.NewGuid().ToString(),
                GeoLocation = "-1000.222222,1522",
                Photos = new List<PhotoModel>
                {
                    new PhotoModel
                    {
                        PhotoId = Guid.NewGuid().ToString(),
                        TripId = Guid.NewGuid().ToString(),
                        Url = "http://tempUri.com/photo.png"
                    }
                }
            };
        }

        public async Task<bool> ArchivePhoto(string tripId, string photoId)
        {
            return true;
        }

        public async Task<bool> UploadPhoto(string tripId, byte[] file)
        {
            return true;
        }
    }
}

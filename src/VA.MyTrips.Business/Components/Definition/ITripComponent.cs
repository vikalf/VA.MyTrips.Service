using System.Collections.Generic;
using System.Threading.Tasks;
using VA.MyTrips.Business.Models;

namespace VA.MyTrips.Business.Components.Definition
{
    public interface ITripComponent
    {
        Task<List<TripModel>> GetTrips();

        Task<TripModel> GetTrip(string tripId);

        Task<TripModel> CreateTrip(TripModel newTrip);

        Task<bool> UploadPhoto(string tripId, byte[] file);

        Task<bool> ArchivePhoto(string tripId, string photoId);
    }
}
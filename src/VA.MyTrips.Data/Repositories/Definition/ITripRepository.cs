using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Data.Repositories.Definition
{
    public interface ITripRepository
    {
        Task<List<Models.Trip>> GetTrips();

        Task<Models.Trip> GetTrip(string tripId);

        Task<Models.Trip> CreateTrip(Models.Trip newTrip);
    }
}

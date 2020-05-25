using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VA.MyTrips.Data.Models;
using VA.MyTrips.Data.Repositories.Definition;

namespace VA.MyTrips.Data.Repositories.Implementation
{
    public class TripRepository : ITripRepository
    {

        private readonly Container _container;
        private readonly string dbName = "MyTrips";
        private readonly string containerName = "trip";

        public TripRepository(CosmosClient client)
        {
            _container = client.GetContainer(dbName, containerName);
        }

        public async Task<Trip> GetTrip(string tripId)
        {
            try
            {
                ItemResponse<Trip> response = await this._container.ReadItemAsync<Trip>(tripId, new PartitionKey(tripId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<Trip>> GetTrips()
        {

            var query = this._container.GetItemQueryIterator<Trip>(new QueryDefinition("Select * from c"));
            List<Trip> results = new List<Trip>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Trip> CreateTrip(Trip newTrip)
        {

            var response = await this._container.CreateItemAsync<Trip>(newTrip, new PartitionKey(newTrip.TripId));
            return response.Resource;

        }

    }
}

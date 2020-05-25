using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.MyTrips.Data.Models;
using VA.MyTrips.Data.Repositories.Definition;

namespace VA.MyTrips.Data.Repositories.Implementation
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Container _container;
        private readonly string dbName = "MyTrips";
        private readonly string containerName = "photo";

        public PhotoRepository(CosmosClient client)
        {
            _container = client.GetContainer(dbName, containerName);
        }

        public async Task<Photo> AddPhoto(Photo photo)
        {

            var response = await this._container.CreateItemAsync<Photo>(photo, new PartitionKey(photo.TripId));
            return response.Resource;
        }

        public async Task<bool> ArchivePhoto(string tripId, string photoId)
        {

            var itemResponse = await this._container.ReadItemAsync<Photo>(photoId, new PartitionKey(tripId));
            var item = itemResponse.Resource;
            item.Archived = true;

            var response = await this._container.UpsertItemAsync<Photo>(item, new PartitionKey(item.TripId));

            return response.StatusCode == System.Net.HttpStatusCode.OK;

        }

        public async Task<List<Photo>> GetPhotos(string tripId)
        {
            var query = this._container.GetItemQueryIterator<Photo>(
                new QueryDefinition($"SELECT * FROM c WHERE c.tripid = \"{tripId}\" and c.archived = false"));

            List<Photo> results = new List<Photo>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }
    }
}

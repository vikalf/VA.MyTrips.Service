using Azure.Storage.Blobs;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.MyTrips.Data.Repositories.Definition;

namespace VA.MyTrips.Data.Repositories.Implementation
{
    public class PhotoStorageRepository : IPhotoStorageRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _photoBlobName = "photos";
        private readonly string _archiveBlobName = "archive";

        public PhotoStorageRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }


        public async Task UploadPhotoToBlob(string name, byte[] file) 
        {
            var stream = new MemoryStream(file);
            var blobClient = _blobServiceClient.GetBlobContainerClient(_photoBlobName);
            await blobClient.UploadBlobAsync(name, stream);            
        }

    }
}

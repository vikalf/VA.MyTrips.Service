using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Data.Repositories.Definition
{
    public interface IPhotoStorageRepository
    {
        Task UploadPhotoToBlob(string name, byte[] file);
    }
}

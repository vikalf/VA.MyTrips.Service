using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Data.Repositories.Definition
{
    public interface IPhotoRepository
    {
        Task<List<Models.Photo>> GetPhotos(string tripId);

        Task<Models.Photo> AddPhoto(Models.Photo photo);

        Task<bool> ArchivePhoto(string tripId, string photoId);
    }
}

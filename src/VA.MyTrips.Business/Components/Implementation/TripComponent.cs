using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VA.MyTrips.Business.Components.Definition;
using VA.MyTrips.Business.Models;
using VA.MyTrips.Data.Models;
using VA.MyTrips.Data.Repositories.Definition;

namespace VA.MyTrips.Business.Components.Implementation
{
    public class TripComponent : ITripComponent
    {
        private readonly ITripRepository _tripRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoStorageRepository _photoStorageRepository;
        private readonly IMapper _mapper;

        public TripComponent(ITripRepository tripRepository, IPhotoRepository photoRepository, IPhotoStorageRepository photoStorageRepository, IMapper mapper)
        {
            _photoStorageRepository = photoStorageRepository;
            _tripRepository = tripRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        public async Task<TripModel> GetTrip(string tripId)
        {
            var dtoTrip = await _tripRepository.GetTrip(tripId); 
            var dtoPhotos = await _photoRepository.GetPhotos(tripId);

            var result = _mapper.Map<Trip, TripModel>(dtoTrip);
            result.Photos = _mapper.Map<List<Photo>, List<PhotoModel>>(dtoPhotos);

            return result;
        }

        public async Task<List<TripModel>> GetTrips()
        {

            var tripsDto = await _tripRepository.GetTrips();
            var result = _mapper.Map<List<Trip>, List<TripModel>>(tripsDto);

            return result;

        }

        public async Task<TripModel> CreateTrip(TripModel newTrip)
        {

            var tripDto = _mapper.Map<TripModel, Trip>(newTrip);
            var newTripDto = await _tripRepository.CreateTrip(tripDto);
            var result = _mapper.Map<Trip, TripModel>(newTripDto);

            return result;

        }

        public async Task<bool> UploadPhoto(string tripId, byte[] file, string fileName)
        {
            var photoId = Guid.NewGuid().ToString();
            var baseUrl = Environment.GetEnvironmentVariable("STORAGE_BLOB_BASE_URL");

            await _photoStorageRepository.UploadPhotoToBlob(fileName, file);

            var newPhoto = await _photoRepository.AddPhoto(new Photo
            {
                PhotoId = photoId,
                TripId = tripId,
                Url = $"{baseUrl}/{fileName}"
            });

            return newPhoto != null;
        }

        public async Task<bool> ArchivePhoto(string tripId, string photoId)
        {
            var response = await _photoRepository.ArchivePhoto(tripId, photoId);

            return response;
        }
    }
}

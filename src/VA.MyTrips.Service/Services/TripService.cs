using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VA.MyTrips.Business.Components.Definition;

namespace VA.MyTrips.Service
{
    public class TripService : Trip.TripBase
    {
        private readonly ILogger<TripService> _logger;
        private readonly ITripComponent _tripComponent;
        private readonly IMapper _mapper;
        public TripService(ILogger<TripService> logger, ITripComponent tripComponent, IMapper mapper)
        {
            _logger = logger;
            _tripComponent = tripComponent;
            _mapper = mapper;
        }

        public async override Task<TripsReply> GetTrips(EmtpyRequest request, ServerCallContext context)
        {
            try
            {
                var trips = await _tripComponent.GetTrips();

                var tripsReply = _mapper.Map<List<Business.Models.TripModel>, List<TripModel>>(trips);

                var reply = new TripsReply();
                reply.Trips.AddRange(tripsReply);

                return reply;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "GetTrips()");
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
        }

        public async override Task<TripModel> GetTrip(TripRequest request, ServerCallContext context)
        {

            if (string.IsNullOrWhiteSpace(request.TripId))
                throw new RpcException(new Status( StatusCode.InvalidArgument, "TripId must not be empty"));

            try
            {
                var trip = await _tripComponent.GetTrip(request.TripId);

                var reply = _mapper.Map<Business.Models.TripModel, TripModel>(trip);

                return reply;

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "GetTrips({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, "Internal Error"));
            }
            
        }

        public async override Task<TripModel> CreateTrip(CreateTripRequest request, ServerCallContext context)
        {

            var newTrip = await _tripComponent.CreateTrip(new Business.Models.TripModel
            {
                Name = request.Name,
                Destination = request.Destination,
                EndDate = System.DateTime.Parse(request.EndDate),
                GeoLocation = request.GeoLocation,
                Photos = new List<Business.Models.PhotoModel>(),
                StartDate = System.DateTime.Parse(request.StartDate),
                TripId = Guid.NewGuid().ToString()
            });

            var reply = _mapper.Map<Business.Models.TripModel, TripModel>(newTrip);

            return reply;


        }

        public async override Task<SuccessReply> UploadPhoto(UploadPhotoRequest request, ServerCallContext context)
        {
            var bytes = request.Filebytes.ToByteArray();
            var succeeded = await _tripComponent.UploadPhoto(request.TripId, bytes);

            return new SuccessReply
            {
                IsSuccess = succeeded
            };

        }

        public async override Task<SuccessReply> ArchivePhoto(ArchivePhotoRequest request, ServerCallContext context)
        {
            var result = await _tripComponent.ArchivePhoto(request.TripId, request.PhotoId);

            return new SuccessReply { IsSuccess = result };
        }

    }
}

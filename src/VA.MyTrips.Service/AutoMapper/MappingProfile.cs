using AutoMapper;

namespace VA.MyTrips.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Business.Models.TripModel, TripModel>()
                .ReverseMap();

            CreateMap<Business.Models.PhotoModel, PhotoModel>()
                .ReverseMap();
        }
    }
}

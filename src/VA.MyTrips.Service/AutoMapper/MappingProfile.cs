using AutoMapper;
using Microsoft.VisualBasic;
using System;
using System.Linq;

namespace VA.MyTrips.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Business.Models.TripModel, TripModel>()
                .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? ""))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination ?? ""))
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => src.GeoLocation ?? ""))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
                .ReverseMap()
                .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? ""))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination ?? ""))
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => src.GeoLocation ?? ""))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

            CreateMap<Business.Models.PhotoModel, PhotoModel>()
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => src.TripId))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));


            CreateMap<Data.Models.Trip, Business.Models.TripModel>();
            CreateMap<Data.Models.Photo, Business.Models.PhotoModel>();

        }

    }
}

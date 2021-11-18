using AutoMapper;
using MoviesAPI.Data.Dtos.Theater;
using MoviesAPI.Models;

namespace MoviesAPI.Profiles
{
    public class TheaterProfile : Profile
    {
        public TheaterProfile()
        {
            CreateMap<CreateTheaterDto, Theater>();
            CreateMap<Theater, ReadTheaterDto>();
            CreateMap<UpdateTheaterDto, Theater>();
        }
    }
}

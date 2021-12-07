using AutoMapper;
using MoviesAPI.Data.Dtos;
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

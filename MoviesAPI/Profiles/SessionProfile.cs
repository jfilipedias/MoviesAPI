using AutoMapper;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<CreateSessionDto, Session>();
            CreateMap<Session, ReadSessionDto>()
                .ForMember(dto => dto.OpeningTime, options => options
                .MapFrom(dto => dto.ClosingTime.AddMinutes(dto.Movie.Duration * (-1))));
            CreateMap<UpdateSessionDto, Session>();
        }
    }
}

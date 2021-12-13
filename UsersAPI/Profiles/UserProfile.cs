using AutoMapper;
using UsersAPI.Data.Dto;
using UsersAPI.Models;

namespace UsersAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}

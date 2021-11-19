using AutoMapper;
using MoviesAPI.Data.Dtos.Address;
using MoviesAPI.Models;

namespace MoviesAPI.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressDto, Address>();
            CreateMap<Address, ReadAddressDto>();
            CreateMap<UpdateAddressDto, Address>();
        }
    }
}

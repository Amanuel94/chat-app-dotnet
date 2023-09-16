using AutoMapper;
using ChatApp.Models.DTOs;
using ChatApp.Models.EntityModels;

namespace ChatApp.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }

}
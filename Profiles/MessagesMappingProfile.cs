using AutoMapper;
using ChatApp.Models.DTOs;
using ChatApp.Models.EntityModels;

namespace ChatApp.Profiles;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, CreateMessageDto>().ReverseMap();
        CreateMap<Message, MessageDto>().ReverseMap();
    }

}
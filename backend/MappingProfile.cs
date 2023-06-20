using AutoMapper;
using backend.Dtos;
using backend.Entities;

namespace backend;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
    }
}
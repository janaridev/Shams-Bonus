using AutoMapper;
using backend.Application.Dtos;
using backend.Domain.Entities;

namespace backend.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
    }
}
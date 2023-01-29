using Application.Features.Users.Commands.Create;
using AutoMapper;
using Domain.EntityModels;

namespace Application.Helper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>();
        }
    }
}
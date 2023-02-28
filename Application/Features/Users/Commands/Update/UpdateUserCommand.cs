using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : UserRegisterDto, IRequest<User>, IMapFrom<User>
    {
        public int Id { get; set; }
    }
}
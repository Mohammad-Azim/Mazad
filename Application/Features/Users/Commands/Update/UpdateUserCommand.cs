using Application.Features.Users.Dtos;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : UserDto, IRequest<User>
    {
        public int Id { get; set; }
    }
}
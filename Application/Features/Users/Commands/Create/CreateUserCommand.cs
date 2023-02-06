using Application.Features.Users.Dtos;
using Domain.EntityModels;
using MediatR;
namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : UserDto, IRequest<User> { }
}
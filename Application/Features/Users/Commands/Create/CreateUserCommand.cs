using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;
namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : UserRegisterDto, IRequest<CreateUserCommandResponse>, IMapFrom<User> { }
}
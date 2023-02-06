using Application.Features.Users.Dtos;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : UserRegisterDto, IRequest<User>
    {
        public int Id { get; set; }
    }
}
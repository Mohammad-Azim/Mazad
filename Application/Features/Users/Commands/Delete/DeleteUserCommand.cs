using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
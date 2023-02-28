using Application.Services.UserService;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Delete(request.Id);
        }
    }
}
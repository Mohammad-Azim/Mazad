using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0; // #??#!!
        }
    }
}
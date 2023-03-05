using Application.Context;
using Domain.EntityModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetWithEvents
{
    public record GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        }
    }
}
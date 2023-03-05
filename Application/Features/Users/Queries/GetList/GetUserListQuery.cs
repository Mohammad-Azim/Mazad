using Application.Context;
using Domain.EntityModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetList
{
    public record GetUserListQuery : IRequest<List<User>> { }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly ApplicationDbContext _context;

        public GetUserListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public record GetBidByIdQuery : IRequest<GetBidByIdQueryResponse>
    {
        public int Id { get; set; }
    }
    public class GetBidByIdQueryHandler : IRequestHandler<GetBidByIdQuery, GetBidByIdQueryResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetBidByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetBidByIdQueryResponse> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
        {
            var getBidByIdQueryResponse = new GetBidByIdQueryResponse();
            var data = await _context.Bids.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (data != null)
            {
                getBidByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getBidByIdQueryResponse.NotFoundResponse();
            }
            return getBidByIdQueryResponse;
        }
    }
}
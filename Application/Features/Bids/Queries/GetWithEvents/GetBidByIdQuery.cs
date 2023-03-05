using Application.Context;
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
        private readonly IGetBidByIdQueryResponse _response;

        public GetBidByIdQueryHandler(ApplicationDbContext context, IGetBidByIdQueryResponse response)
        {
            _context = context;
            _response = response;
        }
        public async Task<GetBidByIdQueryResponse> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Bids.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (data != null)
            {
                _response.SuccessResponse(data);
            }
            else
            {
                _response.NotFoundResponse();
            }
            return (GetBidByIdQueryResponse)_response;
        }
    }
}
using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public record GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetProductByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var getProductByIdQueryResponse = new GetProductByIdQueryResponse();

            var data = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (data != null)
            {
                getProductByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getProductByIdQueryResponse.NotFoundResponse();
            }
            return getProductByIdQueryResponse;
        }
    }
}
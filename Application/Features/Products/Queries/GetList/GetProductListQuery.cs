using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetList
{
    public class GetProductListQuery : IRequest<GetListProductQueryResponse> { }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, GetListProductQueryResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetProductListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetListProductQueryResponse> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var getListProductQueryResponse = new GetListProductQueryResponse();
            var data = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
            getListProductQueryResponse.SuccessResponse(data);
            return getListProductQueryResponse;
        }
    }
}
using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public record GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetCategoryByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var getCategoryByIdQueryResponse = new GetCategoryByIdQueryResponse();

            var data = await _context.Categories.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (data != null)
            {
                getCategoryByIdQueryResponse.SuccessResponse(data);
            }
            else
            {
                getCategoryByIdQueryResponse.NotFoundResponse();
            }
            return getCategoryByIdQueryResponse;
        }
    }
}
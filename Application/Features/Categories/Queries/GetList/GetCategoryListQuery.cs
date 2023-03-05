using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetList
{
    public record GetCategoryListQuery : IRequest<GetListCategoryQueryResponse> { }

    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, GetListCategoryQueryResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetCategoryListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetListCategoryQueryResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var getListCategoryQueryResponse = new GetListCategoryQueryResponse();

            //var data = await _categoryService.GetAll();
            var data = await _context.Categories.AsNoTracking().AsQueryable().ToListAsync(cancellationToken);

            getListCategoryQueryResponse.SuccessResponse(data);
            return getListCategoryQueryResponse;
        }
    }
}
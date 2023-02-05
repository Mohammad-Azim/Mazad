
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
    {
        public int Id { get; set; }

    }
}
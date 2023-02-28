using MediatR;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public record GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
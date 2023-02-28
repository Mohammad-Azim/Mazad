using MediatR;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public record GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
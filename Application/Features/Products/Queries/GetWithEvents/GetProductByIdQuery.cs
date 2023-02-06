using MediatR;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
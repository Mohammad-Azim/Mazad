
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Queries.GetWithEvents
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

    }
}
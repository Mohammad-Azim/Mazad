using Application.Features.Products.Dtos;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand : ProductDto, IRequest<Product>
    {
        public int Id { get; set; }
    }
}
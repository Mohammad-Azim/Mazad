using Application.Features.Products.Dtos;
using Domain.EntityModels;
using MediatR;
namespace Application.Features.Products.Commands.Create
{
    public record CreateProductCommand : ProductDto, IRequest<Product>
    {

    }
}
using Application.Features.Products.Dtos;
using MediatR;
namespace Application.Features.Products.Commands.Create
{
    public record CreateProductCommand : ProductDto, IRequest<CreateProductCommandResponse>
    {

    }
}
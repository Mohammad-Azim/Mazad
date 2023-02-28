using Application.Features.Products.Dtos;
using Application.Helper.Profiles;
using MediatR;
namespace Application.Features.Products.Commands.Create
{
    public record CreateProductCommand : ProductDto, IRequest<CreateProductCommandResponse>, IMapFrom<ProductDto> { }
}
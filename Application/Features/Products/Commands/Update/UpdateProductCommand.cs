using Application.Features.Products.Dtos;
using Application.Helper.Profiles;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand : ProductDto, IRequest<UpdateProductCommandResponse>, IMapFrom<ProductDto>
    {
        public int Id { get; set; }
    }
}
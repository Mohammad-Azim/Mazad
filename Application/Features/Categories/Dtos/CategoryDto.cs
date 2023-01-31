using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Dtos
{
    public record CategoryDto : IRequest<Category>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
    }
}
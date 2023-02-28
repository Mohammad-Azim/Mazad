
using Application.Features.Categories.Commands.Update;
using Application.Helper.Profiles;
using Domain.EntityModels;

namespace Application.Features.Categories.Dtos
{
    public record CategoryDto : IMapFrom<UpdateCategoryCommand>, IMapFrom<Category>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
    }
}
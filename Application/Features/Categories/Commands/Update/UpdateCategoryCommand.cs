using Application.Features.Categories.Dtos;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand : CategoryDto, IRequest<UpdateCategoryCommandResponse>
    {
        public int Id { get; set; }
    }
}
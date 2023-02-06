using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand : CategoryDto, IRequest<CreateCategoryCommandResponse> { }
}
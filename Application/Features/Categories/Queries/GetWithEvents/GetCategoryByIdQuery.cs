
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Queries.GetWithEvents
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }

    }
}
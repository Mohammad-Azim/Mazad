using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetWithEvents
{
    public record GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
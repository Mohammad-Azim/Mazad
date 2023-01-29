
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetWithEvents
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }

    }
}
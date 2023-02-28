using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetList
{
    public record GetUserListQuery : IRequest<List<User>> { }
}
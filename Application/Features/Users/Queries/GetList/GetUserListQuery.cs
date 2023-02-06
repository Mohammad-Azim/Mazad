using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetList
{
    public class GetUserListQuery : IRequest<List<User>> { }
}

using Application.Services.UserService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly IUserService _userService;


        public GetUserListQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<List<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAll();
        }
    }
}
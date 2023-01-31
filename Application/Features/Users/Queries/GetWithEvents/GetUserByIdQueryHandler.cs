
using Application.Services.UserService;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Queries.GetWithEvents
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserService _userService;


        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetById(request.Id);
        }


    }
}
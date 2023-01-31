
using Application.Services.UserService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(command);

            return await _userService.Create(user);
        }

    }
}
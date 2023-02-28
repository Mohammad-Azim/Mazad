using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;
using Application.Services.UserService;
using AutoMapper;


namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : UserRegisterDto, IRequest<User>, IMapFrom<User>
    {
        public int Id { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(command);
            var value = await _userService.GetById(user.Id);
            if (value != null)
                return await _userService.Update(user);
            return null;
        }
    }
}
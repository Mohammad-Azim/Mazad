using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;
using Application.Helper.CustomIdentity;
using Application.Services.UserService;
using AutoMapper;
using FluentValidation;

namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : UserRegisterDto, IRequest<CreateUserCommandResponse>, IMapFrom<User> { }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IValidator<CreateUserCommand> validator, IMapper mapper)
        {
            _userService = userService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var createUserCommandResponse = new CreateUserCommandResponse();
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.Errors.Count > 0)
            {
                createUserCommandResponse.ErrorsResponse(validationResult.Errors);
            }
            if (createUserCommandResponse.Success)
            {
                //User user = command.Mapping(_mapper);
                User user = _mapper.Map<User>(command);

                //User user = command.Mapping(_mapper);



                Tuple<string, string> PasswordAndSalt = UserManager.HashPassword(command.Password);
                user.Password = PasswordAndSalt.Item1;
                user.Password_Salts = PasswordAndSalt.Item2;

                user.Admin = false;
                user.AuthenticatedEmail = false;

                createUserCommandResponse.Data = await _userService.Create(user);
            }
            return createUserCommandResponse;
        }
    }
}
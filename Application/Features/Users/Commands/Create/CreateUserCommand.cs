using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;
using Application.Helper.CustomIdentity;
using AutoMapper;
using FluentValidation;
using Application.Context;

namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : UserRegisterDto, IRequest<CreateUserCommandResponse>, IMapFrom<User> { }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ApplicationDbContext context, IValidator<CreateUserCommand> validator, IMapper mapper)
        {
            _context = context;
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
                User user = _mapper.Map<User>(command);

                Tuple<string, string> PasswordAndSalt = UserManager.HashPassword(command.Password);
                user.Password = PasswordAndSalt.Item1;
                user.Password_Salts = PasswordAndSalt.Item2;

                user.Admin = false;
                user.AuthenticatedEmail = false;

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return createUserCommandResponse;
        }
    }
}
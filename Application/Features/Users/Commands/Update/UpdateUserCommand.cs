using Application.Features.Users.Dtos;
using Application.Helper.Profiles;
using Domain.EntityModels;
using MediatR;
using AutoMapper;
using Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : UserRegisterDto, IRequest<User>, IMapFrom<User>
    {
        public int Id { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UpdateUserCommandHandler(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(command);
            var value = await _context.Users.SingleOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
            if (value != null)
            {
                var data = _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                return data.Entity;
            }
            return null;
        }
    }
}
using Application.Features.Users.Commands.Update;
using Application.Helper.Profiles;

namespace Application.Features.Users.Dtos
{
    public record UserUpdateDto : UserRegisterDto, IMapFrom<UpdateUserCommand> { }
}
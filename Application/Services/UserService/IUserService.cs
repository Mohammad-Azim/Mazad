
using Domain.EntityModels;

namespace Application.Services.UserService
{
    public interface IUserService : IGenericService<User>
    {
        Task<bool> EmailExist(string emailAddress);

    }
}
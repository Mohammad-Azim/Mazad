
using Domain.EntityModels;
using Infrastructure.Context;

namespace Application.Services.UserService
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
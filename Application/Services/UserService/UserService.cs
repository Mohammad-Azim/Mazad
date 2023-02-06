using Domain.EntityModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserService
{
    public class UserService : GenericService<User>, IUserService
    {

        private readonly DbSet<User> entities;
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {
            entities = dbContext.Set<User>();
        }

        public async Task<bool> EmailExist(string emailAddress)
        {
            var user = await entities.AsNoTracking().Where(x => x.Email == emailAddress).FirstOrDefaultAsync();
            return user != null;
        }
    }
}
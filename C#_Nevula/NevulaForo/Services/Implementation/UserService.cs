using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Services.Contract;

namespace NevulaForo.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly NevulaContext _dbContext;

        public UserService(NevulaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(string email, string password)
        {
            User user_found = await _dbContext.Users.Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

            return user_found;
        }

        public async Task<User> SaveUser(User model)
        {
            _dbContext.Add(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }
    }
}

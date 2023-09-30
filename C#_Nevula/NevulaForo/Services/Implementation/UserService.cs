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
            User user_found = await _dbContext.Users.Include(u => u.UserRoles).Where(u => u.Email == email && u.Password == password && u.DeletedAt == null)
                .FirstOrDefaultAsync();

            return user_found;
        }

        public async Task<User> SaveUserAndRole(User userModel, int RoleId)
        {
            _dbContext.Add(userModel);
            await _dbContext.SaveChangesAsync();

            UserRole role = new UserRole()
            {
                IdUser = userModel.Id,
                IdRole = RoleId
            };

            _dbContext.Add(role);
            await _dbContext.SaveChangesAsync();

            return userModel;
        }
    }
}

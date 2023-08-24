using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;

namespace NevulaForo.Services.Contract
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);

        Task<User> SaveUser(User model);
    }
}

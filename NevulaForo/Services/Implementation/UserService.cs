using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Services.Contract;

namespace NevulaForo.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly NevulaContext _dbContext;
        private IWebHostEnvironment _hostingEnvironment;

        public UserService(NevulaContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<User> GetUser(string email, string password)
        {
            User user_found = await _dbContext.Users.Include(u => u.UserRoles).Where(u => u.Email == email && u.Password == password && (u.DeletedAt == null || u.DeletedAt > DateTime.Now.AddDays(-30)) && u.IsBanned == null)
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

        public async Task<User> UpdateUser(User userModel)
        {
            _dbContext.Update(userModel);
            await _dbContext.SaveChangesAsync();

            return userModel;
        }


        private readonly string _defaultProfileImagePath = "/images/profiles/default.jpg";

        public string GetUserProfileImagePath(int IdUser, bool renewImg = false)
        {
            string userFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, $"images/profiles/{IdUser}");

            foreach (var extension in new[] { ".jpg", ".png", ".webp", ".gif" })
            {
                string imagePath = Path.Combine(userFolderPath, $"profile_pic{extension}");
                if (File.Exists(imagePath))
                { 
                    // Usuario tiene una foto de perfil personalizada con la extensión encontrada.
                    if (renewImg)
                    {
                        /* Le agrego un parametro de consulta unico (el tiempo actual), para asegurar que el navegador lo tome como una nueva version
                         * de la imagen cuando se cambie el parametro (sirve para ver el cambio automaticamente en la pag. EditAvatar)
                         */
                        return $"/images/profiles/{IdUser}/profile_pic{extension}" + "?t=" + DateTime.Now.Ticks;
                    } else
                    {
                        return $"/images/profiles/{IdUser}/profile_pic{extension}";
                    }
                }
            }

            // Usuario no tiene una foto de perfil personalizada, devuelve la por defecto
            return _defaultProfileImagePath;
        }
    }
}

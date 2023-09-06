using NevulaForo.Models.DB;

namespace NevulaForo.Models.ViewModels
{
    public class UserProfileVM
    {
        public User oUser { get; set; }

        public List<Publication> oPublications { get; set; }

    }
}

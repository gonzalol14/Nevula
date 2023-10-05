using NevulaForo.Models.DB;
using NevulaForo.Validations;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class SearchVM
    {
        public string Search { get; set; } = null!;

        public string For { get; set; } = "posts";

        public List<Publication>? Publications { get; set; }

        public List<User>? Users { get; set; }
    }

    /*public class SearchPublicationsVM : SearchResultVM
    {
        public int IdUser { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual User IdUserNavigation { get; set; } = null!;
    }

    public class SearchAccountsVM : SearchResultVM
    {
        public string Username { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }*/

}

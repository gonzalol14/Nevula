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

}

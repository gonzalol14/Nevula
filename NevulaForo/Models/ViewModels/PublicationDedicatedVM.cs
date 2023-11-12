using NevulaForo.Models.DB;

namespace NevulaForo.Models.ViewModels
{
    public class PublicationDedicatedVM
    {
        public Publication? oPublication { get; set; }

        public List<Comment> oComments { get; set; }
    }
}

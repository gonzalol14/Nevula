using NevulaForo.Models.DB;

namespace NevulaForo.Services.Contract
{
    public interface IPublicationService
    {

        Task<Publication> GetPublication(int id);

        Task<Publication> SavePublication(Publication model);
    }
}

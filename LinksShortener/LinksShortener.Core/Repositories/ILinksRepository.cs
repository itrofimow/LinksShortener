using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinksShortener.Core.Repositories
{
    public interface ILinksRepository
    {
        Task Create(Link link);

        Task<Link> Hit(string url);

        Task<List<Link>> GetAll();

        Task<List<Link>> GetAllByOwner(string ownerId);
    }
}
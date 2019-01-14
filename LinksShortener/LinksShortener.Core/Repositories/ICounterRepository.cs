using System.Threading.Tasks;

namespace LinksShortener.Core.Repositories
{
    public interface ICounterRepository
    {
        Task<Counter> Inc();
    }
}
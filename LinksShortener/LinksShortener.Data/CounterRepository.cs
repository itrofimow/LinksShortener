using System.Threading.Tasks;
using LinksShortener.Core;
using LinksShortener.Core.Repositories;
using MongoDB.Driver;

namespace LinksShortener.Data
{
    public class CounterRepository : ICounterRepository
    {
        private readonly MongoContext _context;

        public CounterRepository(MongoContext context)
        {
            _context = context;
        }

        public Task<Counter> Inc()
        {
            var filter = Builders<Counter>.Filter.Where(x => x.MyId == CounterHelper.Id);
            var update = Builders<Counter>.Update.Inc(x => x.Value, 1);
            
            return _context.For<Counter>()
                .FindOneAndUpdateAsync(
                    filter, update,
                    new FindOneAndUpdateOptions<Counter>
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    });
        }
    }
}
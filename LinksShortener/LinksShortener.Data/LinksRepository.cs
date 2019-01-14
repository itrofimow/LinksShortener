using System.Collections.Generic;
using System.Threading.Tasks;
using LinksShortener.Core;
using LinksShortener.Core.Repositories;
using MongoDB.Driver;

namespace LinksShortener.Data
{
    public class LinksRepository : ILinksRepository
    {
        private readonly MongoContext _context;

        public LinksRepository(MongoContext context)
        {
            _context = context;

            CreateIndexes();
        }
        
        public Task Create(Link link)
        {
            return _context.For<Link>()
                .InsertOneAsync(link);
        }

        public Task<Link> Hit(string url)
        {
            return _context.For<Link>()
                .FindOneAndUpdateAsync(x => x.Url == url,
                    Builders<Link>.Update.Inc(y => y.Hits, 1));
        }

        public Task<List<Link>> GetAll()
        {
            return _context.For<Link>()
                .Find(_ => true)
                .ToListAsync();
        }

        public Task<List<Link>> GetAllByOwner(string ownerId)
        {
            return _context.For<Link>()
                .Find(x => x.OwnerId == ownerId)
                .ToListAsync();
        }

        private void CreateIndexes()
        {
            _context.For<Link>().Indexes.CreateMany(
                new []
                {
                    new CreateIndexModel<Link>(
                        Builders<Link>.IndexKeys.Ascending(x => x.OwnerId)
                        ),
                    new CreateIndexModel<Link>(
                        Builders<Link>.IndexKeys.Ascending(x => x.Url), new CreateIndexOptions
                        {
                            Unique = true
                        })
                }
            );
        }
    }
}
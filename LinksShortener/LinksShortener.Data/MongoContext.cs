using System;
using System.Collections.Concurrent;
using LinksShortener.Core.Mongo;
using MongoDB.Driver;

namespace LinksShortener.Data
{
    public class MongoContext
    {
        private readonly MongoUrl _mongoUrl;
        private readonly MongoClient _mongoClient;
        
        private readonly ConcurrentDictionary<Type, string> _collectionNameCache = 
            new ConcurrentDictionary<Type, string>();

        public MongoContext(string uri)
        {
            _mongoUrl = new MongoUrl(uri);

            var settings = MongoClientSettings.FromUrl(_mongoUrl);
            _mongoClient = new MongoClient(settings);
        }

        public IMongoCollection<T> For<T>()
        {
            var collectionName = _collectionNameCache.GetOrAdd(typeof(T), GetCollectionName);
            
            return _mongoClient.GetDatabase(_mongoUrl.DatabaseName).GetCollection<T>(collectionName);
        }

        private string GetCollectionName(Type entityType)
        {
            var entityAttribute = (MongoEntity) Attribute.GetCustomAttribute(entityType, typeof(MongoEntity));
            if (entityAttribute == null)
                throw new Exception($"Mark {entityType} with {typeof(MongoEntity)}");

            return entityAttribute.CollectionName;
        }
    }
}
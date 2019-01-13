using System;

namespace LinksShortener.Core.Mongo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoEntity : Attribute
    {
        public string CollectionName { get; }

        public MongoEntity(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
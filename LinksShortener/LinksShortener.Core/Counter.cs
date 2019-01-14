using System.Text;
using LinksShortener.Core.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace LinksShortener.Core
{
    [MongoEntity("Counter")]
    public class Counter
    {
        [BsonId]
        public int MyId { get; set; } = CounterHelper.Id;
        
        public long Value { get; set; }
    }

    public static class CounterHelper
    {
        public const int Id = 1;
        
        private const string Alphabet =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToUrl(Counter counter)
        {
            var sb = new StringBuilder();
            
            var value = counter.Value;
            var alpLen = Alphabet.Length;

            while (value != 0)
            {
                sb.Append(Alphabet[(int)(value % alpLen)]);
                value /= alpLen;
            }

            return sb.ToString();
        }
    }
}
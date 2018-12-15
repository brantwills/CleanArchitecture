using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.RedisDb
{
    /// <summary>
    /// wip(bwills): generic read store thinking to swap between mongo and redis?
    /// </summary>
    public class RedisLookup : IReadStoreHandler
    {
        private IContext _redis;

        public RedisLookup(IContext redis)
        {
            _redis = redis;
        }

        public void Add<T>(T item, int key)
        {
            _redis.Cache.SetHashed(
                $"{typeof(T).Name.ToLower()}:hash", 
                $"{typeof(T).Name.ToLower()}:id:{key}", 
                item);
        }

        public void DeleteById<T>(int id)
        {
            _redis.Cache.RemoveHashed(
                $"{typeof(T).Name.ToLower()}:hash", 
                $"{typeof(T).Name.ToLower()}:id:{id}");
        }

        public async Task<T> GetById<T>(int id)
        {
            return await _redis.Cache.FetchHashedAsync<T>(
                $"{typeof(T).Name.ToLower()}:hash", 
                $"{typeof(T).Name.ToLower()}:id:{id}", 
                () => null, 
                null);
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            var tList = await _redis.Cache.GetHashedAllAsync<T>($"{typeof(T).Name.ToLower()}:hash");
            return tList.Select(i => i.Value);
        }

    }
}

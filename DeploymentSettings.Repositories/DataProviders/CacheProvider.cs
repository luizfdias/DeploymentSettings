using DeploymentSettings.Repositories.DataProviders.Interfaces;
using System.Runtime.Caching;
using System;

namespace DeploymentSettings.Repositories.DataProviders
{
    public class CacheProvider : ICacheProvider
    {
        public ObjectCache Cache { get; set; }

        public DateTime ExpirationDate { get; set; }

        public CacheProvider(ObjectCache cache, DateTime expirationDate)
        {
            Cache = cache;
            ExpirationDate = expirationDate;
        }

        public CacheResult<T> GetValue<T>(string key)
        {
            var item = Cache.Get(key);

            if (item == null)
            {
                return new CacheResult<T>()
                {
                    Item = default(T),
                    Result = CacheResultEnum.NotFound
                };
            }

            return new CacheResult<T>()
            {
                Item = (T)item,
                Result = CacheResultEnum.Found
            };
        }

        public void AddOrUpdate(string key, object value)
        {
            var item = GetValue<object>(key);

            if (item == null)
            {
                Cache.Add(key, value, ExpirationDate);
            }
            else
            {
                Cache.Set(key, value, ExpirationDate);
            }
        }
    }

    public class CacheResult<T>
    {
        public CacheResultEnum Result { get; set; }

        public T Item { get; set; }
    }

    public enum CacheResultEnum
    {
        NotFound = 0,
        Found = 1
    }
}

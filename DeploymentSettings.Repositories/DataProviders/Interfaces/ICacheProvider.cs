using System;

namespace DeploymentSettings.Repositories.DataProviders.Interfaces
{
    public interface ICacheProvider
    {
        CacheResult<T> GetValue<T>(string key);

        void AddOrUpdate(string key, object value);
    }
}

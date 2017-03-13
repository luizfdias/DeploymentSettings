using DeploymentSettings.Repositories.Interfaces;
using DeploymentSettings.Repositories.DataProviders.Interfaces;
using DeploymentSettings.Repositories.DataProviders;
using DeploymentSettings.Repositories.Exceptions;
using DeploymentSettings.Repositories.Models;

namespace DeploymentSettings.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        public ICacheProvider CacheProvider { get; private set; }

        public IDataProvider DataProvider { get; private set; }

        public SettingsRepository(ICacheProvider cacheProvider, IDataProvider dataProvider)
        {
            CacheProvider = cacheProvider;
            DataProvider = dataProvider;
        }

        public T GetValue<T>(string project, string name)
        {
            var cacheResult = CacheProvider.GetValue<T>(project + name);

            if (cacheResult.Result == CacheResultEnum.Found)
            {
                return cacheResult.Item;
            }

            var item = DataProvider.GetValue<SettingsModel<T>>(x => x.Key == name, project);

            if (item == null)
            {
                throw new SettingsNotFoundException(name, project);
            }

            CacheProvider.AddOrUpdate(project + name, item.Value);

            return item.Value;
        }

        public void AddOrUpdate<T>(string project, string name, T value)
        {
            DataProvider.AddOrUpdate(new SettingsModel<T>()
            {
                Key = name,
                Value = value
            }, 
            project, x => x.Key == name);

            CacheProvider.AddOrUpdate(project + name, value);
        }
    }
}

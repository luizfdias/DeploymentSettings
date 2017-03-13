using DeploymentSettings.WebApi.Models;

namespace DeploymentSettings.WebApi.Services.Interfaces
{
    public interface ISettingsService
    {
        SettingsResult<T> GetValue<T>(string project, string name);

        void AddValue<T>(string project, string name, T value);
    }
}
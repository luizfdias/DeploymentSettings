using System;
using DeploymentSettings.Repositories.Interfaces;
using DeploymentSettings.WebApi.Models;
using DeploymentSettings.WebApi.Services.Interfaces;

namespace DeploymentSettings.WebApi.Services
{
    public class SettingsService : ISettingsService
    {
        public ISettingsRepository SettingsRepository { get; set; }

        public SettingsService(ISettingsRepository settingsRepository)
        {
            SettingsRepository = settingsRepository;
        }

        public SettingsResult<T> GetValue<T>(string project, string name)
        {
            var value = SettingsRepository.GetValue<T>(project, name);

            return new SettingsResult<T>()
            {
                Name = name,
                Value = value
            };
        }

        public void AddValue<T>(string project, string name, T value)
        {
            SettingsRepository.AddOrUpdate(project, name, value);
        }
    }
}
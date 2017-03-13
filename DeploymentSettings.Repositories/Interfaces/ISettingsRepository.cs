namespace DeploymentSettings.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        T GetValue<T>(string project, string name);

        void AddOrUpdate<T>(string project, string name, T value);
    }
}

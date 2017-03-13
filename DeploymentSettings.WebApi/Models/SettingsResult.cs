namespace DeploymentSettings.WebApi.Models
{
    public class SettingsResult<T>
    {
        public string Name { get; set; }

        public T Value { get; set; }
    }
}
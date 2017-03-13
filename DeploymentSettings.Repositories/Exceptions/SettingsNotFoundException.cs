using System;

namespace DeploymentSettings.Repositories.Exceptions
{
    public class SettingsNotFoundException : ApplicationException
    {
        public SettingsNotFoundException(string settingsName, string project) : base(string.Format("Settings {0} of project {1} not found.", settingsName, project))
        {
        }
    }
}

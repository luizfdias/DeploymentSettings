using Autofac;
using DeploymentSettings.WebApi.Services;
using DeploymentSettings.WebApi.Services.Interfaces;

namespace DeploymentSettings.WebApi.Modules
{
    public class WebApiModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsService>().As<ISettingsService>();            

            base.Load(builder);
        }
    }
}
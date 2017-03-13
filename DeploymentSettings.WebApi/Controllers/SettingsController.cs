using DeploymentSettings.WebApi.Services.Interfaces;
using System.Web.Http;

namespace DeploymentSettings.WebApi.Controllers
{
    public class SettingsController : CustomApiController
    {
        public ISettingsService SettingsService { get; set; }

        public SettingsController(ISettingsService settingsService)
        {
            SettingsService = settingsService;
        }

        [HttpGet]        
        public IHttpActionResult Get(string project, string name)
        {
            return ProcessRequest(() =>
            {
                 return Ok(SettingsService.GetValue<string>(project, name));
            });
        }

        [HttpPost]
        public IHttpActionResult Post(string project, [FromBody]SettingsObject settings)
        {
            return ProcessRequest(() =>
            {
                SettingsService.AddValue(project, settings.Name, settings.Value);
                return Ok();
            });
        }
    }

    public class SettingsObject
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}

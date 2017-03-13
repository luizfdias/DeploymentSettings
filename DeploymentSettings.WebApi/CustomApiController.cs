using System;
using System.Web.Http;

namespace DeploymentSettings.WebApi
{
    public class CustomApiController : ApiController
    {
        protected IHttpActionResult ProcessRequest(Func<IHttpActionResult> callback)
        {
            try
            {
                var result = callback.Invoke();

                return result;
            }
            catch (Exception)
            {
                return InternalServerError();                
            }            
        }
    }
}
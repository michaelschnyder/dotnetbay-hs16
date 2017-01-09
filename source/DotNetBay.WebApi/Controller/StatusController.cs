using System.Web.Http;

namespace DotNetBay.WebApi
{
    [RoutePrefix("api/status")]
    public class StatusController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult CheckStatus()
        {
            return this.Ok("I'm fine");
        }
    }
}

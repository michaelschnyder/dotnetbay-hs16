using System.Web.Http;

using DotNetBay.WebApp;

using Microsoft.Owin;

using Owin;
using DotNetBay.WebApi;

[assembly: OwinStartup(typeof(Startup))]

namespace DotNetBay.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var type = typeof(StatusController);

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}
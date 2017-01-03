using System;
using System.Threading.Tasks;
using DotNetBay.Health.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DotNetBay.SelfHost.Startup))]

namespace DotNetBay.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHealth("/health");
            //app.UseHealth();
        }
    }
}

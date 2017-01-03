using System.Web.Http;
using DotNetBay.Health.Owin;
using DotNetBay.WebApi;
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
            
            var httpConfig = new HttpConfiguration();

            httpConfig.MapHttpAttributeRoutes();

            // Option 1: Make sure the assembly is loaded in the current Context with the typeof()-"Hack"
            var type = typeof(StatusController);

            // Option 2: Custom Assemblies Resolver to load additional Assemblies when searching for Controllers
            // httpConfig.Services.Replace(typeof(IAssembliesResolver), new MyNewAssembliesResolver());

            app.UseWebApi(httpConfig);

        }

        //public class MyNewAssembliesResolver : DefaultAssembliesResolver
        //{
        //    public override ICollection<Assembly> GetAssemblies()
        //    {
        //        var baseAssemblies = base.GetAssemblies();
        //        var controllersAssembly = Assembly.LoadFrom("DotNetBay.WebApi.dll");
        //        baseAssemblies.Add(controllersAssembly);

        //        return baseAssemblies;
        //    }
        //}
    }
}

﻿using System.Web.Http;

using DotNetBay.WebApp;

using Microsoft.Owin;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            app.UseWebApi(config);

            app.MapSignalR();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
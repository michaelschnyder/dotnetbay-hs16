using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace DotNetBay.WebApp
{
    public class WebApiUnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public WebApiUnityResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiUnityResolver(container.CreateChildContainer());
        }
    }
}
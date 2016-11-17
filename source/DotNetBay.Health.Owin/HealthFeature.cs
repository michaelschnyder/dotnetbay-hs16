using Owin;

namespace DotNetBay.Health.Owin
{
    public static class HealthFeature
    {
        public static void UseHealth(this IAppBuilder appBuilder, string path)
        {
            appBuilder.Map(path, ab => ab.Use<HealthMiddleware>());
        }
    }
}

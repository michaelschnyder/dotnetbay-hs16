using System.Linq;
using System.Threading.Tasks;
using DotNetBay.Data.EF;
using Microsoft.Owin;

namespace DotNetBay.Health.Owin
{
    public class HealthMiddleware : OwinMiddleware
    {
        private EFMainRepository repository;

        public HealthMiddleware(OwinMiddleware next) : base(next)
        {
            this.repository = new EFMainRepository();

        }

        public override async Task Invoke(IOwinContext context)
        {
            await context.Response.WriteAsync("<h1>Health Information</h1>");

            await context.Response.WriteAsync("<h2>Database Connection String</h2>");
            await context.Response.WriteAsync(this.repository.Database.Connection.ConnectionString);

            await context.Response.WriteAsync("<h2>Data Health</h2>");
            await context.Response.WriteAsync("Number of Auctions: " + this.repository.GetAuctions().Count());

            await this.Next.Invoke(context);
        }
    }
}

using System;
using System.Data.Entity.SqlServer;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
        // As it is installed in the GAC, Copy Local does not work. It is required for probing.
        // Fixed "Provider not loaded" error
        SqlProviderServices ensureDLLIsCopied = SqlProviderServices.Instance;

        static void Main(string[] args)
        {
            var binding = "http://localhost:1901/";

            WebApp.Start<Startup>(binding);

            Console.WriteLine($"Webserver is running on {binding}");

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(binding);
            var response = httpClient.GetStringAsync("api/status").Result;
            Console.WriteLine($"Selfcheck call returned: {response}");

            Console.WriteLine("");
            Console.Write("Press Enter to quit.");
            Console.ReadLine();

        }
    }
}

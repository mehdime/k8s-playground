using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace StatusCoder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost
            .CreateDefaultBuilder(args)
            // Hardcode the port for now. See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1#endpoint-configuration for the various ways of setting and overriding this.
            .UseUrls("http://*:5100")
            .UseStartup<Startup>();
    }
}

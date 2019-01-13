using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LinksShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://0.0.0.0:34131")
                .UseStartup<Startup>();
    }
}
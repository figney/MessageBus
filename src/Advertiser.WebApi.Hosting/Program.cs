using Advertiser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
{
    builder.UseStartup<Startup>();
}).RunConsoleAsync();

using System.Text.Json;
using Common.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server.Utility;

namespace Server;

public static class App
{

    static IWebHost host = null!;

    static void Main()
    {

        if (AppUtility.IsSecondaryInstance())
        {
            Console.WriteLine("Existing instance detected, exiting...");
            return;
        }

        try
        {
            Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _ = Console.ReadKey();
        }

    }

    static void Run()
    {
        try
        {

            Console.WriteLine("Starting server...");
            host = CreateWebHostBuilder().Build();
            Console.WriteLine("Server started.");

            host.Run();
            Console.WriteLine("Quitting server...");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _ = Console.ReadKey();
        }

    }

    public static IWebHostBuilder CreateWebHostBuilder() =>
        new WebHostBuilder().
        UseKestrel().
        UseStartup<Startup>();

    public static void Quit() =>
        host?.StopAsync();

}

class Startup
{

    public void ConfigureServices(IServiceCollection services) =>
        services.
        AddControllers();

    public void Configure(IApplicationBuilder app) =>
        app.
        UseRouting().
        UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllers();
            _ = endpoints.MapGet("/desktop/server.json", () => JsonSerializer.Serialize(ServerUtility.GetInfo()));
            _ = endpoints.MapGet("/desktop/system-status.json", () => JsonSerializer.Serialize(SystemUtility.GetInfo()));
        });

}

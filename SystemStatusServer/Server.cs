using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server.Utility;

namespace Server;

public static class App
{

    public static string Path => typeof(App).Assembly.Location.Replace(".dll", ".exe");

    static IWebHost host = null!;

    static void Main(string[] args)
    {

        if (int.TryParse(args.ElementAtOrDefault(0), out var parentProcessID) && Process.GetProcessById(parentProcessID) is Process process)
        {

            ServerUtility.ParentProcess = parentProcessID;

            _ = Task.Run(async () =>
            {
                await process.WaitForExitAsync();
                Console.WriteLine("Parent process has quit, stopping server...");
                Quit();
            });

        }
        else
        {
            Console.WriteLine("No parent process found, stopping server...");
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

#pragma warning disable CA1822 // Mark members as static
    public void ConfigureServices(IServiceCollection services) =>
        services.
        AddControllers();

    public void Configure(IApplicationBuilder app) =>
        app.
        UseRouting().
        UseEndpoints(endpoints =>
        {

            _ = endpoints.MapControllers();

            _ = endpoints.MapGet("/desktop/system-status.json", () => JsonSerializer.Serialize(SystemUtility.GetInfo()));
            _ = endpoints.MapGet("/desktop/server.json", () => JsonSerializer.Serialize(ServerUtility.GetInfo()));

            _ = endpoints.MapGet("/desktop/notifications/queue.json", () => JsonSerializer.Serialize(NotificationUtility.ListQueue()));
            _ = endpoints.MapPost("/desktop/notifications/pop.json", () => JsonSerializer.Serialize(NotificationUtility.PopQueue()));
            _ = endpoints.MapPut("/desktop/notifications/queue", (e) =>
            {
                NotificationUtility.Queue(e.Request.Query["appTitle"][0], e.Request.Query["content"][0]);
                return Task.CompletedTask;
            });

        });
#pragma warning restore CA1822 // Mark members as static

}

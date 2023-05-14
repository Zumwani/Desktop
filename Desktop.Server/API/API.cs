using System.Text.Json;
using Server.Models;

namespace Server;

public static class API
{

    static async Task<T?> Get<T>(string endpoint)
    {

        try
        {

            using var client = new HttpClient();
            var json = await client.GetStringAsync($"http://localhost:5000/desktop/{endpoint}");
            var model = JsonSerializer.Deserialize<T>(json);

            return model;

        }
        catch (HttpRequestException e)
        {
            return default;
        }

    }

    public static async Task<SystemInfo?> GetSystemStatus() =>
        await Get<SystemInfo>("system-status.json");

    public static async Task<ServerInfo?> GetServerInfo() =>
        await Get<ServerInfo>("server.json");

}

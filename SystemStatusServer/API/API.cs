﻿using System.Text.Json;
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
        catch (HttpRequestException)
        {
            return default;
        }

    }

    //static Task Put(string endpoint)
    //{

    //    try
    //    {
    //        //using var client = new HttpClient();
    //        //_ = await client.PutAsync($"http://localhost:5000/desktop/{endpoint}");
    //    }
    //    catch (HttpRequestException)
    //    { }

    //}

    public static async Task<SystemInfo?> GetSystemStatus() =>
        await Get<SystemInfo>("system-status.json");

    public static async Task<ServerInfo?> GetServerInfo() =>
        await Get<ServerInfo>("server.json");

    //public static async Task<IEnumerable<Notification>> GetNotifications() =>
    //    await Get<Notification[]>("notifications/queue.json") ?? Array.Empty<Notification>();

    //public static async Task<IEnumerable<Notification>> PopNotifications() =>
    //    await Get<Notification[]>("notifications/pop.json") ?? Array.Empty<Notification>();

    //public static async Task QueueNotification(string appTitle, string content) =>
    //    await Put($"notifications/queue?appTitle={appTitle}&content={content}");

}

using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Desktop.Models;

namespace Desktop.Utility;

public enum WeatherUnit
{
    Metric, Imperial
}

static class WeatherUtility
{

    public static string Url =>
        $"https://api.openweathermap.org/data/2.5/weather?" +
        $"q={Settings.Weather.Current.Value!.SearchLocation}" +
        $"&units={Settings.Weather.Current.Value!.Units}" +
        $"&appid={Settings.Weather.Current.Value!.ApiKey}";

    public static bool IsValid =>
        !string.IsNullOrWhiteSpace(Settings.Weather.Current.Value?.SearchLocation) &&
        !string.IsNullOrWhiteSpace(Settings.Weather.Current.Value?.ApiKey);

    public static int LocationID { get; private set; }

    public static string WebsiteUrl =>
        "https://openweathermap.org/city/" + LocationID;

    public static async Task<Weather> Get()
    {

        try
        {

            if (!IsValid)
                return default;

            using var client = new HttpClient();
            var json = await client.GetStringAsync(Url);
            var result = JsonSerializer.Deserialize<Result>(json);

            LocationID = result.ID;

            return new()
            {
                Temperature = result.Main.FeelsLike,
                Icon = new($"http://openweathermap.org/img/w/{result.Weather[0].Icon}.png"),
            };

        }
        catch
        { }

        return default;

    }

    struct Result
    {

        [JsonPropertyName("weather")] public WeatherResult[] Weather { get; set; }
        [JsonPropertyName("main")] public MainResults Main { get; set; }
        [JsonPropertyName("id")] public int ID { get; set; }

    }

    struct WeatherResult
    {
        [JsonPropertyName("icon")] public string Icon { get; set; }
    }

    struct MainResults
    {
        [JsonPropertyName("feels_like")] public float FeelsLike { get; set; }
    }

}

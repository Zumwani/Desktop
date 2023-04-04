using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Desktop.Models;

namespace Desktop.Utility;

static class WeatherUtility
{

    public static async Task<Weather> Get()
    {

        try
        {

            using var client = new HttpClient();
            var json = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=lindesberg&units=metric&appid=39cba963c6e14e95929f371cef19f99a");
            var result = JsonSerializer.Deserialize<Result>(json);

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

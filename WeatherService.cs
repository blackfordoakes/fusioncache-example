using Newtonsoft.Json;
using ZiggyCreatures.Caching.Fusion;

namespace FusionExample;

public class WeatherService(IFusionCache cache)
{
    private const string WeatherUrl = "https://api.open-meteo.com/v1/forecast?latitude=40.440624&longitude=-79.995888&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m";

    public async Task<List<Weather>> GetWeather()
    {
        var weather = await cache.GetOrSetAsync(
            "weather_key",
            _ => _getWeatherFromSource());

        return weather;
    }

    private async Task<List<Weather>> _getWeatherFromSource()
    {
        var results = new List<Weather>();
        using var client = new HttpClient();
        var response = await client.GetAsync(WeatherUrl);
        
        if (!response.IsSuccessStatusCode)
            return results;
        
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        var weather = JsonConvert.DeserializeObject<Weather>(content);
        results.Add(weather);

        return results;
    }
}

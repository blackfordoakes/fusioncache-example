using System.Diagnostics;

namespace FusionExample;

public class App(WeatherService weatherService)
{
    private readonly WeatherService _weatherService = weatherService;

    public async Task Run(string[] args)
    {
        var watch = Stopwatch.StartNew();
        var weather = await _weatherService.GetWeather();

        watch.Stop();
        Console.WriteLine($"Records: {weather.Count()} Elapsed time: {watch.ElapsedMilliseconds}");

        watch.Restart();
        var weather2 = await _weatherService.GetWeather();
        watch.Stop();

        Console.WriteLine($"Records: {weather2.Count()} Elapsed time: {watch.ElapsedMilliseconds}");
    }
}

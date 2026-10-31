using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace WebAPI.Services;

public class WeatherForecastService : WeatherForecast.WeatherForecastBase
{
    private readonly ILogger<WeatherForecastService> _logger;
    private readonly string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public override Task<WeatherForecastReply> SayHello(WeatherForecastRequest request,
        ServerCallContext context)
    {
        var reply = new WeatherForecastReply();
        foreach (var index in Enumerable.Range(1, 5))
        {
            var item = new WeatherForecastItem
            {
                Date = Timestamp.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            };
            item.TemperatureF = 32 + (int)(item.TemperatureC / 0.5556);
            reply.Items.Add(item);
        }

        Thread.Sleep(100);

        return Task.FromResult(reply);
    }
}

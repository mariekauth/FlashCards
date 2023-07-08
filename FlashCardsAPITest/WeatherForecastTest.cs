using FlashCardsAPI;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlashCardsAPITest;

public class WeatherForecastTest
{
    [Fact]
    public void WeatherForecastReturnsArray()
    {
        var logger = new Mock<ILogger<FlashCardsAPI.Controllers.WeatherForecastController>>();
        var sut = new FlashCardsAPI.Controllers.WeatherForecastController(logger.Object);
        var expectedType = typeof(FlashCardsAPI.WeatherForecast[]);
        var actual = sut.Get();
        var actualType = actual.GetType();
        var actualFirst = actual.FirstOrDefault();

        Assert.True(actualFirst != null);
        Assert.True(typeof(DateTime) == actualFirst?.Date.GetType());
        Assert.True(typeof(int) == actualFirst?.TemperatureC.GetType());
        Assert.True(typeof(int) == actualFirst?.TemperatureF.GetType());

        Assert.Equal(expectedType, actualType);
        Assert.True(expectedType == actualType);
    }
}

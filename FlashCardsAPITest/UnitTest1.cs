using FlashCardsAPI;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlashCardsAPITest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var logger = new Mock<ILogger<FlashCardsAPI.Controllers.WeatherForecastController>>();
        var sut = new FlashCardsAPI.Controllers.WeatherForecastController(logger.Object);
        var expectedType = typeof(FlashCardsAPI.WeatherForecast[]);
        var actual = sut.Get();
        var actualType = actual.GetType();
        var actualFirst = actual.FirstOrDefault();

        Assert.True(actualFirst != null);
        Assert.True(typeof(DateTime) == actualFirst.Date.GetType());
        Assert.True(typeof(int) == actualFirst.TemperatureC.GetType());
        Assert.True(typeof(int) == actualFirst.TemperatureF.GetType());

        Assert.Equal(expectedType, actualType);
        Assert.True(expectedType == actualType);
    }
}
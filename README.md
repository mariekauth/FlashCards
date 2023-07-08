# FlashCards
Generate Flash Cards for learning

# Welcome
Used for documenting set up

# Objective
Setup and document from 0 to Complete

1. Add API (Will allow saving and retrieving Flash Cards))
2. Add Testing Module (Code should be covered at a minimum of 60%)
3. Initial DB will be in-memory only, can switch later
4. UI - Will not be tested, can change later
5. Do not want to pay for hosting sooner than necessary, will add build and deployment later.
6. Will likely eventually move main files to a library, can do later.
7. Document using and implementing design patterns.
8. Create Flash Cards for design patterns.
9. Initially will get a running app, will add flash cards and export to JSON.
10. Will use JSON as initial DB for flash cards, can update later.

# Steps
1. Create git repo with readme and license
2. Clone the repo
```
git clone git@github.com:mariekauth/FlashCards.git
```
3. Update readme and add .gitignore
4. Add FlashCardsAPI
```powershell
dotnet new webapi -n "FlashCardsAPI" -o ./FlashCardsAPI -lang C# -f net6.0
```
Run Project:
```powershell
dotnet run --project .\FlashCardsAPI\FlashCardsAPI.csproj
```
In the browser go the the url indicated, the port changes.
Check that the weather forecast works: https://localhost:{port}/WeatherForecast

Check that swagger works: https://localhost:{port}/swagger

*Note: Currently this will fail with a 404: https://localhost:{port}

5. Add xunit
```powershell
dotnet new xunit -n "FlashCardsAPITest" -o ./FlashCardsAPITest -lang C# -f net6.0
```
6. Trust the dev certificate
```
dotnet dev-certs https --trust
```
7. Configure xUnit to run unit tests, and confirm it works
```
dotnet add .\FlashCardsAPITest reference .\FlashCardsAPI\FlashcardsAPI.csproj

dotnet test .\FlashCardsAPITest\
```
*Note: A unit test was added for WeatherForecastController (for testing purposes). The test fails.

To fix this, we need to add a mock logger.
```
dotnet add .\FlashCardsAPITest package Microsoft.Extensions.Logging

dotnet add .\FlashCardsAPITest package moq

dotnet test .\FlashCardsAPITest\ --collect "XPlat Code Coverage"

reportgenerator -reports:".\FlashCardsAPITest\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereportFlashCardsAPI" -reporttypes:Html

reportgenerator -reports:".\FlashCardsAPITest\TestResults\5d984981-b751-4cf6-8aba-31257c1e0e8e\coverage.cobertura.xml" -targetdir:"coveragereportFlashCardsAPI" -reporttypes:Html

```
8. Swagger comes preinstalled. So I just need to enable it to display by default.
```C#
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
```
Run Project:
```powershell
dotnet run --project .\FlashCardsAPI\FlashCardsAPI.csproj
```
Swagger should now work by default:

In the browser:

Check that swagger works: https://localhost:{port}

Check that the weather forecast works: https://localhost:{port}/WeatherForecast

   Documentation for [Swagger](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio) can be found by clicking the link.

---
Short Cut

Set Project Name git bash
```
MY_PROJECT="FlashCards"
```

Set Project Name PowerShell

```
$MY_PROJECT="FlashCards"
```

git bash OR PowerShell
```
git init $MY_PROJECT

cd $MY_PROJECT

dotnet new webapi -n "${MY_PROJECT}API" -o ./${MY_PROJECT}API -lang C# -f net6.0

dotnet new xunit -n "${MY_PROJECT}APITest" -o ./${MY_PROJECT}APITest -lang C# -f net6.0

dotnet dev-certs https --trust

dotnet add ./${MY_PROJECT}APITest reference ./${MY_PROJECT}API/${MY_PROJECT}API.csproj

dotnet add ./${MY_PROJECT}APITest package Microsoft.Extensions.Logging

dotnet add ./${MY_PROJECT}APITest package moq

dotnet test ./${MY_PROJECT}APITest/

code .

dotnet run --project ./${MY_PROJECT}API/${MY_PROJECT}API.csproj

```

Update Files:
1. Display Swagger by default
Update FlashCardsAPI/Program.cs
```c#
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
```

2. Have a test pass
Update FlashCardsAPITest/UnitTest1.cs
```c#
    public void Test1()
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
```

3. Build and run test
```
dotnet build ./${MY_PROJECT}APITest/
dotnet test ./${MY_PROJECT}APITest/ --collect "XPlat Code Coverage"
```

4. Set the guid, to the output from above
```PowerShell
$guid=""
```

```gitBash
guid=""
```

5. Generate the html report
```
reportgenerator -reports:".\${MY_PROJECT}APITest\TestResults\${guid}\coverage.cobertura.xml" -targetdir:"coveragereportFlashCardsAPI" -reporttypes:Html
```

6. Open file explorer, and find the html output. Open it in a browser to view code coverage.

7. Confirm that the project runs and loads the swagger page by default.
```
dotnet run --project ./${MY_PROJECT}API/${MY_PROJECT}API.csproj
```
https://localhost:{port}

8. Press [CTRL][C] to stop the project from running.

9. Clean Up Project
Clean Up
```
cd ..
rm ${MY_PROJECT} -r
unset MY_PROJECT
```

*Note: If you are going to use it, instead of cleaning it up. Ensure that you add the following. 
- .gitignore (Copied from original)
- README.md
- LICENSE (Choose the appropriate one for your project)

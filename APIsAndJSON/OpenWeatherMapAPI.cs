using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace APIsAndJSON
{
    public class OpenWeatherMapAPI
    {
        public static void OpenWeatherMap()
        {
            var client = new HttpClient();
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string apiKey = configuration.GetSection("AppSettings")["ApiKey"];

            Console.WriteLine("Enter a city that you want to see the current weather conditions:"); 
            var cityName = Console.ReadLine();
            Console.WriteLine();

            string currentWeatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=imperial";

            var userResponse = client.GetStringAsync(currentWeatherUrl).Result;

            var currentWeather = JObject.Parse(userResponse);

            var windDir = (double)currentWeather["wind"]["deg"];
            var windDirection = string.Empty;
            if (windDir > 337.5 || windDir <= 22.5) { windDirection = "North"; }
            if (windDir > 22.5 && windDir <= 67.5) { windDirection = "Northeast"; }
            if (windDir > 67.5 && windDir <= 112.5) { windDirection = "East"; }
            if (windDir > 112.5 && windDir <= 157.5) { windDirection = "Southeast"; }
            if (windDir > 157.5 && windDir <= 202.5) { windDirection = "South"; }
            if (windDir > 202.5 && windDir <= 247.5) { windDirection = "Southwest"; }
            if (windDir > 247.5 && windDir <= 292.5) { windDirection = "West"; }
            if (windDir > 292.5 && windDir <= 337.5) { windDirection = "Northwest"; }

            Console.WriteLine($"The current Temperature in {cityName} is {currentWeather["main"]["temp"]} degF but feels like {currentWeather["main"]["feels_like"]} degF");
            Console.WriteLine($"The forcast calls for {currentWeather["weather"][0]["description"]}");
            Console.WriteLine($"The High Temperature will be {currentWeather["main"]["temp_max"]} degF");
            Console.WriteLine($"The Low Temperature will be {currentWeather["main"]["temp_min"]} degF");
            Console.WriteLine($"The Humidity is {currentWeather["main"]["humidity"]}%");
            Console.WriteLine($"The wind speed is {currentWeather["wind"]["speed"]} MPH due {windDirection} with gusts up to {currentWeather["wind"]["gust"]} MPH");
            Console.WriteLine();

        }
    }
}

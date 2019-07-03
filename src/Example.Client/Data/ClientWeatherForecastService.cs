using Example.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.Client.Data
{
    public class ClientWeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _http;

        public ClientWeatherForecastService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync()
        {
            var forecasts = await _http.GetJsonAsync<WeatherForecast[]>("api/WeatherForecasts");

            foreach (var f in forecasts)
            {
                f.Summary = "CLT:" + f.Summary;
            }

            return forecasts;
        }
    }
}

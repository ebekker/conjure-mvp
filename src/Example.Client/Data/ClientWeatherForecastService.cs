using Conjure.Data;
using Example.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Example.Client.Data
{
    public class ClientWeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _http;

        public ClientWeatherForecastService(HttpClient http)
        {
            _http = http;
        }

        public async Task<FetchResult<WeatherForecast>> GetForecastAsync(FetchOptions options)
        {
            //var query = new System.Collections.Specialized.NameValueCollection();
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (options.Sort!= null)
                query[nameof(options.Sort)] = options.Sort;
            if (options.Skip.HasValue)
                query[nameof(options.Skip)] = options.Skip.ToString();
            if (options.Take.HasValue)
                query[nameof(options.Take)] = options.Take.ToString();

            var forecasts = await _http.GetJsonAsync<FetchResult<WeatherForecast>>(
                "api/WeatherForecasts?" + query.ToString());

            foreach (var f in forecasts.Items)
            {
                f.Summary = "CLT:" + f.Summary;
            }

            return forecasts;
        }
    }
}

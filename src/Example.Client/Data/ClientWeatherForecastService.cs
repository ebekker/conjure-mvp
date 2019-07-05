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

        public async Task<QueryResultPage<WeatherForecast>> GetForecastAsync(string sortBy, bool? sortDesc, int? skip, int? take)
        {
            //var query = new System.Collections.Specialized.NameValueCollection();
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (sortBy != null)
                query[nameof(sortBy)] = sortBy;
            if (sortDesc ?? false)
                query[nameof(sortDesc)] = sortDesc.ToString();
            if (skip.HasValue)
                query[nameof(skip)] = skip.ToString();
            if (take.HasValue)
                query[nameof(take)] = take.ToString();

            var forecasts = await _http.GetJsonAsync<QueryResultPage<WeatherForecast>>(
                "api/WeatherForecasts?" + query.ToString());

            foreach (var f in forecasts.PageItems)
            {
                f.Summary = "CLT:" + f.Summary;
            }

            return forecasts;
        }
    }
}

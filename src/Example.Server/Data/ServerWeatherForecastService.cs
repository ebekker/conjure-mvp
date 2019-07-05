using Conjure.Data;
using Example.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Server.Data
{
    public class ServerWeatherForecastService : IWeatherForecastService
    {
        public const int RowCount = 5000;

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static DateTime _lastUpdateTime = DateTime.MinValue;
        private static IEnumerable<WeatherForecast> _lastUpdateData;

        public Task<QueryResultPage<WeatherForecast>> GetForecastAsync(string sortBy, bool? sortDesc, int? skip, int? take)
        {
            if ((DateTime.Now - _lastUpdateTime).TotalMinutes > 5.0)
            {
                lock (typeof(ServerWeatherForecastService))
                {
                    if ((DateTime.Now - _lastUpdateTime).TotalMinutes > 5.0)
                    {
                        GenerateForecasts();
                    }
                }
            }

            var rows = _lastUpdateData;

            if (sortBy != null)
            {
                var p = typeof(WeatherForecast).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase
                    | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (sortDesc ?? false)
                    rows = rows.OrderByDescending(wf => p.GetValue(wf));
                else
                    rows = rows.OrderBy(wf => p.GetValue(wf));
            }

            if (skip.HasValue)
                rows = rows.Skip(skip.Value);
            if (take.HasValue)
                rows = rows.Take(take.Value);

            return Task.FromResult(new QueryResultPage<WeatherForecast>
            {
                TotalCount = _lastUpdateData.Count(),
                PageItems = rows,
            });
        }

        private void GenerateForecasts()
        {
            var rng = new Random();
            _lastUpdateData = Enumerable.Range(1, RowCount).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = "SVR:" + Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
            _lastUpdateTime = DateTime.Now;
        }
    }
}

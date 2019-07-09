using Conjure.Data;
using Example.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public Task<FetchResult<WeatherForecast>> GetForecastAsync(FetchOptions options)
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

            if (options.Sort != null)
            {
                foreach (var sortCol in options.Sort.Split(','))
                {
                    var desc = sortCol.StartsWith("-");
                    var propName = desc ? sortCol.Substring(1) : sortCol;
                    var p = typeof(WeatherForecast).GetProperty(propName, System.Reflection.BindingFlags.IgnoreCase
                        | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    if (desc)
                        rows = rows.OrderByDescending(wf => p.GetValue(wf));
                    else
                        rows = rows.OrderBy(wf => p.GetValue(wf));
                }

            }

            if (options.Skip.HasValue)
                rows = rows.Skip(options.Skip.Value);

            if (options.Take.HasValue)
                rows = rows.Take(options.Take.Value);

            return Task.FromResult(new FetchResult<WeatherForecast>
            {
                TotalCount = _lastUpdateData.Count(),
                Items = rows,
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

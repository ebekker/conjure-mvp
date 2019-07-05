using Example.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastsController : Controller
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastsController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var forecasts = await _service.GetForecastAsync(null, null, null, null);
            var rows = forecasts.pageRows.ToArray();

            foreach (var f in rows)
            {
                f.Summary = "CTL:" + f.Summary;
            }

            return rows;
        }

        [HttpGet]
        public async Task<(int, IEnumerable<WeatherForecast>)> Get(string sortBy, bool? sortDesc, int? skip, int? take)
        {
            var forecasts = await _service.GetForecastAsync(sortBy, sortDesc, skip, take);
            var rows = forecasts.pageRows.ToArray();

            foreach (var f in rows)
            {
                f.Summary = "CTL:" + f.Summary;
            }

            return forecasts;
        }
    }
}

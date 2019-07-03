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
            var forecasts = (await _service.GetForecastAsync()).ToArray();

            foreach (var f in forecasts)
            {
                f.Summary = "CTL:" + f.Summary;
            }

            return forecasts;
        }
    }
}

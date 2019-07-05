using Conjure.Data;
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
        public async Task<QueryResultPage<WeatherForecast>> Get(string sortBy, bool? sortDesc, int? skip, int? take = 3)
        {
            var forecasts = await _service.GetForecastAsync(sortBy, sortDesc, skip, take);
            return forecasts;
        }
    }
}

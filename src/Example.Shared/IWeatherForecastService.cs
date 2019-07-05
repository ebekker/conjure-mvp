using Conjure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shared
{
    public interface IWeatherForecastService
    {
        Task<QueryResultPage<WeatherForecast>> GetForecastAsync(string sortBy, bool? sortDesc, int? skip, int? take);
    }
}

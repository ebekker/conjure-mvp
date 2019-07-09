using Conjure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shared
{
    public interface IWeatherForecastService
    {
        Task<FetchResult<WeatherForecast>> GetForecastAsync(FetchOptions options);
    }
}

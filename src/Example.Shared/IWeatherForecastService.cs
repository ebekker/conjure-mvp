﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shared
{
    public interface IWeatherForecastService
    {
        Task<(int totalRows, IEnumerable<WeatherForecast> pageRows)> GetForecastAsync(string sortBy, bool? sortDesc, int? skip, int? take);
    }
}

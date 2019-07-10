using Example.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Client.Pages
{
    public class FetchData2Model
    {
        public event EventHandler Refreshed;

        public void Refresh() => Refreshed?.Invoke(this, EventArgs.Empty);

        public WeatherForecast CurrentForecast { get; set; }
    }
}

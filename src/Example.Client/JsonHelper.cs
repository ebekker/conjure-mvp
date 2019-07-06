using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Example.Client
{
    public static class JsonHelper
    {
        public static readonly JsonSerializerOptions DefaultSerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static T Parse<T>(string json)
        {
            return JsonSerializer.Parse<T>(json, DefaultSerOptions);
        }

        public static string ToString<T>(T value)
        {
            return JsonSerializer.ToString<T>(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services.GoogleSearch
{
    public class GoogleSearchResultInformation
    {
        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; }

        [JsonPropertyName("searchTime")]
        public double SearchTime { get; set; }

        public int Total => int.TryParse(TotalResults, out int total) ? total : 0;
    }
}

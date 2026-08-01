using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services.GoogleSearch
{
    public class GoogleSearchResult
    {
        [JsonPropertyName("searchInformation")]
        public GoogleSearchResultInformation Information { get; set; }

        [JsonPropertyName("items")]
        public List<GoogleSearchResultItem> Items { get; set; } = new List<GoogleSearchResultItem>();
    }
}

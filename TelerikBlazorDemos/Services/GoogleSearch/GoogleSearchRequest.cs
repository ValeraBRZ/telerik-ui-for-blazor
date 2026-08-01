using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services.GoogleSearch
{
    internal class GoogleSearchRequest
    {
        [JsonIgnore]
        public int Skip => Page <= 1 ? 0 : (1 + (Page - 1) * PageSize);

        [JsonIgnore]
        public int Page { get; set; }

        [JsonIgnore]
        public int PageSize { get; set; }

        [JsonPropertyName("filter")]
        public string Filter
        {
            get
            {
                //filter = 0 : deactivate all filters
                //filter = p : deactivate the directory filtering
                //filter = s : deactivate the snippet filtering

                // explicitly deactivate filtering to get more results
                // and prevent Google search from not displaying similar results
                return "0";
            }
        }

        [JsonPropertyName("start")]
        public string StartIndex
        {
            get
            {
                // Google Search API allows a maximum start index of 100.
                return (Skip >= 100 ? 91 : Skip).ToString();
            }
        }

        [JsonPropertyName("num")]
        public string NumberOfResults => PageSize.ToString();

        [JsonPropertyName("cx")]
        public string SearchEngineId { get; set; }

        [JsonPropertyName("key")]
        public string SearchApiKey { get; set; }

        [JsonPropertyName("q")]
        public string Query { get; set; }

        public string AddQueryString(string url)
        {
            var requestAsDictionary = ToDictionary();
            return QueryHelpers.AddQueryString(url, requestAsDictionary);
        }

        private Dictionary<string, string> ToDictionary()
        {
            var serialized = JsonSerializer.Serialize(this);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(serialized);
            var keywords = string.Join("+", dictionary["q"].Split(" "));

            dictionary["q"] = $"intitle:{keywords}";

            return dictionary;
        }
    }
}

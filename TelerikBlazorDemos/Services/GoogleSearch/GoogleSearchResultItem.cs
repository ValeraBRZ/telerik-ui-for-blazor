using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services.GoogleSearch
{
    public class GoogleSearchResultItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("htmlSnippet")]
        public string Snippet { get; set; }

        public string ShortLink => string.Join("/", Link?.Split("/").Skip(3));
    }
}

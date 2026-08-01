using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services.GoogleSearch
{
    public class GoogleSearchService
    {
        const string SearchUrl = "https://www.googleapis.com/customsearch/v1";

        private HttpClient HttpClient { get; }

        public GoogleSearchService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<GoogleSearchResult> SearchAsync(IJSRuntime jsRuntime, string query = "", int page = 1)
        {
            if (jsRuntime != null)
            {
                // search using an XHR to workaround Google Search API restrictions for HttpClient
                var data = await SearchWithAjaxRequestAsync(jsRuntime, query, page);
                return data;
            }
            else
            {
                var data = await SearchWithHttpClientAsync(query, page);
                return data;
            }
        }

        public async Task<GoogleSearchResult> SearchWithAjaxRequestAsync(IJSRuntime jsRuntime, string query = "", int page = 1)
        {
            var requestUrl = GetSearchRequestUrl(query, page);
            var data = await jsRuntime.InvokeAsync<GoogleSearchResult>("googleSearchService.searchAsync", new { url = requestUrl });
            return data;
        }

        public async Task<GoogleSearchResult> SearchWithHttpClientAsync(string query = "", int page = 1)
        {
            var requestUrl = GetSearchRequestUrl(query, page);
            var response = await HttpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await GetSearchResultAsync(response);
                return data;
            }
            else
            {
                return null;
            }
        }

        private string GetSearchRequestUrl(string query = "", int page = 1)
        {
            var request = new GoogleSearchRequest()
            {
                // the API key can be publicly visible and doesn't need encoding
                // see docs - https://developers.google.com/custom-search/v1/introduction
                // it is restricted to requests from our sites only
                SearchApiKey = "AIzaSyAoMzKVlaLGxBlzHnHXYUekeDC1Sg0UMzY",
                SearchEngineId = "013254944667501211873:mnuhi5cobtw",
                Page = page,
                PageSize = 10,
                Query = query
            };

            var requestUrl = request.AddQueryString(SearchUrl);

            return requestUrl;
        }

        private async Task<GoogleSearchResult> GetSearchResultAsync(HttpResponseMessage response)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<GoogleSearchResult>(responseStream);

            return data;
        }
    }
}

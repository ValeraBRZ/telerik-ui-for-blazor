using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services
{
    public class JsonDataService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public JsonDataService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ValueTask<List<TItem>> GetJsonData<TItem>(string jsonUrl)
        {
            return GetJsonDataAsync<List<TItem>>(jsonUrl);
        }

        public ValueTask<TItem> GetJsonDataAsync<TItem>(string jsonUrl, string basePath = "json-data")
        {
            var jsonPath = Path.Combine(_hostingEnvironment.WebRootPath, basePath, jsonUrl);

            if (!File.Exists(jsonPath))
            {
                return default(ValueTask<TItem>);
            }

            string jsonResult = File.ReadAllText(jsonPath);

            return new ValueTask<TItem>(JsonSerializer.Deserialize<TItem>(jsonResult, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
        }

    }
}

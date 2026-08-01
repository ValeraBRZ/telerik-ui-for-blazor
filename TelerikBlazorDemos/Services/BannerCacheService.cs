using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services
{
    public class BannerCacheService
    {
        private const string AnnouncementEndPoint = "https://www.telerik.com/webapi/announcements/getMarkup?url=https://demos.telerik.com/blazor-ui";

        private DateTime? LastExecTime { get; set; }

        private string BannerContent { get; set; }

        public BannerCacheService()
        {
            _ = UpdateBannerContent();
        }

        public async Task<string> GetBannerContent()
        {
            var currentTime = DateTime.UtcNow;
            var shouldUpdate = LastExecTime == null ||
                currentTime.Subtract(LastExecTime.Value) >= TimeSpan.FromMinutes(20);

            if (shouldUpdate)
            {
                await UpdateBannerContent();
            }

            return BannerContent;
        }

        private async Task UpdateBannerContent()
        {
            // we are setting small timeout, because the endpoint is flacky and we don't want to block the rendering.
            var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1.5) };
            var response = await client.GetAsync(AnnouncementEndPoint);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                BannerContent = await response.Content.ReadAsStringAsync();
                LastExecTime = DateTime.UtcNow;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                BannerContent = string.Empty;
                LastExecTime = DateTime.UtcNow;
            }
        }
    }
}

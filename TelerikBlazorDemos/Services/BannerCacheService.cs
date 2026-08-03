using System.Threading.Tasks;

namespace TelerikBlazorDemos.Services
{
    public class BannerCacheService
    {
        public BannerCacheService()
        {
        }

        public Task<string> GetBannerContent()
        {
            // Banner is disabled.
            //return Task.FromResult(string.Empty);
            return Task.FromResult("Telerik UI for Blazor 12.3.0");
        }
    }
}

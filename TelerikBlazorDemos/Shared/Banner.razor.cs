using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TelerikBlazorDemos.Services;

namespace TelerikBlazorDemos.Shared
{
    public partial class Banner : ComponentBase
    {
        [Inject]
        public BannerCacheService BannerService { get; set; }

        public string BannerContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BannerContent = await BannerService.GetBannerContent();

            await base.OnInitializedAsync();
        }
    }
}

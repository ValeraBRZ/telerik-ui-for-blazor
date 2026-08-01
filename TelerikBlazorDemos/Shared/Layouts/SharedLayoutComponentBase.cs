using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TelerikBlazorDemos.Shared
{
    public class SharedLayoutComponentBase : LayoutComponentBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("telerikNav.initialize");
            }
        }
    }
}

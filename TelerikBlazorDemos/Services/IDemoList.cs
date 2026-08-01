using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TelerikBlazorDemos.Shared;

namespace TelerikBlazorDemos.Services
{
    public interface IDemoList
    {
        List<DemoItem> GetDemos();

        List<DemoItem> GetAllDemosFlat();

        DemoItem GetDemoFromUri(string uri);

        string GetCurrentDemoPage(NavigationManager navigationManager);
    }
}

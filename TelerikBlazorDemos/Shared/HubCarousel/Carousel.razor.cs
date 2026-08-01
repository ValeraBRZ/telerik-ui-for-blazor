using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelerikBlazorDemos.Shared.HubCarousel;

namespace TelerikBlazorDemos.Shared
{
    public partial class Carousel : ComponentBase, IDisposable
    {
        private readonly List<CarouselItem> Slides = new List<CarouselItem>()
        {
            new CarouselItem() { Title = "Finance Portfolio PWA built on WASM", Url = "https://demos.telerik.com/blazor-financial-portfolio", ImageAlt = "Finance Portfolio", ImageSrc = "images/sample-apps/finance-portfolio-pwa.png" },
            new CarouselItem() { Title = "Issues Dashboard showcasing the capabilities of the Grid and the Charts.", Url = "https://demos.telerik.com/blazor-dashboard-app/", ImageAlt = "Blazor DashBoard", ImageSrc = "images/sample-apps/blazor-demo-app.png" },
            new CarouselItem() { Title = "Blazing Coffee PWA showcasing the capabilities of the Grid, Charts, TileLayout, and more.", Url = "https://demos.telerik.com/blazor-coffee/", ImageAlt = "Blazor Coffee", ImageSrc = "images/sample-apps/blazor-coffee.png" },
            new CarouselItem() { Title = "Web-based Report Viewer for Blazor", Url = "https://www.telerik.com/products/reporting.aspx", ImageAlt = "Reporting Blazor", ImageSrc = "images/sample-apps/dashboard-reporting.png" }
        };

        [Inject]
        public IJSRuntime JsRuntimeService { get; set; }

        public string PrevButtonClass => ActiveIndex == 0 ? "disabled" : string.Empty;

        public string NextButtonClass => ActiveIndex == GroupsCount - 1 ? "disabled" : string.Empty;

        public int ActiveIndex { get; set; } = 0;

        public int GroupsCount { get; set; }

        public int ItemsInGroup { get; set; }

        private DotNetObjectReference<Carousel> DotNetReference { get; set; }

        public List<List<CarouselItem>> Groups { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitCarousel();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            DestroyCarousel();
        }

        private async Task InitCarousel()
        {
            if (DotNetReference == null)
            {
                DotNetReference = DotNetObjectReference.Create(this);
                var args = await JsRuntimeService.InvokeAsync<CarouselResizeEventArgs>("carousel.init", new
                {
                    dotNetInstance = DotNetReference,
                    resizeEvent = nameof(Carousel_OnResize)
                });

                Carousel_OnResize(args);
            }
        }

        private void DestroyCarousel()
        {
            if (DotNetReference != null)
            {
                DotNetReference.Dispose();
                JsRuntimeService.InvokeAsync<int>("carousel.destroy");
            }
        }

        private void SetGroups()
        {
            Groups = new List<List<CarouselItem>>();

            for (int i = 0; i < GroupsCount; i++)
            {
                var group = new List<CarouselItem>();
                var startSlideIndex = ItemsInGroup * i;

                group.AddRange(Slides.GetRange(
                                        startSlideIndex,
                                        Math.Min(ItemsInGroup, Slides.Count - startSlideIndex)));

                Groups.Add(group);
            }

            ActiveIndex = 0;
        }

        private void OnPrevClick()
        {
            ActiveIndex = Math.Max(0, ActiveIndex - 1);
        }

        private void OnNextClick()
        {
            ActiveIndex = Math.Min(GroupsCount - 1, ActiveIndex + 1);
        }

        private void SetActiveIndex(int index)
        {
            ActiveIndex = index;
        }

        [JSInvokable]
        public void Carousel_OnResize(CarouselResizeEventArgs args)
        {
            GroupsCount = args.GroupsCount;
            ItemsInGroup = args.ItemsInGroup;

            SetGroups();

            StateHasChanged();
        }
    }
}

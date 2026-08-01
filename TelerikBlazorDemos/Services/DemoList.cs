using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TelerikBlazorDemos.Shared;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;

namespace TelerikBlazorDemos.Services
{
    public class DemoList : IDemoList
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private List<DemoItem> _demos { get; set; }

        public DemoList(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<DemoItem> GetDemos()
        {
            if (_demos != null)
            {
                return _demos;
            }

            _demos = ReadDemos();

            foreach (DemoItem rootDemo in _demos)
            {
                if (string.IsNullOrWhiteSpace(rootDemo.Text))
                {
                    rootDemo.Text = rootDemo.Component;
                }

                rootDemo.ForEachItem((childDemo, parentDemo) =>
                {
                    childDemo.Component = rootDemo.Component;
                    childDemo.SubComponent = childDemo.SubComponent ?? parentDemo.SubComponent;
                    childDemo.Parent = parentDemo;

                    if (childDemo.ExcludeFromThemeBuilder == false)
                    {
                        childDemo.ExcludeFromThemeBuilder = rootDemo.ExcludeFromThemeBuilder;
                    }
                });

                if (!rootDemo.IsNew && rootDemo.Items.ExistsRecursive(x => x.IsNew == true || x.IsUpdated == true))
                {
                    rootDemo.IsUpdated = true;
                }
            }

            return _demos;
        }

        public List<DemoItem> GetAllDemosFlat()
        {
            // create separate reference so that we don't alter the main list used for navigation
            var demos = new List<DemoItem>(GetDemos());
            var childDemos = demos.SelectMany(d => d.Items).ToList();
            demos.AddRange(childDemos);
            return demos;
        }

        private List<DemoItem> ReadDemos()
        {
            var demosListPath = Path.Combine(_hostingEnvironment.WebRootPath, "demoList.json");

            if (!File.Exists(demosListPath))
            {
                throw new FileNotFoundException("Demos list not found at wwwroot/demoList.json.");
            }

            var demosJson = File.ReadAllText(demosListPath);
            var demos = JsonConvert.DeserializeObject<List<DemoItem>>(demosJson);

            return demos;
        }

        public DemoItem GetDemoFromUri(string uri)
        {
            string location = uri.Replace("/index", "/overview");
            var demos = GetDemos();
            var demoForUrl = demos.FindRecursive(x => x.Path?.StartsWith(location));

            if (demoForUrl != null)
            {
                return demoForUrl;
            }

            // nothing found, return an empty item to avoid issues, and try to guess the component for marginally better appearance
            string inferredComponent = location.Split("/")[0];
            inferredComponent = inferredComponent.First().ToString().ToUpper() + inferredComponent.Substring(1);

            return new DemoItem() { Component = inferredComponent };
        }

        public string GetCurrentDemoPage(NavigationManager navigationManager)
        {
            string uriWithoutQueryStringAndHash = navigationManager.Uri.Split("?")[0].Split("#")[0];
            string componentPage = uriWithoutQueryStringAndHash.Substring(Math.Min(uriWithoutQueryStringAndHash.Length, navigationManager.BaseUri.Length));
            return componentPage;
        }
    }
}

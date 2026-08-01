using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Services;

namespace TelerikBlazorDemos.Services
{
    public class CustomStringLocalizer : ITelerikStringLocalizer
    {
        public string this[string name]
        {
            get
            {
                return GetStringFromResource(name);
            }
        }

        public string GetStringFromResource(string key)
        {
            return Resources.TelerikMessages.ResourceManager.GetString(key, Resources.TelerikMessages.Culture);
        }

        public string GetStringFromDemoResource(string key)
        {
            return Resources.TelerikDemoMessages.ResourceManager.GetString(key, Resources.TelerikMessages.Culture);
        }
    }
}

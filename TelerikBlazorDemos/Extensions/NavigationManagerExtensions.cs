using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikBlazorDemos.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static void NavigateToAbsoluteUrl(this NavigationManager navigationManager, string url, bool forceLoad = false)
        {
            var absoluteUrl = navigationManager.ToAbsoluteUri(url).AbsoluteUri;
            navigationManager.NavigateTo(absoluteUrl, forceLoad);
        }

        public static bool TryGetQueryString<T>(this NavigationManager navigationManager, string key, out T value)
        {
            var absoluteUrl = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (QueryHelpers.ParseQuery(absoluteUrl.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }
            }

            value = default;

            return false;
        }

        public static string ToAbsoluteUrl(this NavigationManager navigationManager, string url)
        {
            return $"{navigationManager.BaseUri}{url}";
        }
    }
}

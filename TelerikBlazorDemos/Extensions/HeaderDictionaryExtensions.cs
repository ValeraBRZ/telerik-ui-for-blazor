using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TelerikBlazorDemos.Extensions
{
    public static class HeaderDictionaryExtensions
    {
        public static void AppendOrUpdate(this IHeaderDictionary headers, string header, string newValue)
        {
            if (headers.ContainsKey(header))
            {
                headers.Remove(header);
            }

            headers.Append(header, newValue);
        }
    }
}

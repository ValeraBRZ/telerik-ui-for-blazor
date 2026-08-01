using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace TelerikBlazorDemos.RewriteRules
{
    public class RedirectToNoQueryString : IRule
    {
        public int StatusCode { get; } = (int)HttpStatusCode.MovedPermanently;

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var path = context.HttpContext.Request.Path;
            var query = context.HttpContext.Request.Query;
            bool isGetRequest = request.Method.ToLowerInvariant() == "get";
            bool isFile = Path.HasExtension(path.Value);

            if (isGetRequest && !isFile && (path == "/blazor-ui" && query.Any()))
            {
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = StatusCode;
                response.Headers[HeaderNames.Location] = "https://demos.telerik.com/blazor-ui";
                context.Result = RuleResult.EndResponse;
            }
            else
            {
                context.Result = RuleResult.ContinueRules;
            }
        }
    }
}

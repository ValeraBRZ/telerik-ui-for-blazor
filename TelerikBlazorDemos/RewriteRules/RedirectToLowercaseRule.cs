using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace TelerikBlazorDemos.RewriteRules
{
    public class RedirectLowerCaseRule : IRule
    {
        public int StatusCode { get; } = (int)HttpStatusCode.MovedPermanently;

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var path = context.HttpContext.Request.Path;
            var host = context.HttpContext.Request.Host;
            bool isGetRequest = request.Method.ToLowerInvariant() == "get";
            bool hostHasUpper = host.HasValue && host.Value.Any(char.IsUpper);
            bool pathHasUpper = path.HasValue && path.Value.Any(char.IsUpper);
            bool isFile = Path.HasExtension(path.Value);

            if (isGetRequest && !isFile && (pathHasUpper || hostHasUpper))
            {
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = StatusCode;
                response.Headers[HeaderNames.Location] = "https://demos.telerik.com" + request.Path.Value.ToLowerInvariant() + request.QueryString;
                context.Result = RuleResult.EndResponse;
            }
            else
            {
                context.Result = RuleResult.ContinueRules;
            }
        }
    }
}

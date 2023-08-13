#nullable disable
using Microsoft.AspNetCore.Http;

namespace Common
{
    public static class HttpContextHelper
    {
        public static readonly IHttpContextAccessor _httpContextAccessor;

        static HttpContextHelper()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public static string? BaseUrl(this HttpRequest req)
        {
            if (req == null) return null;
            var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}

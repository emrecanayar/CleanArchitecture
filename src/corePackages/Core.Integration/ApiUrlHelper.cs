using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration
{
    public static class ApiUrlHelper
    {
        public static string GetApiUrlForController(string baseApiUrl, string controllerUrl, string append = null, NameValueCollection parameters = null)
        {
            var controllerUri = $"{baseApiUrl.TrimEnd('/')}/{controllerUrl.TrimStart('/')}{(!string.IsNullOrEmpty(append) ? $"/{append}" : string.Empty)}";

            if (parameters == null || parameters.Count == 0)
                return controllerUri;

            return $"{controllerUri}{(controllerUri.IndexOf('?') > -1 ? "&" : "?")}{parameters.ToQueryString()}";
        }

        private static string ToQueryString(this NameValueCollection urlParams)
        {
            return string.Join("&", urlParams.AllKeys.Distinct().Select(a => a + "=" + urlParams[a]));
        }
    }
}

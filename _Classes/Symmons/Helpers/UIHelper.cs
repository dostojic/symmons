using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class UIHelper
    {
        public static bool IsDotComHost()
        {
            return HttpContext.Current.Request.Url.Host.EndsWith(".com");
        }

        public static string GetParameterValue(string paramName, string defaultValue = "")
        {
            string retVal = defaultValue;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request[paramName]))
            {
                retVal = HttpContext.Current.Request[paramName];
            }

            return retVal;
        }
    }
}
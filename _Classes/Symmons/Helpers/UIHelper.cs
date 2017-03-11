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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace symmons.com._Classes.Custom.API
{
    public class ApiResult
    {
        public HttpStatusCode statusCode { get; set; }
        public  string description { get; set; }

        public object result { get; set; }
    }
}
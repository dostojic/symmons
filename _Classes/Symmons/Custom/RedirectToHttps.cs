using Sitecore.Pipelines.HttpRequest;
using System.Configuration;
using System.Web;

namespace symmons.com._Classes.Symmons.Custom
{
    public class RedirectToHttps : HttpRequestProcessor
    {
        private HttpContext _currentContext = null;

        public override void Process(HttpRequestArgs args)
        {
            _currentContext = args.Context;

            if (!_currentContext.Request.IsSecureConnection && ForceHttps())
            {
                _currentContext.Response.Redirect(_currentContext.Request.Url.ToString().Replace("http:", "https:"));
            }
        }

        private bool ForceHttps()
        {
            bool forceHttps = false;
            bool forceHttpsTemp;

            if (bool.TryParse(ConfigurationManager.AppSettings["ForceHttps"], out forceHttpsTemp))
            {
                forceHttps = forceHttpsTemp;

                forceHttps = forceHttps && _currentContext.Request.Url.Host.Contains("symmons.com");
            }

            return forceHttps;
        }
    }
}
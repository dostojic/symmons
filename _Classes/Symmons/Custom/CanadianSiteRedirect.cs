using System.Web;
using Sitecore.Pipelines.HttpRequest;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Helpers;

namespace symmons.com._Classes.Symmons.Custom
{
    public class CanadianSiteRedirect : HttpRequestProcessor
    {
        private HttpContext _currentContext = null;
        private Sitecore.Web.RequestUrl _requestUrl = null;

        public override void Process(HttpRequestArgs args)
        {
            _currentContext = args.Context;
            _requestUrl = args.Url;
            DoRedirect();
        }

        public void DoRedirect()
        {
            if (_currentContext == null || _requestUrl == null) return;
            //Check US/CA price
            var location = string.Empty;
            if (Sitecore.Context.Site.Name == Constants.ConstantValues.CaSiteKey)
            {
                location = "CA";
            }
            else if (Sitecore.Context.Site.Name == Constants.ConstantValues.DefaultSiteKey)
            {
                location = LocationsHelper.GetLocation(_currentContext.Request, Sitecore.Context.Site.Name);
            }
            if (location == "REDIRECT")
            {
                var newAddress = "http://" + Constants.ConstantValues.CaSitePath;
                var requestUrl = HttpContext.Current.Request.RawUrl;
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", newAddress + requestUrl);
            }
        }
    }
}
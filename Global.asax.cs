using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace symmons.com
{
    public partial class MvcApplication : Sitecore.Web.Application
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System.Web.Mvc;
using symmons.com.Areas.Symmons.Models.Pages.HomePage;
using symmons.com.Areas.Symmons.Models.Pages.Global;
using symmons.com.Areas.Symmons.Controllers.Global;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Global
{
    public class SymmonsGlobalController : SymmonsController
    {
        // *******************************************************************************************************************

        // GetGenericPage: function will render the generic page view...
        public ActionResult GetGenericPage()
        {
            var genericModel = SitecoreCurrentContext.GetCurrentItem<SymmonsGeneric>();
            return View(Constants.ViewPaths.SymmonsGeneric, genericModel);
        }

        // *******************************************************************************************************************

        // GetErrorPage: function will render the error page view...
        public ActionResult GetErrorPage()
        {
            var errorModel = SitecoreCurrentContext.GetCurrentItem<SymmonsError>();
            return View(Constants.ViewPaths.SymmonsError, errorModel);
        }

        // *******************************************************************************************************************

        // GetSitemap: function will render the sitemap view...
        public ActionResult GetSitemap()
        {
            var homePage = SitecoreCurrentContext.GetItem<HomePage>(Constants.PageIds.Home);
            return View(Constants.ViewPaths.Sitemap, homePage);
        }

        public ActionResult GetWriteReview()
        {
            return View(Constants.ViewPaths.WriteReview, null);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
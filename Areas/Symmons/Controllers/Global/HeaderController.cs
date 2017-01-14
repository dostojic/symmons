using System.Web.Mvc;
using symmons.com.Areas.Symmons.Models.Global;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com.Areas.Symmons.Controllers.Global
{
    public class HeaderController : SymmonsController
    {
        // *******************************************************************************************************************

        public ActionResult RenderHeader()
        {
            var siteSettings = SitecoreCurrentContext.GetItem<SymmonsSiteSettings>(Constants.PageIds.SiteSettings);
            return View(Constants.ViewPaths.Header, siteSettings);
        }

        // *******************************************************************************************************************

        public ActionResult ContentHeader()
        {
            return View(Constants.ViewPaths.ContentHeader);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
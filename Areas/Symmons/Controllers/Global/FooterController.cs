using System.Web.Mvc;
using symmons.com._Classes.Shared.Global;

namespace symmons.com.Areas.Symmons.Controllers.Global
{
    public class FooterController : SymmonsController
    {
        // *******************************************************************************************************************

        public ActionResult GetFooterDetails()
        {
            return View(Constants.ViewPaths.Footer, SiteSettingsItem);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
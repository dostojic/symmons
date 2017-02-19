using symmons.com._Classes.Shared.Global;
using symmons.com.Areas.Symmons.Controllers.Global;
using System.Web.Mvc;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Temptrol
{
    public class TemptrolController : SymmonsController
    {
        public ActionResult RenderTemptrol()
        {
            return View(Constants.ViewPaths.TemptrolValvePage);
        }
    }
}
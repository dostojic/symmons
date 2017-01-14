using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages;
using System.Web.Mvc;
using symmons.com._Classes.Shared.Global;

namespace symmons.com.Areas.Symmons.Controllers.Pages
{
    public class NewsandEventsController : SymmonsController
    {
        // *******************************************************************************************************************

        public ActionResult GetNewsDetail()
        {
            var newsModel = SitecoreCurrentContext.GetCurrentItem<News>();
            return View(Constants.ViewPaths.NewsDetails, newsModel);
        }

        // *******************************************************************************************************************

        public ActionResult GetEventsDetail()
        {
            var eventsModel = SitecoreCurrentContext.GetCurrentItem<Events>();
            return View(Constants.ViewPaths.EventsDetails, eventsModel);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
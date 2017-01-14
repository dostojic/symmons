using Glass.Mapper.Sc;
using symmons.com.Areas.Symmons.Controllers.Global;
using System.Web.Mvc;
using symmons.com.Areas.Symmons.Models.Modules.Home;
using symmons.com.Areas.Symmons.Models.Pages.HomePage;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Global
{
    public class HomePageController : SymmonsController
    {
        // *******************************************************************************************************************

        public ActionResult RenderHomePage()
        {
            #region Clear All Sessions
            Session[Constants.SessionConstants.FeatureId] = null;
            Session[Constants.SessionConstants.CollectionId] = null;
            Session[Constants.SessionConstants.CategoryId] = null;
            Session[Constants.SessionConstants.ProductFamily] = null;
            Session[Constants.SessionConstants.ProductSegment] = null;
            Session[Constants.SessionConstants.StyleId] = null;
            Session[Constants.SessionConstants.Keyword] = null;
            Session[Constants.SessionConstants.ContentSearchCount] = null;
            #endregion

            var homePage = SitecoreCurrentContext.GetItem<HomePage>(Constants.PageIds.Home);
            return View(Constants.ViewPaths.HomePage, homePage);
        }

        // *******************************************************************************************************************

        public ActionResult GetHeroSlider()
        {
            var heroSliderModule = (DatasourceItem == null ? null : DatasourceItem.GlassCast<HeroSlideModule>());
            return View(Constants.ViewPaths.HeroSlider, heroSliderModule);
        }

        // *******************************************************************************************************************
        public ActionResult GetStyleHeroSlider()
        {
            var styleHeroSliderModule = (DatasourceItem == null ? null : DatasourceItem.GlassCast<StyleHeroSlideModule>());
            return View(Constants.ViewPaths.StyleHeroSlider, styleHeroSliderModule);
        }

        // *******************************************************************************************************************
    }
}
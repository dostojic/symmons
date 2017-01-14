using System.Web.Mvc;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Shared.Global;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Products
{
    public class CollectionStyleFeatureController : SymmonsController
    {
        // *******************************************************************************************************************

        public ActionResult GetCollection()
        {
            var productCollectionModel = SitecoreCurrentContext.GetCurrentItem<ProductCollection>();
            return View(Constants.ViewPaths.CollectionLanding, productCollectionModel);
        }

        // *******************************************************************************************************************

        public ActionResult GetStyle()
        {
            var productStyleModel = SitecoreCurrentContext.GetCurrentItem<ProductStyle>();
            return View(Constants.ViewPaths.StyleLanding, productStyleModel);
        }

        // *******************************************************************************************************************

        public ActionResult GetFeature()
        {
            var productSmartFeaturesModel = SitecoreCurrentContext.GetCurrentItem<ProductSmartFeatures>();
            return View(Constants.ViewPaths.FeatureLanding, productSmartFeaturesModel);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
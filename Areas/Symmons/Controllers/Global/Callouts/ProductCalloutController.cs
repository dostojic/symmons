using System.Web.Mvc;
using Glass.Mapper.Sc;
using symmons.com.Areas.Symmons.Models.Modules.Callouts;
using symmons.com.Areas.Symmons.Models.Modules.Callouts.Products;
using symmons.com._Classes.Shared.Global;

namespace symmons.com.Areas.Symmons.Controllers.Global.Callouts
{
    public class ProductCalloutController : SymmonsController
    {
        public ActionResult CategoryCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<CategoryCallout>());
            return View(Constants.ViewPaths.CategoryCallout, calloutModel);
        }

        public ActionResult CollectionCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<CollectionCallout>());
            return View(Constants.ViewPaths.CollectionCallout, calloutModel);
        }

        public ActionResult FamilyCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<FamilyCallout>());
            return View(Constants.ViewPaths.FamilyCallout, calloutModel);
        }

        public ActionResult ApprovedProductCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<ApprovedProductsCallout>());
            return View(Constants.ViewPaths.ApprovedProductsCallout, calloutModel);
        }

        public ActionResult GetDesignStudioRtf()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<RichTextBlockCallout>());
            return View(Constants.ViewPaths.DesignStudioProduct, calloutModel);
        }
    }
}
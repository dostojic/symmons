using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Glass.Mapper.Sc;
using symmons.com._Classes.Shared.Global;
using symmons.com.Areas.Symmons.Models.Modules.Callouts;
using System.Web.Mvc;
using symmons.com._Classes.Symmons.Global;
using Verndale.SharedSource.Helpers;
using Sitecore.Data.Items;

namespace symmons.com.Areas.Symmons.Controllers.Global.Callouts
{
    public class CalloutController : SymmonsController
    {
        public ActionResult RenderRichTextBlockCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<RichTextBlockCallout>());
            return View(Constants.ViewPaths.RichTextCallout, calloutModel);
        }

        public ActionResult RenderImageCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<ImageCallout>());
            return View(Constants.ViewPaths.ImageCallout, calloutModel);
        }

        public ActionResult RenderShareThisCallout()
        {
            ViewBag.Publisher = SiteSettingsItem.ShareThisPublisherId;
            return View(Constants.ViewPaths.ShareThisCallout);
        }

        public ActionResult GetLatestVideos()
        {
            var galleryModule = (DatasourceItem == null ? null : DatasourceItem.GlassCast<GalleryModule>());
            if (galleryModule != null)
            {
                galleryModule.GalleryClass = galleryModule.MaxVideos ? Constants.ConstantValues.GalleryVertical : Constants.ConstantValues.GalleryHorizontal;
                Session[Constants.ConstantValues.SessionGalleryData] = galleryModule.GalleryList.Where(x => x.Thumbnail.Src != null).ToList();
                Session[Constants.ConstantValues.SessionMaxVideos] = galleryModule.MaxVideos;
            }
            return View(Constants.ViewPaths.LatestVideosCallout, galleryModule);
        }

        public JsonResult GetGalleryData()
        {
            List<Gallery> galleryList = null;
            if (Session[Constants.ConstantValues.SessionGalleryData] != null)
            {
                galleryList = (List<Gallery>)Session[Constants.ConstantValues.SessionGalleryData];
            }
            if (galleryList == null)
            {
                return null;
            }

            var galleryListingData = new List<GalleryListingData>();
            var galleryData = (bool)Session[Constants.ConstantValues.SessionMaxVideos]
                ? galleryList : galleryList.Take(4);

            var galleryItemList = galleryData as IList<Gallery> ?? galleryData.ToList();
            for (var item = 0; item < galleryItemList.Count(); item++)
            {
                galleryListingData.Add(new GalleryListingData
                {
                    ListingId = item.ToString(CultureInfo.InvariantCulture),
                    ListingType = string.IsNullOrEmpty(galleryList[item].Video) ? Constants.ConstantValues.ConstantImage : Constants.ConstantValues.ConstantVideo,
                    ListingTitle = galleryList[item].Headline.Text,
                    ListingUrl = galleryList[item].Headline.Url,
                    ListingThumbImageUrl = galleryList[item].Thumbnail.Src,
                    ListingImageAlt = galleryList[item].Thumbnail.Alt,
                    ListingVideoUrl = galleryList[item].Video,
                    ListingImageUrl = galleryList[item].Image.Src,
                    ListingTeaserDesc = galleryList[item].Description
                });
            }
            var masterListing = new MasterListing { MasterListingData = galleryListingData };
            return Json(masterListing, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<MapCallout>());
            return View(Constants.ViewPaths.MapCallout, calloutModel);
        }

        public ActionResult FaqCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<FaqCallout>());
            return View(Constants.ViewPaths.Faq, calloutModel);
        }

        public ActionResult ProductLibraryCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<ProductGenericCallout>());
            return View(Constants.ViewPaths.ProductLibraryCallout, calloutModel);
        }

        public ActionResult WheretoBuyCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<ProductGenericCallout>());
            return View(Constants.ViewPaths.WheretoBuyCallout, calloutModel);
        }

        public ActionResult DocumentSearchPage(string keyword)
        {
            var searchPageUrl = string.Empty;
            keyword = keyword.Trim();

            var productItems = GetProductItems(keyword);
            var mediaItems = GetMediaItems(keyword);

            if ((productItems.Any() && mediaItems.Any()) || (!productItems.Any() && !mediaItems.Any()))
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.HybridSearchPage);
            }
            else if (productItems.Any())
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.ProductBrowse);
            }
            else if (mediaItems.Any())
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.ContentSearchPage);
            }

            searchPageUrl += "?" + Constants.QueryString.Keyword + "=" + keyword + "&" +
                       Constants.QueryString.IsFromCallout + "=true";
            return Redirect(searchPageUrl);
        }

        private static List<Item> GetProductItems(string keyword)
        {
            var productItems = SearchHelper.GetItems(Constants.SearchIndex.Products,
                Sitecore.Context.Language.ToString(), Constants.TemplateIds.ProductDetailsTemplateId,
                Constants.FolderIds.ProductsRepository, keyword, null) ?? new List<Item>();

            return productItems;
        }

        private static List<Item> GetMediaItems(string keyword)
        {
            var mediaItems = SearchHelper.GetItems(Constants.SearchIndex.Media, Sitecore.Context.Language.ToString(),
                Constants.TemplateIds.MediaItems, Constants.PageIds.MediaSymmons, keyword, null) ?? new List<Item>();

            return mediaItems;
        }

        public ActionResult WheretoBuyPage(string keyword)
        {
            keyword = keyword.Trim();

            var pageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.WhereToBuy);
            pageUrl += "?" + Constants.QueryString.Keyword + "=" + keyword;

            return Redirect(pageUrl);
        }

        // Render the design studio timelines callout...
        public ActionResult RenderTimelinesCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<TimelineModule>());
            return View(Constants.ViewPaths.SymmonsTimelineCallout, calloutModel);
        }

        // Render the design studio timelines video callout...
        public ActionResult RenderTimelinesVideoCallout()
        {
            var calloutModel = (DatasourceItem == null ? null : DatasourceItem.GlassCast<SymmonsVideo>());
            return View(Constants.ViewPaths.SymmonsTimelineVideoCallout, calloutModel);
        }
    }
}
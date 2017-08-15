using Sitecore.Data.Items;
using symmons.com._Classes.Shared.Global;
using symmons.com.Areas.Symmons.Models.Pages.CaseStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verndale.SharedSource.Helpers;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class CaseStudyHelper
    {
        public static List<CaseStudyApiModel> GetCaseStudies()
        {
            var caseStudies = new List<CaseStudyApiModel>();
            var caseStudiesList = SitecoreHelper.ItemMethods.GetItemFromGUID("{1142C390-53E9-4005-9ABD-9DC52CEEC2ED}");

            try
            {
                var items =
                    caseStudiesList.Axes.GetDescendants()
                        .Where(x => x.TemplateName.ToString().Equals("Symmons Generic"))
                        .ToList();

                caseStudies = ConvertItemsToCaseStudies(items);
            }

            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("CaseStudies API: Error in GetCaseStudies. Exception message:{0}",
                        exception.Message), new object());
            }

            return caseStudies;
        }

        private static List<CaseStudyApiModel> ConvertItemsToCaseStudies(List<Item> items)
        {
            var caseStudies = new List<CaseStudyApiModel>();

            foreach (var item in items)
            {
                var caseStudy = new CaseStudyApiModel();
                caseStudy.Name = item.Name;
                caseStudy.PageTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Page Title", item, false);
                caseStudy.PageSubTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Page SubTitle", item, false);
                caseStudy.NavigationTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Navigation Title", item, false);
                caseStudy.Teaser = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Teaser", item, false);

                string listingImageUrl;
                if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Listing Image", item, out listingImageUrl))
                {
                    listingImageUrl = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + listingImageUrl;
                }

                caseStudy.ListingImage = !(string.IsNullOrEmpty(listingImageUrl)) ? listingImageUrl : string.Empty;

                string pageImageUrl;
                if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Page Image", item, out pageImageUrl))
                {
                    pageImageUrl = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + pageImageUrl;
                }

                caseStudy.PageImage = !(string.IsNullOrEmpty(pageImageUrl)) ? pageImageUrl : string.Empty;
                caseStudy.MainContent = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Main Content", item, false);

                string link;
                if (SitecoreHelper.ItemRenderMethods.GetGeneralLinkURL(item, "Link", out link))
                {
                    link = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + link;
                }

                caseStudy.Link = !(string.IsNullOrEmpty(link)) ? link : string.Empty;

                string redirectLink;
                if (SitecoreHelper.ItemRenderMethods.GetGeneralLinkURL(item, "Redirect Link", out redirectLink))
                {
                    redirectLink = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + redirectLink;
                }

                caseStudy.RedirectLink = !(string.IsNullOrEmpty(redirectLink)) ? redirectLink : string.Empty;
                caseStudy.RedirectType = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Redirect Type", item, false);

                caseStudy.ShowOnNavigation = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Show On Navigation", item, false);
                caseStudy.ShowOnHtmlSitemap = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Show On Html SiteMap", item, false);
                caseStudy.ShowOnXmlSitemap = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Show On Xml SiteMap", item, false);

                caseStudy.SeoTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Title", item, false);
                caseStudy.SeoDescription = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Description", item, false);
                caseStudy.SeoKeywords = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Keywords", item, false);
                caseStudy.MetaRobotsNoIndex = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Meta Robots NOINDEX", item, false);
                caseStudy.MetaRobotsNoFollow = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Meta Robots NOFOLLOW", item, false);
                caseStudy.SeoGoogleAnalyticsSnippet = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Google Analytics Snippet", item, false);

                caseStudy.RightImages = GetRightImages(item.ID.ToString());
            
                caseStudies.Add(caseStudy);
            }

            return caseStudies;
        }

        private static List<string> GetRightImages(string itemId)
        {
            var images = new List<string>();
            var dataSources = RenderingHelper.GetDataSources(itemId, "scRight");

            foreach (string dataSource in dataSources)
            {
                var item = SitecoreHelper.ItemMethods.GetItemByPath(dataSource);

                if (item.Template.Name == "Image Callout")
                {
                    string imageUrl;
                    if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Image", item, out imageUrl))
                    {
                        imageUrl = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + imageUrl;
                        images.Add(imageUrl);
                    }
                }
            }

            return images;
        }
    }
}
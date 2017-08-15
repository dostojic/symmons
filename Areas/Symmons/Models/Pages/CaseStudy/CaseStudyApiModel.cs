using System.Collections.Generic;

namespace symmons.com.Areas.Symmons.Models.Pages.CaseStudy
{
    public class CaseStudyApiModel
    {
        public CaseStudyApiModel()
        {
            RightImages = new List<string>();
        }

        public string Name { get; set; }

        public string PageTitle { get; set; }

        public string PageSubTitle { get; set; }

        public string NavigationTitle { get; set; }

        public string Teaser { get; set; }

        public string ListingImage { get; set; }

        public string PageImage { get; set; }

        public string MainContent { get; set; }

        public string Link { get; set; }

        public string ShowOnNavigation { get; set; }

        public string ShowOnHtmlSitemap { get; set; }

        public string ShowOnXmlSitemap { get; set; }

        public string RedirectLink { get; set; }

        public string RedirectType { get; set; }

        public string SeoTitle { get; set; }

        public string SeoKeywords { get; set; }

        public string MetaRobotsNoIndex { get; set; }

        public string MetaRobotsNoFollow { get; set; }

        public string SeoDescription { get; set; }

        public string SeoCanonialUrl { get; set; }

        public string SeoGoogleAnalyticsSnippet { get; set; }

        public List<string> RightImages { get; set; }
    }
}
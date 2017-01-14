using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{EF3A0418-8A7B-4932-93BD-93899CD53856}", AutoMap = true)]
    public class ProductStyle : ISymmonsBasePage
    {
        public virtual Guid Id { get; set; }

        public virtual int Version { get; set; }

        public virtual string Url { get; set; }

        public virtual string PageTitle { get; set; }

        public virtual string PageSubTitle { get; set; }

        public virtual string NavigationTitle { get; set; }

        public virtual string MainContent { get; set; }

        public virtual Image PageImage { get; set; }

        public virtual string Teaser { get; set; }

        public virtual bool ShowOnNavigation { get; set; }

        public virtual bool ShowOnHtmlSiteMap { get; set; }

        public virtual bool ShowOnXmlSiteMap { get; set; }

        public virtual string SeoTitle { get; set; }

        public virtual string SeoKeywords { get; set; }

        public virtual bool MetaRobotsNoIndex { get; set; }

        public virtual bool MetaRobotsNofollow { get; set; }

        public virtual string SeoDescription { get; set; }

        public virtual Link SeoCanonicalReference { get; set; }

        public virtual string SeoGoogleAnalyticsSnippet { get; set; }

        [SitecoreField(FieldName = "Redirect Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link RedirectLink { get; set; }

        [SitecoreField(FieldName = "Redirect Type")]
        public virtual Guid RedirectType { get; set; }

        [SitecoreField(FieldName = "Bath Small Style Image")]
        public virtual Image BathSmallStyleImage { get; set; }

        [SitecoreField(FieldName = "Kitchen Small Style Image")]
        public virtual Image KitchenSmallStyleImage { get; set; }


        public virtual Image ListingImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
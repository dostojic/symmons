using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;

namespace symmons.com.Areas.Symmons.Models.Pages
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{CA8308D4-0F9F-4898-8058-6A42B7121A09}", AutoMap = true)]
    public class ContactUs : ISymmonsBasePage
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

        public virtual string SeoDescription { get; set; }

        public virtual bool MetaRobotsNoIndex { get; set; }

        public virtual bool MetaRobotsNofollow { get; set; }

        public virtual Link SeoCanonicalReference { get; set; }

        public virtual string SeoGoogleAnalyticsSnippet { get; set; }

        [SitecoreField(FieldName = "Redirect Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link RedirectLink { get; set; }

        [SitecoreField(FieldName = "Redirect Type")]
        public virtual  Guid RedirectType { get; set; }

        [SitecoreField(FieldName = "Webmaster Email Id")]
        public virtual string WebmasterEmailId { get; set; }

        [SitecoreField(FieldName = "Contact Us Email Id")]
        public virtual string ContactUsEmailId { get; set; }

        [SitecoreField(FieldName = "Contact Us Email Get Started Id")]
        public virtual string ContactUsEmailGetStartedId { get; set; }

        [SitecoreField(FieldName = "Contact Us Mail Subject")]
        public virtual string ContactUsMailSubject { get; set; }

        public virtual Image ListingImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
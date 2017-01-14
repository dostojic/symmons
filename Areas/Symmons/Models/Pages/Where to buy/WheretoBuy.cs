using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;
using symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy;


namespace symmons.com.Areas.Symmons.Models.Pages
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{F6A3A272-BD19-448B-A2C9-96ED9B5CADD3}", AutoMap = true)]
    public class WheretoBuy : ISymmonsBasePage
    {
        [SitecoreId]
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

        [SitecoreField(FieldName = "Shop Online")]
        public virtual string ShopOnline { get; set; }

        [SitecoreField(FieldName = "CAN Shop Online")]
        public virtual string CanShopOnline { get; set; }

        public List<WhereToBuyStoreTypes> WhereToBuyStoreTypes { get; set; }

        public virtual Image ListingImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}

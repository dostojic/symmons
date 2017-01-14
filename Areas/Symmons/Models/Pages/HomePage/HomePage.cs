using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;
using symmons.com.Areas.Symmons.Models.Modules.Home;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Pages.HomePage
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{1C106469-9905-4975-8E07-EEE5FCE68DC9}", AutoMap = true)]
    public class HomePage : ISymmonsBasePage
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

        [SitecoreField(FieldName = "Symmons Commercial Title")]
        public string SymmonsCommercialTitle { get; set; }

        [SitecoreField(FieldName = "Symmons for Commercial List")]
        public IEnumerable<SuperCategory> SymmonsForCommercialList { get; set; }

        [SitecoreField(FieldName = "Symmons Home Title")]
        public string SymmonsHomeTitle { get; set; }

        [SitecoreField(FieldName = "Symmons for Home List")]
        public IEnumerable<SuperCategory> SymmonsForHomeList { get; set; }

        [SitecoreField(FieldName = "Icons List")]
        public IEnumerable<IconCallout> IconsList { get; set; }

        public virtual Image ListingImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
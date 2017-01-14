using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Modules.Global;

namespace symmons.com.Areas.Symmons.Models.Global.Base
{
    [SitecoreType(TemplateId = "{36879F8F-82C5-4829-88E0-23AB8F07F5A7}", AutoMap = true)]
    public interface ISymmonsBasePage
    {
        [SitecoreId]
        Guid Id { get; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        string Url { get; }

        [SitecoreField(FieldName = "Page Title")]
        string PageTitle { get; set; }

        [SitecoreField(FieldName = "Page Subtitle")]
        string PageSubTitle { get; set; }

        [SitecoreField(FieldName = "Navigation Title")]
        string NavigationTitle { get; set; }

        [SitecoreField(FieldName = "Main Content")]
        string MainContent { get; set; }

        [SitecoreField(FieldName = "Listing Image")]
        Image ListingImage { get; set; }

        [SitecoreField(FieldName = "Page Image")]
        Image PageImage { get; set; }

        [SitecoreField(FieldName = "Teaser")]
        string Teaser { get; set; }

        [SitecoreField(FieldName = "Show On Navigation")]
        bool ShowOnNavigation { get; set; }

        [SitecoreField(FieldName = "Show On Html SiteMap")]
        bool ShowOnHtmlSiteMap { get; set; }

        [SitecoreField(FieldName = "Show On Xml SiteMap")]
        bool ShowOnXmlSiteMap { get; set; }
        
        [SitecoreField(FieldName = "SEO Title")]
        string SeoTitle { get; set; }

        [SitecoreField(FieldName = "SEO Keywords")]
        string SeoKeywords { get; set; }

        [SitecoreField(FieldName = "Meta Robots NOINDEX")]
        bool MetaRobotsNoIndex { get; set; }

        [SitecoreField(FieldName = "Meta Robots NOFOLLOW")]
        bool MetaRobotsNofollow { get; set; }

        [SitecoreField(FieldName = "SEO Description")]
        string SeoDescription { get; set; }

        [SitecoreField(FieldName = "SEO Canonical Reference", FieldType = SitecoreFieldType.GeneralLink)]
        Link SeoCanonicalReference { get; set; }

        [SitecoreField(FieldName = "SEO Google Analytics Snippet")]
        string SeoGoogleAnalyticsSnippet { get; set; }

        [SitecoreField(FieldName = "Redirect Link", FieldType = SitecoreFieldType.GeneralLink)]
        Link RedirectLink { get; set; }

        [SitecoreField(FieldName = "Redirect Type")]
        Guid RedirectType { get; set; }
    }
}

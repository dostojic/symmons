using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;
using System;

namespace symmons.com.Areas.Symmons.Models.Pages.Temptrol
{
    [SitecoreType(TemplateId = "{9D359B08-80AB-4ED3-A177-F98605ED40B9}", AutoMap = true)]
    public class Temptrol : ISymmonsBasePage
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

        public Image ListingImage { get; set; }

        public Link RedirectLink { get; set; }
        
        public Guid RedirectType { get; set; }

        [SitecoreField(FieldName = "Temptrol Image")]
        public virtual Image TemptrolImage { get; set;  }

        [SitecoreField(FieldName = "Logo")]
        public virtual Image Logo { get; set; }

        [SitecoreField(FieldName = "Features")]
        public virtual string Features { get; set; }

        [SitecoreField(FieldName = "Feature Image")]
        public virtual Image FeatureImage { get; set; }

        [SitecoreField(FieldName = "Feature Link")]
        public virtual Link FeatureLink { get; set;  }

        [SitecoreField(FieldName = "Middle Section Title")]
        public virtual string MiddleSectionTitle { get; set; }

        [SitecoreField(FieldName = "Middle Section Subtitle")]
        public virtual string MiddleSectionSubtitle { get; set; }

        [SitecoreField(FieldName = "Middle Section Text")]
        public virtual string MiddleSectionText { get; set; }

        [SitecoreField(FieldName = "Middle Section Image")]
        public virtual Image MiddleSectionImage { get; set; }

        [SitecoreField(FieldName = "Right Control 1 Title")]
        public virtual string RightControl1Title { get; set; }

        [SitecoreField(FieldName = "Right Control 1 Text")]
        public virtual string RightControl1Text { get; set;  }

        [SitecoreField(FieldName = "Right Control 1 Image")]
        public virtual Image RightControl1Image { get; set; }

        [SitecoreField(FieldName = "Right Control 2 Title")]
        public virtual string RightControl2Title { get; set; }

        [SitecoreField(FieldName = "Right Control 2 Text")]
        public virtual string RightControl2Text { get; set; }

        [SitecoreField(FieldName = "Right Control 2 Image")]
        public virtual Image RightControl2Image { get; set; }

        [SitecoreField(FieldName = "Bottom Section Title")]
        public virtual string BottomSectionTitle { get; set; }

        [SitecoreField(FieldName = "Bottom Section Text")]
        public virtual string BottomSectionText { get; set; }

        [SitecoreField(FieldName = "Bottom Section Image")]
        public virtual Image BottomSectionImage { get; set; }

        [SitecoreField(FieldName = "Left Column Link")]
        public virtual Link LeftColumnLink { get; set; }

        [SitecoreField(FieldName = "Left Column Text")]
        public virtual string LeftColumnText { get; set; }

        [SitecoreField(FieldName = "Left Column Image")]
        public virtual Image LeftColumnImage { get; set; }

        [SitecoreField(FieldName = "Right Column Link")]
        public virtual Link RightColumnLink { get; set; }

        [SitecoreField(FieldName = "Right Column Text")]
        public virtual string RightColumnText { get; set; }

        [SitecoreField(FieldName = "Right Column Image")]
        public virtual Image RightColumnImage { get; set; }

        [SitecoreField(FieldName = "Navigation Image")]
        public virtual Image NavigationImage { get; set; }

        [SitecoreField(FieldName = "Navigation Subtitle")]
        public virtual string NavigationSubtitle { get; set; }

        [SitecoreField(FieldName = "Navigation Text")]
        public virtual string NavigationText { get; set; }
    }
}
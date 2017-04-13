using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;

namespace symmons.com.Areas.Symmons.Models.Pages
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{E9B219A0-05AB-48DA-AD9D-161F0FB472F4}", AutoMap = true)]
    public class Events : ISymmonsBasePage
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

        [SitecoreField(FieldName = "Event Start Date")]
        public virtual DateTime EventStartDate { get; set; }

        [SitecoreField(FieldName = "Event End Date")]
        public virtual DateTime EventEndDate { get; set; }

        [SitecoreField(FieldName = "Event Listing Image")]
        public virtual Image EventsListingImage { get; set; }

        [SitecoreField(FieldName = "Event Location")]
        public virtual Link EventLocation { get; set; }

        [SitecoreField(FieldName = "Script Event")]
        public virtual string ScriptEvent { get; set; }

        [SitecoreField(FieldName = "Content Image")]
        public virtual Image ContentImage { get; set; }

        public virtual string EventDate
        {
            get
            {
                if (!EventStartDate.Equals(DateTime.MinValue) && !EventEndDate.Equals(DateTime.MinValue))
                {
                    if (EventStartDate.Year != EventEndDate.Year || EventStartDate.Month != EventEndDate.Month)
                    {
                        if (EventStartDate.Year != EventEndDate.Year)
                        {
                            return EventStartDate.ToString("MMM d, yyyy") +
                               "-" + EventEndDate.ToString("MMM d, yyyy");
                        }
                        return EventStartDate.ToString("MMM d") + "-" +
                                   EventEndDate.ToString("MMM d, yyyy");
                    }
                    return EventStartDate.ToString("MMM d") + "-" + EventEndDate.ToString("d, yyyy");
                }

                return !EventStartDate.Equals(DateTime.MinValue)
                    ? EventStartDate.ToString("MMM d, yyyy")
                    : (!EventEndDate.Equals(DateTime.MinValue) ? EventEndDate.ToString("MMM d, yyyy") : null);
            }
        }

        public virtual Image ListingImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
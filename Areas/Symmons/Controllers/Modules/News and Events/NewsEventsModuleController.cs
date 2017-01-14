using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Modules.Callouts;
using symmons.com.Areas.Symmons.Models.Pages;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;

namespace symmons.com.Areas.Symmons.Controllers.Modules
{
    public class NewsEventsModuleController : SymmonsController
    {
        // *******************************************************************************************************************

        public NewsandEventsLanding NewsandEventsLandingModel { get; set; }
        public EventsListing EventsListingModel { get; set; }

        // *******************************************************************************************************************

        // To get the Featured News based on Datasource item or based on "News and Events Landing" page item...
        public ActionResult GetFeaturedNews()
        {
            var featuredNewsCalloutItem = (DatasourceItem == null ? null : DatasourceItem.GlassCast<FeaturedNews>());
            if (featuredNewsCalloutItem != null)
            {
                var featuredNewsModalItem = SitecoreCurrentContext.GetItem<FeaturedNews>(featuredNewsCalloutItem.Id);
                if (featuredNewsModalItem != null)
                {
                    return View(Constants.ViewPaths.FeaturedNews, featuredNewsModalItem);
                }
            }
            return null;
        }

        // *******************************************************************************************************************

        // Function will get the upcoming events based on CA configuration...
        // If there are no events configured, then recent 3 events will be shown...
        public ActionResult GetUpcomingEvents()
        {
            var upcomingEventsCalloutItem = (DatasourceItem == null ? null : DatasourceItem.GlassCast<UpcomingEventsCallout>());
            var upcomingEventsCalloutModelItem = new UpcomingEventsCallout();
            if (upcomingEventsCalloutItem != null)
            {
                upcomingEventsCalloutModelItem = SitecoreCurrentContext.GetItem<UpcomingEventsCallout>(upcomingEventsCalloutItem.Id);
                upcomingEventsCalloutModelItem.UpcomingEvents = upcomingEventsCalloutModelItem.UpcomingEvents.Any() ? upcomingEventsCalloutModelItem.UpcomingEvents.ToList() : GetDynamicUpcomingEvents();
            }
            else
            {
                upcomingEventsCalloutModelItem.UpcomingEventsTitle = Translate.Text(Constants.Dictionary.UpcomingEventsTitle);
                upcomingEventsCalloutModelItem.UpcomingEvents = GetDynamicUpcomingEvents();
            }
            return View(Constants.ViewPaths.UpcomingEvents, upcomingEventsCalloutModelItem);
        }

        // *******************************************************************************************************************

        // Get Recent News based on datasource item configured...
        // if there are no recent news items configured, then system won't show the placeholder also...
        public ActionResult GetRecentNews()
        {
            var recentNewsCalloutItem = (DatasourceItem == null ? null : DatasourceItem.GlassCast<RecentNewsCallout>());
            if (recentNewsCalloutItem != null)
            {
                var recentNewsCalloutModelItem = SitecoreCurrentContext.GetItem<RecentNewsCallout>(recentNewsCalloutItem.Id);
                recentNewsCalloutModelItem.RecentNews = recentNewsCalloutModelItem.RecentNews.Where(x => x.NewsArticleDate.ToString() != DateTime.MinValue.ToString())
                    .OrderByDescending(x => x.NewsArticleDate)
                        .ToList();
                return View(Constants.ViewPaths.RecentNews, recentNewsCalloutModelItem);
            }
            return null;
        }

        // *******************************************************************************************************************

        /// <summary>
        /// Return the latest events if there are no events configured...
        /// </summary>
        /// <returns></returns>
        private List<Events> GetDynamicUpcomingEvents()
        {
            var upcomingEvents = new List<Events>();
            var eventItems = SearchHelper.GetItems(Constants.SearchIndex.Content, Sitecore.Context.Language.ToString(),
                Constants.TemplateIds.Events, Constants.PageIds.EventsListing, String.Empty, null);
            if (eventItems!=null)
            {
                var filteredEvents =
                   eventItems.Where(
                       x =>
                            SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.EventStartDate, x,
                          false)).ToString() != DateTime.MinValue.ToString() &&
                               SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(
                                   SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.EventStartDate, x, false))
                                   .Date >= DateTime.Now.Date).OrderBy(y => SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.EventStartDate, y, false))).ToList();

                filteredEvents.ForEach(x => upcomingEvents.Add(new Events
                {
                    PageTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.PageTitle,x,false),
                    EventStartDate = SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(
                                   SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.EventStartDate, x, false)),
                                   Url = SitecoreHelper.ItemRenderMethods.GetItemUrl(x)
                    
                }));
                return upcomingEvents;
            }
            return upcomingEvents;
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
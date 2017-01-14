using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using symmons.com._Classes.Symmons.Global;
using Verndale.SharedSource.Helpers;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com.Areas.Symmons.Models.Global.Base;
using symmons.com.Areas.Symmons.Models.Pages;
using symmons.com.Areas.Symmons.Models.Pages.Global;
using Glass.Mapper.Sc;
using symmons.com.Areas.Symmons.Models.Global;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com.Areas.Symmons.Controllers.Global
{
    public class ListingController : SymmonsController
    {
        // *******************************************************************************************************************

        public JsonResult GetNewsListingData(int pageNum = 0)
        {
            return GetJsonData(pageNum);
        }

        // *******************************************************************************************************************

        public JsonResult GetEventsListingData(int pageNum = 0)
        {
            return GetJsonData(pageNum);
        }

        // *******************************************************************************************************************

        public JsonResult GetGenericListingData(int pageNum = 0)
        {
            return GetJsonData(pageNum);
        }

        // *******************************************************************************************************************

        private JsonResult GetJsonData(int pageNum)
        {
            var serialNo = 1;
            int pageSize;
            IEnumerable<ISymmonsBasePage> listingData;
            IEnumerable<ISymmonsBasePage> paginationList;

            if (Session[Constants.ConstantValues.SessionGenericListingData] != null)
            {
                listingData = (IEnumerable<ISymmonsBasePage>)Session[Constants.ConstantValues.SessionGenericListingData];
            }
            else
            {
                return null;
            }

            if (pageNum != 0)
            {
                pageSize = Constants.ConstantValues.GlobalPageSize;
                paginationList = listingData.Skip((pageNum - 1) * pageSize).Take(pageSize);
            }
            else
            {
                paginationList = listingData;
                pageSize = listingData.Count();
            }

            var genericListingData = new List<SymmonsListing.GenericListData>();

            foreach (var listingItem in paginationList)
            {
                if (listingItem is News)
                {
                    var newsItem = listingItem as News;

                    genericListingData.Add(new SymmonsListing.GenericListData
                    {
                        ListingId = serialNo++.ToString("D2"),
                        ListingTitle = newsItem.PageTitle,
                        ListingUrl = newsItem.Url,
                        ListingThumbImageUrl = newsItem.NewsListingImage != null ? newsItem.NewsListingImage.Src : string.Empty,
                        ListingImageAlt = newsItem.NewsListingImage != null ? newsItem.NewsListingImage.Alt : string.Empty,
                        ListingTeaserDesc = newsItem.Teaser,
                        ListingDate =
                            newsItem.NewsArticleDate.Equals(DateTime.MinValue)
                                ? string.Empty
                                : newsItem.NewsArticleDate.ToString("MMM dd, yyyy"),
                        ListingCTA = Translate.Text(Constants.Dictionary.ReadMore),
                        ListingCtaUrl = newsItem.Url
                    });
                }
                else if (listingItem is Events)
                {
                    var eventsItem = listingItem as Events;
                    genericListingData.Add(new SymmonsListing.GenericListData
                    {
                        ListingId = serialNo++.ToString("D2"),
                        ListingTitle = eventsItem.PageTitle,
                        ListingUrl = eventsItem.Url,
                        ListingThumbImageUrl =
                            eventsItem.EventsListingImage != null ? eventsItem.EventsListingImage.Src : string.Empty,
                        ListingImageAlt =
                            eventsItem.EventsListingImage != null ? eventsItem.EventsListingImage.Alt : string.Empty,
                        ListingTeaserDesc = eventsItem.Teaser,
                        ListingDate =
                            eventsItem.EventStartDate.Equals(DateTime.MinValue)
                                ? string.Empty
                                : eventsItem.EventStartDate.ToString("MMM dd, yyyy"),
                        ListingCTA = Translate.Text(Constants.Dictionary.ReadMore),
                        ListingCtaUrl = eventsItem.Url
                    });
                }
                else if (listingItem is SymmonsGeneric)
                {
                    var genericItem = listingItem as SymmonsGeneric;
                    genericListingData.Add(new SymmonsListing.GenericListData
                    {
                        ListingId = serialNo++.ToString("D2"),
                        ListingTitle = genericItem.PageTitle,
                        ListingSubTitle = genericItem.PageSubTitle,
                        ListingUrl = genericItem.Url,
                        ListingThumbImageUrl = genericItem.PageImage != null ? genericItem.PageImage.Src : string.Empty,
                        ListingImageAlt = genericItem.PageImage != null ? genericItem.PageImage.Alt : string.Empty,
                        ListingTeaserDesc = genericItem.Teaser,
                        ListingCTA = Translate.Text(Constants.Dictionary.ReadMore),
                        ListingCtaUrl = genericItem.Url
                    });
                }
            }

            var genericListing = new SymmonsListing.GenericListing
            {
                TotalRecordCount = listingData.Count(),
                TotalPagesCount = (int)Math.Ceiling(listingData.Count() / Convert.ToDecimal(pageSize)),
                GenericListingData = genericListingData
            };
            return Json(genericListing, JsonRequestBehavior.AllowGet);
        }

        // *******************************************************************************************************************

        public ActionResult GetListing()
        {
            if (ContextItem.ID.ToString() == Constants.PageIds.NewsListing)
            {
                var listingModel = SitecoreCurrentContext.GetCurrentItem<NewsListing>();
                var newsList = new List<News>();

                if (listingModel != null)
                {
                    var newsItems = SearchHelper.GetItems(Constants.SearchIndex.Content, Sitecore.Context.Language.ToString(), Constants.TemplateIds.News,
                        Constants.PageIds.NewsListing, String.Empty, null);

                    if (newsItems != null)
                    {
                        newsItems.ForEach(x => newsList.Add(new News
                        {
                            PageTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.PageTitle, x, false),
                            NewsListingImage = GetNewsListingImage(x),
                            Teaser = SymmonsHelper.GetTeaser(x),
                            Url = SymmonsHelper.GetUrl(x)
                        }));

                        Session[Constants.ConstantValues.SessionGenericListingData] =
                            newsList.Where(x =>
                            !String.IsNullOrWhiteSpace(x.PageTitle)
                            && !String.IsNullOrWhiteSpace(x.Teaser)).OrderByDescending(x => x.NewsArticleDate);
                    }

                    listingModel.Children = newsList;
                }
                return View(Constants.ViewPaths.NewsAndEventsListing, listingModel);
            }
            if (ContextItem.ID.ToString() == Constants.PageIds.EventsListing)
            {
                var listingModel = SitecoreCurrentContext.GetCurrentItem<EventsListing>();
                var eventList = new List<Events>();

                if (listingModel != null)
                {
                    var eventItems = SearchHelper.GetItems(Constants.SearchIndex.Content, Sitecore.Context.Language.ToString(), Constants.TemplateIds.Events,
                        Constants.PageIds.EventsListing, String.Empty, null);

                    if (eventItems != null)
                    {
                        eventItems.ForEach(x => eventList.Add(new Events
                        {
                            PageTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.PageTitle, x, false),
                            EventsListingImage = GetEventsListingImage(x),
                            Teaser = SymmonsHelper.GetTeaser(x),
                            Url = SymmonsHelper.GetUrl(x)
                        }));

                        Session[Constants.ConstantValues.SessionGenericListingData] =
                            eventList.Where(x =>
                            !String.IsNullOrWhiteSpace(x.PageTitle)
                            && !String.IsNullOrWhiteSpace(x.Teaser));
                    }

                    listingModel.Children = eventList;
                }
                return View(Constants.ViewPaths.NewsAndEventsListing, listingModel);
            }
            return null;
        }

        // *******************************************************************************************************************

        public ActionResult GetGenericListing()
        {
            var genericListingModel = (DatasourceItem == null ?
                null : DatasourceItem.GlassCast<SymmonsGenericListingContainer>());
            if (genericListingModel != null)
            {
                if (genericListingModel.ListingItems != null)
                {
                    Session[Constants.ConstantValues.SessionGenericListingData] =
                        genericListingModel.ListingItems.Where(x =>
                        !String.IsNullOrWhiteSpace(x.PageTitle)
                        && !String.IsNullOrWhiteSpace(x.Teaser));
                }
            }
            return View(Constants.ViewPaths.GenericListing, genericListingModel);
        }

        // *******************************************************************************************************************

        private Image GetNewsListingImage(Item news)
        {
            var newsModal = SitecoreCurrentContext.GetItem<News>(news.ID.ToString());
            if (newsModal != null)
            {
                if (SymmonsHelper.IsValidImage(newsModal.NewsListingImage))
                {
                    var newsListingImage = newsModal.NewsListingImage;
                    if (newsListingImage != null)
                    {
                        return newsListingImage;
                    }
                }
            }
            return null;
        }

        // *******************************************************************************************************************

        private Image GetEventsListingImage(Item events)
        {
            var eventsModal = SitecoreCurrentContext.GetItem<Events>(events.ID.ToString());
            if (eventsModal != null)
            {
                if (SymmonsHelper.IsValidImage(eventsModal.EventsListingImage))
                {
                    var eventsListingImage = eventsModal.EventsListingImage;
                    if (eventsListingImage != null)
                    {
                        return eventsListingImage;
                    }
                }
            }
            return null;
        }

        // *******************************************************************************************************************

        #region ApprovedProducts Listing
        public ActionResult GetApprovedProductsListing()
        {
            Session[Constants.ConstantValues.SessionApprovedProductsListingData] = null;

            var approvedProdListingModel = (DatasourceItem == null ?
                null : DatasourceItem.GlassCast<ApprovedProductsContainer>());
            if (approvedProdListingModel != null)
            {
                if (approvedProdListingModel.ListingItems != null && approvedProdListingModel.ListingItems.Any())
                {
                    approvedProdListingModel.ListingItems = approvedProdListingModel.ListingItems.Where(x =>
                        !String.IsNullOrWhiteSpace(x.PageTitle)
                        && !String.IsNullOrWhiteSpace(x.Teaser));

                    if (approvedProdListingModel.ListingItems != null && approvedProdListingModel.ListingItems.Any())
                    {
                        Session[Constants.ConstantValues.SessionApprovedProductsListingData] =
                            approvedProdListingModel.ListingItems;
                    }

                }
            }
            return View(Constants.ViewPaths.ApprovedProductsListing, approvedProdListingModel);
        }

        public JsonResult GetApprovedProductsListingData(int pageNum = 0)
        {
            var serialNo = 1;
            int pageSize;
            IEnumerable<ApprovedProducts> listingData;
            IEnumerable<ApprovedProducts> paginationList;

            if (Session[Constants.ConstantValues.SessionApprovedProductsListingData] != null)
            {
                listingData = (IEnumerable<ApprovedProducts>)Session[Constants.ConstantValues.SessionApprovedProductsListingData];
            }
            else
            {
                listingData = new List<ApprovedProducts>();
            }

            if (pageNum != 0)
            {
                pageSize = Constants.ConstantValues.GlobalPageSize;
                paginationList = listingData.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                paginationList = listingData;
                pageSize = listingData.Count();
            }

            var genericListingData = paginationList.Select(listingItem => new SymmonsListing.GenericListData
            {
                ListingId = serialNo++.ToString("D2"),
                ListingTitle = listingItem.PageTitle,
                ListingSubTitle = listingItem.PageSubTitle,
                ListingDate = listingItem.PageSubTitle,
                ListingUrl = listingItem.Url,
                ListingThumbImageUrl = listingItem.PageImage != null ? listingItem.PageImage.Src : string.Empty,
                ListingImageAlt = listingItem.PageImage != null ? listingItem.PageImage.Alt : string.Empty,
                ListingTeaserDesc = listingItem.Teaser,
                ListingCTA = Translate.Text(Constants.Dictionary.ReadMore),
                ListingCtaUrl = listingItem.Url
            }).ToList();

            var genericListing = new SymmonsListing.GenericListing
            {
                TotalRecordCount = listingData.Count(),
                TotalPagesCount = (int)Math.Ceiling(listingData.Count() / Convert.ToDecimal(pageSize)),
                GenericListingData = genericListingData
            };
            return Json(genericListing, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
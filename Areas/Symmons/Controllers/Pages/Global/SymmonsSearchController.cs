using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using symmons.com.Areas.Symmons.Models.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com._Classes.Symmons.Global;
using symmons.com._Classes.Symmons.Helpers;
using Verndale.SharedSource.Helpers;
using symmons.com.Areas.Symmons.Models.Modules.Global;
using Constants = symmons.com._Classes.Shared.Global.Constants;
using Convert = System.Convert;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Global
{
    public class SymmonsSearchController : SymmonsController
    {
        // *****************************************************************************************************************

        #region Hybrid Search

        public ActionResult GetHybridSearchResult(string keyword, bool isFromCallout = false)
        {
            Session[Constants.SessionConstants.IsFromCallout] = null;

            if (isFromCallout)
            {
                Session[Constants.SessionConstants.IsFromCallout] = true;
            }
            // Creates a TextInfo based on the "en-US" culture.
            var ti = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
            var hybridSearchModel = SitecoreCurrentContext.GetCurrentItem<HybridSearch>();
            if (!string.IsNullOrEmpty(keyword.Trim()))
            {
                keyword = keyword.Trim();

                var productItems = GetProductItems(keyword);
                var contentItems = GetContentItems(keyword, isFromCallout);
                var productList = productItems.Take(4).Select(product => new ProductsHelper.Products
                {
                    ListingProductId = product.ID.ToString(),
                    ListingProductTitle =
                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductName,
                            product, false),
                    ListingIsProductNew = ProductsHelper.IsProductNew(product),
                    ListingProductTeaser = ProductsHelper.GetProductTeaser(product),
                    ListingProductImageUrl = ProductsHelper.GetProductImageSrc(product),
                    ListingProductPrice =  ti.ToTitleCase(ProductsHelper.GetProductMinPrice(product).ToLower().Trim()),
                    ListingProductUrl =
                        SymmonsHelper.GetProductUrl(
                            SitecoreCurrentContext.GetItem<ProductDetails>(product.ID.ToString()),
                            System.Web.HttpContext.Current.Request.Url),
                    ListingProductModelNumber = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductModelNumber,
                    product, false)
                }).ToList();

                var contentList = contentItems.Take(2).Select(content => new Content
                {
                    ContentId = content.ID.ToString(),
                    ContentTitle = SymmonsHelper.IsMediaItem(content) ? SymmonsHelper.GetMediaTitle(content) : SymmonsHelper.GetNavigationTitle(content),
                    ContentUrl = SymmonsHelper.IsMediaItem(content) ? SymmonsHelper.GetMediaUrl(content) : SymmonsHelper.GetUrl(content),
                    ContentShowUrl = SymmonsHelper.IsMediaItem(content) ? string.Empty : SymmonsHelper.GetUrl(content),
                    ContentTeaser = SymmonsHelper.IsMediaItem(content) ? SymmonsHelper.GetMediaDescription(content) : SymmonsHelper.GetTeaser(content)
                }).ToList();

                hybridSearchModel.SearchParam = keyword;
                hybridSearchModel.ProductResultCount = productItems.Count();
                hybridSearchModel.ContentResultCount = contentItems.Count();
                hybridSearchModel.ProductList = productList;
                hybridSearchModel.ContentList = contentList;
            }
            return View(Constants.ViewPaths.HybridSearch, hybridSearchModel);
        }

        #endregion

        // *****************************************************************************************************************

        #region Content Search

        public ActionResult GetContentSearchPage(string keyword = "", int productResultCount = 0, bool isFromCallout = false)
        {
            Session[Constants.SessionConstants.FromSearchBox] = null;
            Session[Constants.SessionConstants.ContentSearch] = null;
            Session[Constants.SessionConstants.IsFromCallout] = null;

            if (isFromCallout)
            {
                Session[Constants.SessionConstants.IsFromCallout] = true;
            }

            keyword = keyword.Trim();
            var contentSearchModel = SitecoreCurrentContext.GetCurrentItem<ContentSearchModel>();

            var contentItems = GetContentItems(keyword, isFromCallout);
            Session[Constants.SessionConstants.ProductResultCount] = productResultCount;

            contentSearchModel.SearchParam = keyword;
            contentSearchModel.ProductResultCount = productResultCount;
            contentSearchModel.ContentSearchList = contentItems;

            return View(Constants.ViewPaths.ContentSearch, contentSearchModel);
        }

        // *****************************************************************************************************************

        public JsonResult GetContentSearchResult(string keyword = "", int pageNum = 0, bool fromSearch = false)
        {
            var serialNo = 1;
            var productResultCount = 0;
            int pageSize;
            IEnumerable<Item> listingData;
            IEnumerable<Item> paginationList;

            keyword = keyword.Trim();

            if (!String.IsNullOrEmpty(keyword))
            {
                bool isFromCallout = Session[Constants.SessionConstants.IsFromCallout] != null;
                GetContentItems(keyword, isFromCallout);
            }

            if (Session[Constants.SessionConstants.ContentSearch] != null)
            {
                listingData = (IEnumerable<Item>)Session[Constants.SessionConstants.ContentSearch];
            }
            else
            {
                listingData = new List<Item>();
            }

            if (Session[Constants.SessionConstants.FromSearchBox] == null)
            {
                if (fromSearch)
                {
                    productResultCount = 0;
                    Session[Constants.SessionConstants.FromSearchBox] = true;
                }
                else if (Session[Constants.SessionConstants.ProductResultCount] != null)
                {
                    productResultCount = Convert.ToInt32(Session[Constants.SessionConstants.ProductResultCount]);
                }
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

            if (pageSize == 0)
            {
                pageSize = 1;
            }

            var searchListingData = paginationList.Select(listingItem => new SymmonsListing.SearchListingData
            {
                ListingId = serialNo++.ToString("D2"),
                ListingTitle = SymmonsHelper.IsMediaItem(listingItem) ? SymmonsHelper.GetMediaTitle(listingItem) : SymmonsHelper.GetNavigationTitle(listingItem),
                ListingTeaserDesc = SymmonsHelper.IsMediaItem(listingItem) ? SymmonsHelper.GetMediaDescription(listingItem) : SymmonsHelper.GetTeaser(listingItem),
                ListingCTA = SymmonsHelper.IsMediaItem(listingItem) ? SymmonsHelper.GetMediaUrl(listingItem) : SymmonsHelper.GetUrl(listingItem),
                ListingShowUrl = SymmonsHelper.IsMediaItem(listingItem) ? string.Empty : SymmonsHelper.GetUrl(listingItem)
            }).ToList();

            var searchListing = new SymmonsListing.SearchListing
            {
                TotalRecordCount = listingData.Count().ToString(),
                TotalProductCount = productResultCount.ToString(),
                TotalPagesCount = ((int)Math.Ceiling(listingData.Count() / Convert.ToDecimal(pageSize))).ToString(),
                SearchListingData = searchListingData
            };
            return Json(searchListing, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // *****************************************************************************************************************

        #region Common

        public ActionResult GetSearchResult(string globalSearch, bool isFromCallout = false)
        {
            Session[Constants.SessionConstants.IsFromCallout] = null;

            var searchPageUrl = string.Empty;

            if (isFromCallout)
            {
                Session[Constants.SessionConstants.IsFromCallout] = true;
            }

            globalSearch = globalSearch.Trim();

            var productItems = GetProductItems(globalSearch);
            var contentItems = GetContentItems(globalSearch, isFromCallout);

            if ((productItems.Any() && contentItems.Any()) || (!productItems.Any() && !contentItems.Any()))
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.HybridSearchPage);
            }
            else if (productItems.Any())
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.ProductBrowse);
            }
            else if (contentItems.Any())
            {
                searchPageUrl = SymmonsHelper.GetPageUrl(Constants.PageIds.ContentSearchPage);
            }

            searchPageUrl += "?" + Constants.QueryString.Keyword + "=" + globalSearch;

            if (isFromCallout)
            {
                searchPageUrl = searchPageUrl + "&" + Constants.QueryString.IsFromCallout + "=true";
            }

            return Redirect(searchPageUrl);

        }

        // *****************************************************************************************************************

        private static List<Item> GetProductItems(string keyword)
        {
            var refinements = new List<Refinement>
            {
                new Refinement {FieldName = Constants.FieldNames.ExcludeFromSearch, Value = "0"}
            };

            var productItems = SearchHelper.GetItems(Constants.SearchIndex.Products,
                Sitecore.Context.Language.ToString(), Constants.TemplateIds.ProductDetailsTemplateId,
                Constants.FolderIds.ProductsRepository, keyword, refinements, new List<PrioritizedField>(), true,
                string.Empty, false, null, null, true) ?? new List<Item>();
            var regionalPrice = LocationsHelper.IsCaSite() ? Constants.FieldNames.CanPrice : Constants.FieldNames.PriceMin;
            var nonOnAppProducts = productItems.Where(x => SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                regionalPrice, x, false).ToLower().Trim() != Constants.ConstantValues.ProductOnAppPrice).ToList();
             var onAppProducts = productItems.Where(x => SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                regionalPrice, x, false).ToLower().Trim() == Constants.ConstantValues.ProductOnAppPrice).ToList();

            var sortedProductItems = new List<Item>();
            if (onAppProducts.Any())
            {
                sortedProductItems.AddRange(onAppProducts);
            }
            if (nonOnAppProducts.Any())
            {
                sortedProductItems.AddRange(nonOnAppProducts.Where(
                            x =>
                                !String.IsNullOrEmpty(
                                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                        regionalPrice, x, false)))
                            .OrderByDescending(x => Convert.ToDouble(x.Fields[regionalPrice].Value)));
            }
            return sortedProductItems;
        }

        // *****************************************************************************************************************

        public static List<PrioritizedField> GetPrioritizedSearchFields()
        {
            var refinements = new List<PrioritizedField>
           {
               new PrioritizedField
               {
                   FieldName =
                       "Name",
                   BoostValue = 2000,
                   IncludeNonPluralSearch = true
               },
               new PrioritizedField
               {
                   FieldName =
                       "Page Title",
                   BoostValue = 1000,
                   IncludeNonPluralSearch = true
               }
           };
            return refinements;
        }

        private List<Item> GetContentItems(string keyword, bool isFromCallout)
        {
            var lstItems = new List<Item>();

            if (!isFromCallout)
            {
                var lstContentItems = SearchHelper.GetItems(Constants.SearchIndex.Content, Sitecore.Context.Language.ToString(),
                    string.Empty, Constants.PageIds.Home, keyword, null, GetPrioritizedSearchFields());
                if (lstContentItems != null)
                {
                    lstItems.AddRange(lstContentItems);
                }
            }

            var lstMediaItems = SearchHelper.GetItems(Constants.SearchIndex.Media, Sitecore.Context.Language.ToString(),
                Constants.TemplateIds.MediaItems,Constants.PageIds.MediaSymmons, keyword, null);

            if (lstMediaItems != null)
            {
                lstItems.AddRange(lstMediaItems);
            }

            Session[Constants.SessionConstants.ContentSearch] = lstItems;
            return lstItems;
        }

        #endregion

        // *****************************************************************************************************************
        // *****************************************************************************************************************
    }
}
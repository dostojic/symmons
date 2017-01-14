using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy;
using symmons.com.Areas.Symmons.Models.Pages;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Helpers;
using Verndale.SharedSource.Helpers;
using symmons.com.Areas.Symmons.Models.Global;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Global
{
    public class WhereToBuyController : SymmonsController
    {
        // Start :GetWhereToBuyListing  **********************************************************************************************
        // ***************************************************************************************************************************
        // Redirect to "Where to buy" Page...
        public ActionResult GetWhereToBuyListing()
        {
            var storeTypesList = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.ItemIds.WhereToBuyStoreType);
            var storeTypeList = storeTypesList.Axes.GetDescendants();
            var whereToBuyModel = SitecoreCurrentContext.GetCurrentItem<WheretoBuy>();

            var whereToBuyStoreTypes = storeTypeList.Select(item => SitecoreCurrentContext.GetItem<WhereToBuyStoreTypes>(item.ID.ToString())).ToList();
            whereToBuyModel.WhereToBuyStoreTypes = whereToBuyStoreTypes;
            return View(Constants.ViewPaths.WhereToBuy, whereToBuyModel);
        }

        // End :GetWhereToBuyListing  ************************************************************************************************
        // ***************************************************************************************************************************

        // Start : GetStoresByStoreTypes *********************************************************************************************
        // ***************************************************************************************************************************
        /// <summary>
        /// To fetch one store from each store type...
        /// </summary>
        /// <param name="location"></param>
        /// <param name="latitude">latitude to check for...</param>
        /// <param name="longitude">longitude to check for...</param>
        /// <param name="searchOptions"></param>
        /// <returns>Returns one store from each store type...</returns>
        public JsonResult GetStoresByStoreTypes(string location, string latitude, string longitude, string searchOptions)
        {
            Session["location"] = location;

            var storesByStoreType = StoresHelper.GetNearestStoresTypeStores(latitude, longitude, searchOptions, location);
            return Json(storesByStoreType, JsonRequestBehavior.AllowGet);
        }

        // End:GetStoresByStoreTypes  ************************************************************************************************
        // ***************************************************************************************************************************

        // Start : GetAllStoresByStoreTypes ******************************************************************************************
        // ***************************************************************************************************************************
        /// <summary>
        /// To fetch stores based on store type...
        /// </summary>
        /// <param name="location">search string to look for...</param>
        /// <param name="latitude">latitude to check for...</param>
        /// <param name="longitude">longitude to check for...</param>
        /// <param name="store"></param>
        /// <param name="pageNum"></param>
        /// <returns>Returns stores based on particular store type...</returns>
        public JsonResult GetAllStoresByStoreTypes(int pageNum, string store, string latitude, string longitude)
        {

            var location = (string)Session["location"];
            var allStoresByStoreType = StoresHelper.GetAllNearestStoreTypesStores(latitude, longitude, store, location);

            const int pageSize = 6;
            if (pageNum != 0)
            {
                allStoresByStoreType.TotalPagesCount =
                    (int)Math.Ceiling(allStoresByStoreType.MoreStores.Count() / Convert.ToDecimal(pageSize));
                allStoresByStoreType.MoreStores = allStoresByStoreType.MoreStores.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                allStoresByStoreType.TotalPagesCount = 0;
            }
            var x = Json(allStoresByStoreType, JsonRequestBehavior.AllowGet);
            return x;
        }

        // End:GetAllStoresByStoreTypes  *********************************************************************************************
        // ***************************************************************************************************************************

        // Start :GetSalesRepresentatives  ********************************************************************************************
        // ***************************************************************************************************************************
        //  To get the sales representative items based on state Id...
        public JsonResult GetSalesRepresentatives(string statecode, string state = "", int pageNum = 0)
        {
            var serialNo = 1;
            int pageSize;
            var defaultStateCodeId = String.Empty;
            IEnumerable<SalesRepresentative> paginationList;
            if (string.IsNullOrWhiteSpace(state))
            {
                if (!String.IsNullOrEmpty(statecode))
                {
                    var allStateItems = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(),
                    Constants.TemplateIds.State, Constants.FolderIds.StoresRepository, String.Empty, null);
                    state = allStateItems.Where(x => x.DisplayName == statecode).Select(x => x.ID.ToString()).FirstOrDefault();
                    defaultStateCodeId = state;
                }
                else
                {
                    state = Constants.FolderIds.StoresRepository;
                }
            }
            else
            {
                if (state == "0" || state == String.Empty)
                {
                    state = Constants.FolderIds.StoresRepository;
                    defaultStateCodeId = String.Empty;
                }
                else
                {
                    defaultStateCodeId = state;
                }
            }
            List<Item> salesRepItems;
            // Get all representative Items...
            if (LocationsHelper.IsCaSite())
            {
                // Get representatives from canada region...
                salesRepItems = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(),
                Constants.TemplateIds.SalesRepresntative, Constants.PageIds.CanadaStoresRepository, String.Empty, null);
            }
            else
            {
                // Get representatives from other region...
                var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                salesRepItems = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(),
                Constants.TemplateIds.SalesRepresntative, state, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
            }
            
            var salesRepresentatives = new List<SalesRepresentative>();

            if (salesRepItems != null)
            {
                salesRepresentatives.AddRange(
                    salesRepItems.Select(
                    item => SitecoreCurrentContext.GetItem<SalesRepresentative>(item.ID.ToString())
                    ).OrderBy(x => x.LastName));
            }

            if (pageNum != 0)
            {
                pageSize = Constants.ConstantValues.GlobalPageSize;
                paginationList = salesRepresentatives.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                paginationList = salesRepresentatives;
                pageSize = salesRepresentatives.Count();
            }
            var salesRepListingData = paginationList.Select(listingItem => new SymmonsListing.SalesRepresentativeListingData
            {
                ListingId = serialNo++.ToString("D2"),
                ListingFirstName = listingItem.FirstName,
                ListingLastName = listingItem.LastName,
                ListingAddress = StoresHelper.GetStoreRepresentativeAddress(listingItem),
                ListingPhone = listingItem.Phone,
                ListingEmail = listingItem.Email,
            }).ToList();

            var salesRepListing = new SymmonsListing.SalesRepListing
            {
                TotalRecordCount = salesRepresentatives.Count(),
                TotalPagesCount = (int)Math.Ceiling(salesRepresentatives.Count() / Convert.ToDecimal(pageSize)),
                SelectedStateId = defaultStateCodeId,
                SalesRepListingData = salesRepListingData
            };
            return Json(salesRepListing, JsonRequestBehavior.AllowGet);
        }

        // End :GetSalesRepresentatives  *********************************************************************************************
        // ***************************************************************************************************************************
    }
}
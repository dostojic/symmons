using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Shared.Utilities;
using symmons.com._Classes.Symmons.Global;
using Verndale.SharedSource.Helpers;
using Store = symmons.com._Classes.Symmons.Global.Store;

namespace symmons.com._Classes.Symmons.Helpers
{
    public class StoresHelper
    {
        public static List<Store> GetAllStores()
        {
            var retVal = new List<Store>();

            var showRoomStoreItems = GetAllShowRoomStores();
            var wholeSaleStoreItems = GetAllWholeSaleStores();
            var retailStoreItems = GetAllRetailStores();
            var partsStoreItems = GetAllPartsStores();

            foreach (var item in showRoomStoreItems)
            {
                retVal.Add(GetStore(item, "Show Room"));
            }

            foreach (var item in wholeSaleStoreItems)
            {
                retVal.Add(GetStore(item, "Wholesale"));
            }

            foreach (var item in retailStoreItems)
            {
                retVal.Add(GetStore(item, "Retail"));
            }

            foreach (var item in partsStoreItems)
            {
                retVal.Add(GetStore(item, "Parts"));
            }

            return retVal;
        }

        public static List<Item> GetAllShowRoomStores()
        {
            List<Item> retVal = new List<Item>();

            retVal = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                        Constants.PageIds.CanadaStoresRepository, String.Empty, null);
            
            var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
            retVal.AddRange(SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList());

            return retVal;            
        }

        public static List<Item> GetAllWholeSaleStores()
        {
            List<Item> retVal = new List<Item>();

            retVal = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                        Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                  
            var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
            retVal.AddRange(SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList());

            return retVal;
        }

        public static List<Item> GetAllRetailStores()
        {
            List<Item> retVal = new List<Item>();

            retVal = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                    Constants.PageIds.CanadaStoresRepository, String.Empty, null);
            
            var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
            retVal.AddRange(SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList());

            return retVal;
        }

        public static List<Item> GetAllPartsStores()
        {
            List<Item> retVal = new List<Item>();

            retVal = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                        Constants.PageIds.CanadaStoresRepository, String.Empty, null);

            var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
            retVal.AddRange(SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                    Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList());

            return retVal;
        }


        // Start :GetNearestStoresTypeStores  *****************************************************************************************************
        // ****************************************************************************************************************************************
        /// <summary>
        /// To get the list of nearest stores based on latitude and longitude passed.
        /// </summary>
        /// <param name="latitude">latitude of the search location</param>
        /// <param name="longitude">longitude of the search location</param>
        /// <param name="storeTypeIds">templateids of the store Types</param>
        /// <param name="locationNear"></param>
        /// <returns>list of nearest stores</returns>
        public static StoresContainerMaster GetNearestStoresTypeStores(string latitude, string longitude, string storeTypeIds, string locationNear)
        {
            if (String.IsNullOrEmpty(latitude) || String.IsNullOrEmpty(longitude))
            {
                return null;
            }
            var i = 1;
            var templateIdList = storeTypeIds.Split(',');
            var showRoomStoreItems = new List<Item>();
            var wholeSaleStoreItems = new List<Item>();
            var retailStoreItems = new List<Item>();
            var partsStoreItems = new List<Item>();

            #region For Showroom stores

            if (templateIdList.Contains(Constants.TemplateIds.ShowroomStores))
            {
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("ShowroomStoresCAN"))
                    {
                        CacheUtility.Get("ShowroomStoresCAN", out showRoomStoreItems);
                    }
                    else
                    {
                        showRoomStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                    Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                    Constants.PageIds.CanadaStoresRepository, String.Empty, null);

                        if (showRoomStoreItems != null)
                        {
                            CacheUtility.Add(showRoomStoreItems, "ShowroomStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("ShowroomStores"))
                    {
                        CacheUtility.Get("ShowroomStores", out showRoomStoreItems);
                    }

                    var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                    showRoomStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();

                    if (showRoomStoreItems.Any())
                    {
                        CacheUtility.Add(showRoomStoreItems, "ShowroomStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                    }
                }
            }

            #endregion

            #region For Wholesale stores

            if (templateIdList.Contains(Constants.TemplateIds.WholesaleStores))
            {
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("WholesaleStoresCAN"))
                    {
                        CacheUtility.Get("WholesaleStoresCAN", out wholeSaleStoreItems);
                    }
                    else
                    {
                        wholeSaleStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (wholeSaleStoreItems != null)
                        {
                            CacheUtility.Add(wholeSaleStoreItems, "WholesaleStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("WholesaleStores"))
                    {
                        CacheUtility.Get("WholesaleStores", out wholeSaleStoreItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        wholeSaleStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                       Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                       Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (wholeSaleStoreItems.Any())
                        {
                            CacheUtility.Add(wholeSaleStoreItems, "WholesaleStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }

            #endregion

            #region For Retail stores

            if (templateIdList.Contains(Constants.TemplateIds.RetailStores))
            {
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("RetailStoresCAN"))
                    {
                        CacheUtility.Get("RetailStoresCAN", out retailStoreItems);
                    }
                    else
                    {
                        retailStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);

                        if (retailStoreItems.Any())
                        {
                            CacheUtility.Add(retailStoreItems, "RetailStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("RetailStores"))
                    {
                        CacheUtility.Get("RetailStores", out retailStoreItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        retailStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                        Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (retailStoreItems.Any())
                        {
                            CacheUtility.Add(retailStoreItems, "RetailStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }

            #endregion

            #region Parts Store region

            if (templateIdList.Contains(Constants.TemplateIds.PartsStores))
            {
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("PartStoresCAN"))
                    {
                        CacheUtility.Get("PartStoresCAN", out partsStoreItems);
                    }
                    else
                    {
                        partsStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (partsStoreItems.Any())
                        {
                            CacheUtility.Add(partsStoreItems, "PartStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("PartStores"))
                    {
                        CacheUtility.Get("PartStores", out partsStoreItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        partsStoreItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                        Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (partsStoreItems.Any())
                        {
                            CacheUtility.Add(partsStoreItems, "PartStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }

            #endregion

            var storesList = new List<StoresContainer>();

            if (showRoomStoreItems != null && showRoomStoreItems.Any())
            {
                var filterdShowroomStores = GetFilteredStores(showRoomStoreItems, latitude, longitude);
                if (filterdShowroomStores != null)
                {
                    var highlightedShowroomStore = filterdShowroomStores.FirstOrDefault();
                    if (highlightedShowroomStore != null)
                    {
                        highlightedShowroomStore.Store.StoreMapPin = "/_Images/contentManaged/pins/icon-mapmarker_" +
                                                                  i++ + ".png";
                        storesList.Add(new StoresContainer
                        {
                            StoreTypeId = Constants.TemplateIds.ShowroomStores,
                            StoreTypeName = GetStoreTypeName(Constants.PageIds.ShowroomStore),
                            Store = highlightedShowroomStore.Store
                        });
                    }
                }
            }

            if (wholeSaleStoreItems != null && wholeSaleStoreItems.Any())
            {
                var filterdWholesaleStore = GetFilteredStores(wholeSaleStoreItems, latitude, longitude);
                if (filterdWholesaleStore != null)
                {
                    var highlightedShowroomStore = filterdWholesaleStore.FirstOrDefault();
                    if (highlightedShowroomStore != null)
                    {
                        highlightedShowroomStore.Store.StoreMapPin = "/_Images/contentManaged/pins/icon-mapmarker_" +
                                                                  i++ + ".png";
                        storesList.Add(new StoresContainer
                        {
                            StoreTypeId = Constants.TemplateIds.WholesaleStores,
                            StoreTypeName = GetStoreTypeName(Constants.PageIds.WholesaleStore),
                            Store = highlightedShowroomStore.Store
                        });
                    }
                }
            }

            if (retailStoreItems != null && retailStoreItems.Any())
            {
                var filterdRetailStore = GetFilteredStores(retailStoreItems, latitude, longitude);
                if (filterdRetailStore != null)
                {
                    var highlightedShowroomStore = filterdRetailStore.FirstOrDefault();
                    if (highlightedShowroomStore != null)
                    {
                        highlightedShowroomStore.Store.StoreMapPin = "/_Images/contentManaged/pins/icon-mapmarker_" +
                                                               i++ + ".png";
                        storesList.Add(new StoresContainer
                        {
                            StoreTypeId = Constants.TemplateIds.RetailStores,
                            StoreTypeName = GetStoreTypeName(Constants.PageIds.RetailStore),
                            Store = highlightedShowroomStore.Store
                        });
                    }
                }
            }

            if (partsStoreItems != null && partsStoreItems.Any())
            {
                var filterdPartsStore = GetFilteredStores(partsStoreItems, latitude, longitude);
                if (filterdPartsStore != null)
                {
                    var highlightedShowroomStore = filterdPartsStore.FirstOrDefault();
                    if (highlightedShowroomStore != null)
                    {
                        highlightedShowroomStore.Store.StoreMapPin = "/_Images/contentManaged/pins/icon-mapmarker_" +
                                                              i + ".png";
                        storesList.Add(new StoresContainer
                        {
                            StoreTypeId = Constants.TemplateIds.PartsStores,
                            StoreTypeName = GetStoreTypeName(Constants.PageIds.PartsDistributorsStore),
                            Store = highlightedShowroomStore.Store
                        });
                    }
                }
            }

            var storesContainerMaster = new StoresContainerMaster
            {
                TotalRecordCount = storesList.Count(),
                LatitudeValues = latitude,
                LongitudeValues = longitude,
                LocationNearTitle = Translate.Text(Constants.Dictionary.FindLocationsNearStep2),
                LocationNear = locationNear,
                StoresListingData = storesList
            };

            return storesContainerMaster;
        }

        // End :GetNearestStoresTypeStores  *******************************************************************************************************
        // ****************************************************************************************************************************************


        // Start :GetAllNearestStoreTypesStores  **************************************************************************************************
        // ****************************************************************************************************************************************

        /// <summary>
        /// To get all the stores based on store type...
        /// </summary>
        /// <param name="latitude">latitude of the search location</param>
        /// <param name="longitude">longitude of the search location</param>
        /// <param name="storeTypeIds">Storetype to search for...</param>
        /// <param name="locationNear"></param>
        /// <returns>Returns list of all stores of particular store type...</returns>
        public static StoreTypeStores GetAllNearestStoreTypesStores(string latitude, string longitude, string storeTypeIds, string locationNear)
        {
            //var storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(), storeTypeIds,
            //    Constants.FolderIds.StoresRepository, String.Empty, null);
            string storeTypeId;
            List<Item> storeItems;

            if (storeTypeIds.Equals(Constants.TemplateIds.ShowroomStores))
            {
                storeTypeId = Constants.PageIds.ShowroomStore;
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("ShowroomStoresCAN"))
                    {
                        CacheUtility.Get("ShowroomStoresCAN", out storeItems);
                    }
                    else
                    {
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "ShowroomStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("ShowroomStores"))
                    {
                        CacheUtility.Get("ShowroomStores", out storeItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                        Sitecore.Context.Language.ToString(), Constants.TemplateIds.ShowroomStores,
                        Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "ShowroomStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }

            else if (storeTypeIds.Equals(Constants.TemplateIds.RetailStores))
            {
                storeTypeId = Constants.PageIds.RetailStore;
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("RetailStoresCAN"))
                    {
                        CacheUtility.Get("RetailStoresCAN", out storeItems);
                    }
                    else
                    {
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                       Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                       Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (storeItems != null)
                        {
                            CacheUtility.Add(storeItems, "RetailStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("RetailStores"))
                    {
                        CacheUtility.Get("RetailStores", out storeItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                       Sitecore.Context.Language.ToString(), Constants.TemplateIds.RetailStores,
                       Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "RetailStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }
            else if (storeTypeIds.Equals(Constants.TemplateIds.WholesaleStores))
            {
                storeTypeId = Constants.PageIds.WholesaleStore;

                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("WholesaleStoresCAN"))
                    {
                        CacheUtility.Get("WholesaleStoresCAN", out storeItems);
                    }
                    else
                    {
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "WholesaleStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("WholesaleStores"))
                    {
                        CacheUtility.Get("WholesaleStores", out storeItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                       Sitecore.Context.Language.ToString(), Constants.TemplateIds.WholesaleStores,
                       Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "WholesaleStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }

            }
            else
            {
                storeTypeId = Constants.PageIds.PartsDistributorsStore;
                if (LocationsHelper.IsCaSite())
                {
                    if (CacheUtility.Exists("PartStoresCAN"))
                    {
                        CacheUtility.Get("PartStoresCAN", out storeItems);
                    }
                    else
                    {
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                            Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                            Constants.PageIds.CanadaStoresRepository, String.Empty, null);
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "PartStoresCAN", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
                else
                {
                    if (CacheUtility.Exists("PartStores"))
                    {
                        CacheUtility.Get("PartStores", out storeItems);
                    }
                    else
                    {
                        var canadaStoreLocation = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.CanadaStoresRepository);
                        storeItems = SearchHelper.GetItems(Constants.SearchIndex.Stores,
                       Sitecore.Context.Language.ToString(), Constants.TemplateIds.PartsStores,
                       Constants.FolderIds.StoresRepository, String.Empty, null).Where(x => !x.Axes.IsDescendantOf(canadaStoreLocation)).ToList();
                        if (storeItems.Any())
                        {
                            CacheUtility.Add(storeItems, "PartStores", Convert.ToInt32(Constants.ConstantValues.CacheStoresTime));
                        }
                    }
                }
            }

            if (storeItems != null && storeItems.Any())
            {
                var filteredAllStoreItems = GetFilteredStores(storeItems, latitude, longitude).Take(50);
                var filteredStoreItems = filteredAllStoreItems.Select(x => x.Store).OrderBy(x => x.DistanceInMiles).ToList();

                var storeTypeStores = new StoreTypeStores
                {
                    TotalRecordCount = filteredStoreItems.Count(),
                    LatitudeValues = latitude,
                    LongitudeValues = longitude,
                    LocationNearTitle = Translate.Text(Constants.Dictionary.FindLocationsNearStep3),
                    LocationNear = locationNear,
                    StoreTypeId = storeTypeIds,
                    StoreTypeName = GetStoreTypeName(storeTypeId),
                    MoreStores = filteredStoreItems
                };

                return storeTypeStores;
            }
            return null;
        }

        // End :GetAllNearestStoreTypesStores  **************************************************************************************************
        // **************************************************************************************************************************************


        // Start :GetFilteredStores  ************************************************************************************************************
        // **************************************************************************************************************************************

        /// <summary>
        /// To get the list of stores with details required to display...
        /// </summary>
        /// <param name="stores">list of stores to look for...</param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>List of all stores with name,distance,address and other details...</returns>
        public static List<StoresContainer> GetFilteredStores(List<Item> stores, string latitude, string longitude)
        {
            var storesList = new List<StoresContainer>();
            var i = 1;
            if (stores != null && stores.Any())
            {
                foreach (var storeItem in stores)
                {
                    double locLatitudeDouble;
                    double locLongitudeDouble;

                    var locationLatitude = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreLatitude,
                                storeItem, false);
                    var locationLongitude = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreLongitude,
                        storeItem, false);

                    if (double.TryParse(locationLatitude, out locLatitudeDouble) && double.TryParse(locationLongitude, out locLongitudeDouble))
                    {
                        double distance = GlobalHelper.Distance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), locLatitudeDouble, locLongitudeDouble, 'M');
                        if (distance <= Convert.ToDouble(Constants.ConstantValues.StoreRadius))
                        {
                            storesList.Add(new StoresContainer
                            {
                                Store = new Store
                                {
                                    StoreName =
                                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                            Constants.FieldNames.StoreName, storeItem, false),
                                    IsSymmonsPreferred =
                                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                            Constants.FieldNames.IsSymmonsPreferred, storeItem, false) == "1" ? Translate.Text(Constants.Dictionary.SymmonsPreferred) : string.Empty,
                                    IsSymmonsPreferredDescription = Translate.Text(Constants.Dictionary.SymmonsPreferredDescription),
                                    Address = GetStoreAddress(storeItem),
                                    Distance = distance.ToString("N") + " Miles",
                                    DistanceInMiles = distance,
                                    Directions = GetStoreDirection(storeItem),
                                    PhoneNo =
                                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                            Constants.FieldNames.StorePhone, storeItem, false),
                                    Latitude = locationLatitude,
                                    Longitude = locationLongitude,
                                    DirectionsTitle = Translate.Text(Constants.Dictionary.DirectionsTitle),
                                    MoreLikeThis = Translate.Text(Constants.Dictionary.MoreLike),
                                    MoreLocationsLikeThis = Translate.Text(Constants.Dictionary.MoreLocation),
                                }
                            });
                        }
                    }
                }

                var storeListing = storesList.OrderBy(x => x.Store.DistanceInMiles).ToList();
                foreach (var store in storeListing)
                {
                    store.Store.StoreMapPin = "/_Images/contentManaged/pins/icon-mapmarker_" + i++ + ".png";
                }
                return storeListing;
            }
            return null;
        }


        // End :GetFilteredStores  **************************************************************************************************************
        // **************************************************************************************************************************************

        // Start :GetStoreDirection  ************************************************************************************************************
        // **************************************************************************************************************************************

        /// <summary>
        /// To get store direction...
        /// </summary>
        /// <param name="storeItem">Store Item...</param>
        /// <returns>Returns Store direction...</returns>

        private static string GetStoreDirection(Item storeItem)
        {
            string directionsPageUrl = Translate.Text(Constants.Dictionary.DirectionsLink);
            var address1 =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine1,
                    storeItem, false).Trim();
            var address2 =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine2,
                    storeItem, false).Trim();
            var city =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreCity,
                    storeItem, false).Trim();
            var zipCode =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreZip,
                    storeItem, false).Trim();
            var url = directionsPageUrl;
            if (!String.IsNullOrEmpty(address1))
            {
                url = url + address1;
            }

            if (!String.IsNullOrEmpty(address2))
            {
                url = url + ",+" + SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine2, storeItem, false);
            }
            if (!String.IsNullOrEmpty(city))
            {
                url = url + ",+" + SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreCity, storeItem, false);
            }
            if (!String.IsNullOrEmpty(storeItem.Parent.Parent.DisplayName))
            {
                url = url + ",+" + storeItem.Parent.Parent.DisplayName;
            }

            if (!String.IsNullOrEmpty(zipCode))
            {
                url = url + ",+" + SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreZip,
                    storeItem, false);
            }

            return url;
        }

        // End :GetStoreDirection  **************************************************************************************************************
        // **************************************************************************************************************************************


        // Start :GetStoreAddress  **************************************************************************************************************
        // **************************************************************************************************************************************
        /// <summary>
        /// To get address (store/representative)
        /// </summary>
        /// <param name="storeItem">Store Item</param>
        /// <param name="htmlOutput">true if want to get representative address...</param>
        /// <returns>Return address (store/representative)...</returns>
        public static string GetStoreAddress(Item storeItem, bool htmlOutput = false)
        {
            var storeAddress = new StringBuilder();
            var address1 =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine1,
                    storeItem, false).Trim();
            var address2 =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine2,
                    storeItem, false).Trim();
            var city =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreCity,
                    storeItem, false).Trim();
            var state =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreState,
                    storeItem, false).Trim();
            var zipCode =
                SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreZip,
                    storeItem, false).Trim();

            if (!string.IsNullOrEmpty(address1))
            {
                storeAddress.Append(address1);
            }

            if (!string.IsNullOrEmpty(address2))
            {
                if (!string.IsNullOrEmpty(storeAddress.ToString()))
                {
                    storeAddress.Append(", ");
                }
                storeAddress.Append(address2);
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (!string.IsNullOrEmpty(storeAddress.ToString()))
                {
                    storeAddress.Append(htmlOutput ? "<br/>" : " ");
                }
                storeAddress.Append(city);
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (!string.IsNullOrEmpty(storeAddress.ToString()))
                {
                    storeAddress.Append(", ");
                }
                storeAddress.Append(state);
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                if (!string.IsNullOrEmpty(storeAddress.ToString()))
                {
                    storeAddress.Append(" ");
                }
                storeAddress.Append(zipCode);
            }

            return storeAddress.ToString();
        }

        // End :GetStoreAddress  ****************************************************************************************************************
        // **************************************************************************************************************************************


        // Start :GetStoreTypeName  *************************************************************************************************************
        // **************************************************************************************************************************************
        /// <summary>
        /// To get the storeType name based on Item Id...
        /// </summary>
        /// <param name="storeTypeId">store type template id</param>
        /// <returns>Returns Store type name...</returns>
        private static string GetStoreTypeName(string storeTypeId)
        {
            var storeTypeItem = SitecoreHelper.ItemMethods.GetItemFromGUID(storeTypeId);
            return SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.Title, storeTypeItem,
                false);
        }

        // End :GetStoreTypeName  **************************************************************************************************************
        // *************************************************************************************************************************************


        // Start :GetStoreRepresentativeAddress  ***********************************************************************************************
        // *************************************************************************************************************************************
        /// <summary>
        /// To Get the address of sales representtaive
        /// </summary>
        /// <param name="salesRepresentative">Sales Representative Model</param>
        /// <returns>Returns SalesRepresentative address...</returns>
        public static string GetStoreRepresentativeAddress(SalesRepresentative salesRepresentative)
        {
            var item = SitecoreHelper.ItemMethods.GetItemFromGUID(salesRepresentative.Id.ToString());
            return GetStoreAddress(item, true);
        }

        // End :GetStoreRepresentativeAddress  *************************************************************************************************
        // *************************************************************************************************************************************

        private static Store GetStore(Item storeItem, string typeName)
        {
            var locationLatitude = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreLatitude, storeItem, false);
            var locationLongitude = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreLongitude, storeItem, false);

            return new Store
            {
                StoreName = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreName, storeItem, false),
                IsSymmonsPreferred = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                    Constants.FieldNames.IsSymmonsPreferred, storeItem, false) == "1" ? Translate.Text(Constants.Dictionary.SymmonsPreferred) : string.Empty,
                IsSymmonsPreferredDescription = Translate.Text(Constants.Dictionary.SymmonsPreferredDescription),
                Address = GetStoreAddress(storeItem),
                Directions = GetStoreDirection(storeItem),
                PhoneNo = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StorePhone, storeItem, false),
                Latitude = locationLatitude,
                Longitude = locationLongitude,
                DirectionsTitle = Translate.Text(Constants.Dictionary.DirectionsTitle),
                MoreLikeThis = Translate.Text(Constants.Dictionary.MoreLike),
                MoreLocationsLikeThis = Translate.Text(Constants.Dictionary.MoreLocation),
                Address1 = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine1, storeItem, false).Trim(),
                Address2 = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreAddressLine2, storeItem, false).Trim(),
                City = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreCity, storeItem, false).Trim(),
                State = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreState, storeItem, false).Trim(),
                Zip = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreZip, storeItem, false).Trim(),
                Manager = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreManager, storeItem, false).Trim(),
                Url = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreUrl, storeItem, false).Trim(),
                Email = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.StoreEmail, storeItem, false).Trim(),
                Type = typeName
            };
        }
    }
}
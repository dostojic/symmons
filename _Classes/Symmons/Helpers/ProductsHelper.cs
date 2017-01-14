using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Symmons.Global;
using Verndale.SharedSource.Helpers;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com._Classes.Symmons.Helpers
{
    public class ProductsHelper
    {
        public static ProductsData GetProductsListing(string keyword, string priceRangeFilter, List<Refinement> refinements, string sortby, string pageNum = "1")
        {
            var productsItems = GetProducts(keyword, priceRangeFilter, refinements, sortby);
            var productsData = new ProductsData();
            // Creates a TextInfo based on the "en-US" culture.
            var ti = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
            if (productsItems != null)
            {
                productsData.TotalRecordCount = productsItems.Count.ToString();
                var pageSize = Constants.ConstantValues.GlobalPageSize;
                var totalRecordsCount = (int)Math.Ceiling(productsItems.Count / Convert.ToDecimal(pageSize));
                productsData.TotalPagesCount = totalRecordsCount.ToString();
                var productListing = productsItems.Select(product => new Products
                {
                    ListingIsProductNew = IsProductNew(product),
                    ListingProductId = product.ID.ToString(),
                    ListingProductTitle =
                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductName,
                            product, false),
                    ListingProductUrl = SymmonsHelper.GetProductUrl(SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(product.ID.ToString()), HttpContext.Current.Request.Url),
                    ListingProductPrice = ti.ToTitleCase(GetProductMinPrice(product).ToLower().Trim()),
                    ListingProductImageUrl = GetProductImageSrc(product),
                    ListingProductModelNumber = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductModelNumber,
                            product, false),
                }).ToList();

                productsData.ProductListingData = productListing.Skip((Convert.ToInt32(pageNum) - 1) * pageSize).Take(pageSize).ToList();
                return productsData;
            }
            return null;
        }

        // START : GetProductMinPrice *****************************************************************************************************************
        // ********************************************************************************************************************************************

        // Description : Returns the Min price for product...
        public static string GetProductMinPrice(Item product)
        {
            string listPrice;
            if (LocationsHelper.IsCaSite())
            {
                listPrice = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.CanPrice, product, false);
            }
            else
            {
                listPrice = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ListPrice, product, false);
            }
            if (!String.IsNullOrEmpty(listPrice))
            {
                if (listPrice.ToLower().Trim() == Constants.ConstantValues.ProductOnAppPrice)
                {
                    return listPrice;
                }
                return Decimal.Parse(listPrice).ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            }
            return String.Empty;
        }

        // END : GetProductMinPrice *******************************************************************************************************************
        // ********************************************************************************************************************************************

        /// <summary>
        /// Returns Product Modal Items...
        /// </summary>
        /// <param name="indexName">Search index to search from...</param>
        /// <param name="language"></param>
        /// <param name="templateGuidFilter">template to search for...</param>
        /// <param name="locationGuidFilter">starting location of search...</param>
        /// <param name="fullTextQuery">keyword</param>
        /// <param name="refinementFilter">key/value field values to check for...</param>
        /// <returns></returns>
        public static List<ProductDetails> GetProductItems(string indexName, string language, string templateGuidFilter,
            string locationGuidFilter,
            string fullTextQuery, List<Refinement> refinementFilter)
        {
            var productItems = SearchHelper.GetItems(indexName, language, templateGuidFilter, locationGuidFilter,
                fullTextQuery, refinementFilter);
            if (productItems != null)
            {
                var productDetailItems = new List<ProductDetails>();
                productItems.ForEach(x => productDetailItems.Add(new ProductDetails
                {
                    ProductName = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductName, x, false),
                    PageImage = GetProductImage(x),
                    PriceMin = GetProductPrice(x).ToString(CultureInfo.InvariantCulture),
                    Url = SymmonsHelper.GetProductUrl(SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(x.ID.ToString()), HttpContext.Current.Request.Url)
                }));
                return productDetailItems;
            }
            return null;
        }

        /// <summary>
        /// To get filtered products...
        /// </summary>
        /// <param name="keyword">keyword to search...</param>
        /// <param name="priceRangeFilter">price range to search...</param>
        /// <param name="refinements">key/value fields value to search...</param>
        /// <param name="sortBy">"Low to High"/"High to Low"/"Is New"</param>
        /// <returns>Return filtered products...</returns>
        public static List<Item> GetProducts(string keyword, string priceRangeFilter, List<Refinement> refinements, string sortBy)
        {
            var productsItems = SearchHelper.GetItems(Constants.SearchIndex.Products,
                Sitecore.Context.Language.ToString(), Constants.TemplateIds.ProductDetailsTemplateId,
                Constants.FolderIds.ProductsRepository, keyword, refinements, new List<PrioritizedField>(), true,
                string.Empty, false, null, null, true);
            var resultproductsItems = new List<Item>();
            if (productsItems != null)
            {
                var regionalPrice = LocationsHelper.IsCaSite()
                    ? Constants.FieldNames.CanPrice
                    : Constants.FieldNames.PriceMin;
                var onAppProductsItems =
                    productsItems.Where(x => SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                        regionalPrice, x, false).ToLower().Trim() == Constants.ConstantValues.ProductOnAppPrice)
                        .ToList();
                productsItems = productsItems.Where(x => SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                    regionalPrice, x, false).ToLower().Trim() != Constants.ConstantValues.ProductOnAppPrice).ToList();

                if (!String.IsNullOrEmpty(priceRangeFilter))
                {
                    var lstPriceMin = new List<double>();
                    var lstPriceMax = new List<double>();
                    var minPrice = 0.0;
                    var maxPrice = 0.0;
                    var priceRangeFilterIds = priceRangeFilter.Replace(',', '|');
                    var priceRangeFilterItems = priceRangeFilterIds.Split('|');
                    if (priceRangeFilterItems.Any())
                    {
                        foreach (var price in priceRangeFilterItems)
                        {
                            var priceItem = SitecoreHelper.ItemMethods.GetItemFromGUID(price);
                            lstPriceMin.Add(
                                Convert.ToDouble(SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(regionalPrice,
                                    priceItem, false)));
                            lstPriceMax.Add(
                                Convert.ToDouble(
                                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                        Constants.FieldNames.PriceMax, priceItem, false)));
                        }
                        minPrice = lstPriceMin.Min();
                        maxPrice = lstPriceMax.Max();
                    }

                    productsItems =
                        productsItems.Where(
                            x =>
                                !String.IsNullOrEmpty(
                                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                        regionalPrice, x, false))).ToList();

                    productsItems = productsItems.Where(
                        x =>
                            Convert.ToDouble(x.Fields[regionalPrice].Value) >=
                            minPrice &&
                            Convert.ToDouble(x.Fields[regionalPrice].Value) <=
                            maxPrice).ToList();
                }
                if (!String.IsNullOrEmpty(sortBy))
                {
                    if (sortBy == Constants.ConstantValues.ConstantSortByNew)
                    {
                        // Get Products Items that are marked as "New"...
                        List<Item> productsItemsNew = productsItems.Where(
                            x =>
                                SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName(
                                    Constants.FieldNames.IsNew, x, false) &&
                                !String.IsNullOrEmpty(
                                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(regionalPrice, x, false)))
                            .OrderByDescending(x => Convert.ToDouble(x.Fields[regionalPrice].Value))
                            .ToList();
                        List<Item> productsItemsResults = productsItems.Where(
                            x => !SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName(
                                Constants.FieldNames.IsNew, x, false) &&
                                 !String.IsNullOrEmpty(
                                     SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(regionalPrice, x, false)))
                            .OrderByDescending(x => Convert.ToDouble(x.Fields[regionalPrice].Value))
                            .ToList();

                        //productsItems = new List<Item>();
                        if (productsItemsNew.Any())
                        {
                            resultproductsItems.AddRange(productsItemsNew);
                        }
                        if (onAppProductsItems.Any())
                        {
                            resultproductsItems.AddRange(onAppProductsItems);
                        }
                        if (productsItemsResults.Any())
                        {
                            resultproductsItems.AddRange(productsItemsResults);
                        }
                    }
                    else if (sortBy == Constants.ConstantValues.ConstantSortByLow)
                    {
                        resultproductsItems =
                            productsItems.Where(
                                x =>
                                    !String.IsNullOrEmpty(
                                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                            regionalPrice, x, false)))
                                .OrderBy(x => Convert.ToDouble(x.Fields[regionalPrice].Value))
                                .ToList();
                        if (onAppProductsItems.Any())
                        {
                            resultproductsItems.AddRange(onAppProductsItems);
                        }
                    }
                    else
                    {

                        if (onAppProductsItems.Any())
                        {
                            resultproductsItems.AddRange(onAppProductsItems);
                        }
                        resultproductsItems.AddRange(
                            productsItems.Where(
                                x =>
                                    !String.IsNullOrEmpty(
                                        SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                            regionalPrice, x, false)))
                                .OrderByDescending(x => Convert.ToDouble(x.Fields[regionalPrice].Value)));

                    }
                }
                else
                {
                    if (onAppProductsItems.Any())
                    {
                        resultproductsItems.AddRange(onAppProductsItems);
                    }
                    resultproductsItems.AddRange(
                        productsItems.Where(
                            x =>
                                !String.IsNullOrEmpty(
                                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                                        regionalPrice, x, false)))
                            .OrderByDescending(x => Convert.ToDouble(x.Fields[regionalPrice].Value)));


                }
                return resultproductsItems;
            }
            return null;
        }

        // START : GetProductImage *****************************************************************************************************************
        // *****************************************************************************************************************************************

        // Description : get the product Image url based on product Item...
        public static Image GetProductImage(Item product)
        {
            var productModal = SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(product.ID.ToString());
            if (productModal != null)
            {
                if (SymmonsHelper.IsValidImage(productModal.ListingImage))
                {
                    var productImage = productModal.ListingImage;
                    if (productImage != null)
                    {
                        return productImage;
                    }
                }
            }
            return null;
        }

        // END : GetProductImage *******************************************************************************************************************
        // *****************************************************************************************************************************************


        // START : GetProductPrice *****************************************************************************************************************
        // *****************************************************************************************************************************************

        // Description : get the product price...
        public static double GetProductPrice(Item product)
        {
            double canPrice;
            var productModel = SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(product.ID.ToString());
            var canPriceString = double.TryParse(productModel.CanPrice, out canPrice) ? canPrice.ToString("N") : productModel.CanPrice;

            double minPrice;
            var minPriceString = double.TryParse(productModel.PriceMin, out minPrice) ? minPrice.ToString("N") : productModel.PriceMin;

            var price = LocationsHelper.IsCaSite() ? canPriceString : minPriceString;

            if (!String.IsNullOrEmpty(price))
            {
                if (price.Trim().ToLower() == Constants.ConstantValues.ProductOnAppPrice)
                {
                    return 0;
                }
                return
                    Convert.ToDouble(price);
            }
            return 0;
        }


        // END : GetProductPrice *******************************************************************************************************************
        // *****************************************************************************************************************************************

        // START : GetProductImageSrc **************************************************************************************************************
        // *****************************************************************************************************************************************

        // GetProductImageSrc : get the product Image url based on product Item...
        public static string GetProductImageSrc(Item product)
        {
            var productModal = SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(product.ID.ToString());
            if (productModal != null)
            {
                if (SymmonsHelper.IsValidImage(productModal.ListingImage))
                {
                    var productImage = productModal.ListingImage;
                    if (productImage != null)
                    {
                        return productImage.Src;
                    }
                }
            }
            return null;
        }

        public static string GetProductTeaser(Item product)
        {
            var productTeaser = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.Teaser,
                product, false);
            if (!String.IsNullOrEmpty(productTeaser))
            {
                return productTeaser;
            }
            return null;
        }

        // END : GetProductImage *******************************************************************************************************************
        // *****************************************************************************************************************************************

        // START : IsProductNew ********************************************************************************************************************
        // *****************************************************************************************************************************************

        // Description : Function will return if product is marked as new...
        public static string IsProductNew(Item product)
        {
            if (SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName(Constants.FieldNames.IsNew, product,
                false))
            {
                return Constants.ConstantValues.ConstantSortByNew;
            }
            return String.Empty;
        }

        // END : IsProductNew **********************************************************************************************************************
        // *****************************************************************************************************************************************


        // START : GetFilterItems ******************************************************************************************************************
        // *****************************************************************************************************************************************

        // Description : Function will get all the filters list based on page category...
        public static ProductFiltersMaster GetFilterItems(ProductBrowseFilters productBrowseItem)
        {
            var productsFilterGroup = new List<ProductsFilterGroup>();
            // ProductFilterId : This will not change,so setting constants values for ID here...
            if (productBrowseItem.ProductCategory.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "100",
                    ProductFilterName = Constants.FieldNames.ProductCategory,
                    ProductFilterType = GetProductCategory(productBrowseItem.ProductCategory)
                });
            }
            if (productBrowseItem.Styles.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "200",
                    ProductFilterName = Constants.FieldNames.ProductStyles,
                    ProductFilterType = GetStyles(productBrowseItem.Styles)
                });
            }
            if (productBrowseItem.Finishes.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "300",
                    ProductFilterName = Constants.FieldNames.ProductFilterFinish,
                    ProductFilterType = GetFinishes(productBrowseItem.Finishes)
                });
            }
            if (productBrowseItem.Collections.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "400",
                    ProductFilterName = Constants.FieldNames.ProductCollections,
                    ProductFilterType = GetCollections(productBrowseItem.Collections)
                });
            }
            if (productBrowseItem.SpecialAttributes.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "500",
                    ProductFilterName = Constants.FieldNames.ProductAttributes,
                    ProductFilterType = GetProductAttributes(productBrowseItem.SpecialAttributes)
                });
            }
            if (productBrowseItem.SmartFeatures.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "600",
                    ProductFilterName = Constants.FieldNames.ProductSmartFeatures,
                    ProductFilterType = GetSmartFeatures(productBrowseItem.SmartFeatures)
                });
            }
            #region GetPriceRange

            if (LocationsHelper.IsCaSite())
            {
                // Get CA Price Range...
                if (productBrowseItem.CaPriceRange.Any())
                {
                    productsFilterGroup.Add(new ProductsFilterGroup
                    {
                        ProductFilterId = "700",
                        ProductFilterName = Constants.FieldNames.PriceRange,
                        ProductFilterType = GetPriceRanges(productBrowseItem.CaPriceRange)
                    });
                }
            }
            else
            {
                // Get US Price Range...
                if (productBrowseItem.PriceRange.Any())
                {
                    productsFilterGroup.Add(new ProductsFilterGroup
                    {
                        ProductFilterId = "700",
                        ProductFilterName = Constants.FieldNames.PriceRange,
                        ProductFilterType = GetPriceRanges(productBrowseItem.PriceRange)
                    });
                }
            }

            #endregion
            if (productBrowseItem.ProductFamily.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "800",
                    ProductFilterName = Constants.FieldNames.ProductFamily,
                    ProductFilterType = GetProductFamily(productBrowseItem.ProductFamily)
                });
            }
            if (productBrowseItem.ProductSegment.Any())
            {
                productsFilterGroup.Add(new ProductsFilterGroup
                {
                    ProductFilterId = "900",
                    ProductFilterName = Constants.FieldNames.ProductSegment,
                    ProductFilterType = GetProductSegments(productBrowseItem.ProductSegment)
                });
            }

            var masterListing = new ProductFiltersMaster { ProductFiltersGroup = productsFilterGroup };
            return masterListing;
        }

        // END : GetFilterItems *******************************************************************************************************************
        // ****************************************************************************************************************************************


        #region Compare Product Filter
        public static ProductsFilterGroup GetCompareItems(ProductBrowseFilters productBrowseItem)
        {
            var productsFilterGroup = new ProductsFilterGroup();

            if (productBrowseItem.CompareFilters.Any())
            {
                productsFilterGroup = new ProductsFilterGroup
                    {

                        ProductFilterId = "100",
                        ProductFilterName = Constants.FieldNames.CompareFilter,
                        ProductFilterType = GetCompareFilters(productBrowseItem.CompareFilters)
                    };
            }

            return productsFilterGroup;
        }

        #endregion


        // START : GetSmartFeatures ***************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all filtered smart features configured for the selected category...
        private static List<ProductsFilterType> GetSmartFeatures(IEnumerable<ProductSmartFeatures> smartFeatures)
        {
            var productsFilterType = smartFeatures.Select(smartFeature => new ProductsFilterType
            {
                FilterItemId = smartFeature.Id.ToString("B"),
                FilterName = smartFeature.PageTitle
            }).ToList();

            return productsFilterType;
        }

        // END : GetSmartFeatures *****************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetPriceRanges *****************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all filtered price range configured for the selected category...

        private static List<ProductsFilterType> GetPriceRanges(IEnumerable<PriceRange> priceRanges)
        {
            var productsFilterType = priceRanges.Select(priceRange => new ProductsFilterType
            {
                FilterItemId = priceRange.Id.ToString("B"),
                FilterName = priceRange.PriceRangebetween
            }).ToList();

            return productsFilterType;
        }

        // END : GetPriceRanges *******************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetProductSegments *************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all product segments configured for the selected category...

        private static List<ProductsFilterType> GetProductSegments(IEnumerable<ProductSegment> productSegments)
        {
            var productsFilterType = productSegments.Select(productSegment => new ProductsFilterType
            {
                FilterItemId = productSegment.Id.ToString("B"),
                FilterName = productSegment.SegmentName
            }).ToList();

            return productsFilterType;
        }

        // END : GetProductSegments ***************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetProductFamily ***************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all product families configured for the selected category...

        private static List<ProductsFilterType> GetProductFamily(IEnumerable<ProductFamily> productFamilies)
        {
            var productsFilterType = productFamilies.Select(productFamily => new ProductsFilterType
            {
                FilterItemId = productFamily.Id.ToString("B"),
                FilterName = productFamily.FamilyName
            }).ToList();

            return productsFilterType;
        }

        // END : GetProductFamily *****************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetProductCategory *************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all product categories configured for the selected category...

        private static List<ProductsFilterType> GetProductCategory(IEnumerable<ProductCategory> productCategories)
        {
            var productsFilterType = productCategories.Select(productCategory => new ProductsFilterType
            {
                FilterItemId = productCategory.Id.ToString("B"),
                FilterName = productCategory.CategoryName
            }).ToList();

            return productsFilterType;
        }

        // END : GetProductCategory ***************************************************************************************************************
        // ****************************************************************************************************************************************

        // START : GetProductAttributes ***********************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all product categories configured for the selected category...

        private static List<ProductsFilterType> GetProductAttributes(IEnumerable<SpecialAttributes> productAttributes)
        {
            var productsFilterType = productAttributes.Select(productAttribute => new ProductsFilterType
            {
                FilterItemId = productAttribute.Id.ToString("B"),
                FilterName = productAttribute.Name
            }).ToList();

            return productsFilterType;
        }

        // END : GetProductAttributes *************************************************************************************************************
        // ****************************************************************************************************************************************

        #region GetCompareFilters
        private static List<ProductsFilterType> GetCompareFilters(IEnumerable<CompareProductFilter> compareFilter)
        {
            var productsFilterType = compareFilter.Select(x => new ProductsFilterType
            {
                FilterItemId = "{" + x.Id.ToString() + "}",
                FilterName = x.CompareFilterTitle
            }).ToList();

            return productsFilterType;
        }
        #endregion
        // START : GetStyles **********************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all filtered styles configured for the selected category...

        private static List<ProductsFilterType> GetStyles(IEnumerable<ProductStyle> productStyles)
        {
            var productsFilterType = productStyles.Select(style => new ProductsFilterType
            {
                FilterItemId = style.Id.ToString("B"),
                FilterName = style.PageTitle
            }).ToList();

            return productsFilterType;
        }

        // END : GetStyles ************************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetFinishes ********************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all filtered "finishes" configured for the selected category...

        private static List<ProductsFilterType> GetFinishes(IEnumerable<ProductFinishData> productFinishes)
        {
            var productsFilterType = productFinishes.Select(finish => new ProductsFilterType
            {
                FilterItemId = finish.Id.ToString("B"),
                FilterName = finish.FinishName
            }).ToList();

            return productsFilterType;
        }

        // END : GetFinishes **********************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : GetCollections *****************************************************************************************************************
        // ****************************************************************************************************************************************

        // Description : function will the list of all filtered "collections" configured for the selected category...

        private static List<ProductsFilterType> GetCollections(IEnumerable<ProductCollection> productCollections)
        {
            var productsFilterType = productCollections.Select(collection => new ProductsFilterType
            {
                FilterItemId = collection.Id.ToString("B"),
                FilterName = collection.PageTitle
            }).ToList();

            return productsFilterType;
        }

        private static ProductsFilterGroup GetProductsFilterGroup(string productCategoryId)
        {
            var productBrowseFilterLocationItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductBrowse);
            Item selectedFilteredItem;
            Item searchContentItem = null;
            if (!String.IsNullOrEmpty(productCategoryId))
            {
                searchContentItem = SitecoreHelper.ItemMethods.GetItemFromGUID(productCategoryId);
            }

            if (searchContentItem != null)
            {
                selectedFilteredItem =
               productBrowseFilterLocationItem.Axes.GetDescendants()
                   .Where(x => x.Fields[Constants.FieldNames.ReferenceFilterItems].Value.Contains(searchContentItem.ID.ToString()))
                   .ToList()
                   .FirstOrDefault() ?? SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.GlobalFilters);
            }
            else
            {
                selectedFilteredItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.GlobalFilters);
            }

            var selectedProductBrowseItem = SymmonsController.SitecoreCurrentContext.GetItem<ProductBrowseFilters>(selectedFilteredItem.ID.ToString());
            var filterItemsModel = GetCompareItems(selectedProductBrowseItem);

            return filterItemsModel;
        }

        public static CompareProductMaster GetCompareProducts(string productIds)
        {
            var productIdList = productIds.Split(',');
            var commonCategoryId = string.Empty;
            if (productIdList.Length.Equals(1))
            {
                var firstOrDefault = SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productIdList.FirstOrDefault()).ProductCategory.FirstOrDefault();
                if (firstOrDefault != null)
                    commonCategoryId = firstOrDefault.Id.ToString();
            }
            else
            {
                var stringLists = new List<List<string>>();
                if (!string.IsNullOrEmpty(productIdList[0]))
                {
                    var productCategories1 =
                        SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productIdList[0])
                            .ProductCategory.Select(x => x.Id.ToString())
                            .ToList();
                    stringLists.Add(productCategories1);
                }
                if (!string.IsNullOrEmpty(productIdList[1]))
                {
                    var productCategories2 =
                        SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productIdList[1])
                            .ProductCategory.Select(x => x.Id.ToString())
                            .ToList();
                    stringLists.Add(productCategories2);
                }
                if (productIdList.Length.Equals(3))
                {
                    var productCategories3 =
                        SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productIdList[2])
                            .ProductCategory.Select(x => x.Id.ToString())
                            .ToList();
                    stringLists.Add(productCategories3);
                }

                var firstOrDefault = stringLists
                    .SelectMany(list => list)
                    .GroupBy(item => item)
                    .Select(group => new { Count = @group.Count(), Item = @group.Key }).FirstOrDefault(item => item.Count == stringLists.Count);
                if (firstOrDefault != null)
                {
                    commonCategoryId = firstOrDefault.Item;
                }
            }
            if (!string.IsNullOrEmpty(commonCategoryId))
            {
                commonCategoryId = "{" + commonCategoryId + "}";
                var filterItemsModel = GetProductsFilterGroup(commonCategoryId);
                var productCategoryModel =
                    SymmonsController.SitecoreCurrentContext.GetItem<ProductCategory>(commonCategoryId);
                var compareProductList = new CompareProductList
                {
                    ProductCategoryId = commonCategoryId,
                    ProductCategoryName =
                        productCategoryModel == null ? string.Empty : productCategoryModel.CategoryName,
                    CompareProducts = new List<CompareProduct>(),
                    CollectionTitle = string.Empty,
                    FinishesTitle = string.Empty,
                    SmartFeaturesTitle = string.Empty,
                    SpecialFeaturesTitle = string.Empty,
                    LengthTitle = string.Empty,
                    HeightTitle = string.Empty,
                    WidthTitle = string.Empty
                };



                foreach (var productId in productIdList)
                {
                    compareProductList.CompareProducts.Add(GetCompareProduct(productId, filterItemsModel));
                }
                foreach (var productsFilterType in filterItemsModel.ProductFilterType)
                {
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductCollection))
                    {
                        compareProductList.CollectionTitle = Constants.FieldNames.ProductCollection;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductFinishes))
                    {
                        compareProductList.FinishesTitle = Constants.FieldNames.ProductFinishes;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductSmartFeatures))
                    {
                        compareProductList.SmartFeaturesTitle = Constants.FieldNames.ProductSmartFeatures;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductAttributes))
                    {
                        compareProductList.SpecialFeaturesTitle = Constants.FieldNames.ProductAttributes;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductLength))
                    {
                        compareProductList.LengthTitle = Constants.FieldNames.ProductLength;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductHeight))
                    {
                        compareProductList.HeightTitle = Constants.FieldNames.ProductHeight;
                    }
                    if (productsFilterType.FilterName.Equals(Constants.FieldNames.ProductWidth))
                    {
                        compareProductList.WidthTitle = Constants.FieldNames.ProductWidth;
                    }
                }

                var compareProductMaster = new CompareProductMaster
                {
                    CompareProductList = compareProductList
                };
                return compareProductMaster;
            }
            return new CompareProductMaster();
        }

        public static CompareProductMaster GetEmptyCompareProducts()
        {
            var compareProductList = new CompareProductList
            {
                ProductCategoryId = string.Empty,
                ProductCategoryName = string.Empty,
                CompareProducts = new List<CompareProduct>(),
                CollectionTitle = string.Empty,
                FinishesTitle = string.Empty,
                SmartFeaturesTitle = string.Empty,
                SpecialFeaturesTitle = string.Empty,
                LengthTitle = string.Empty,
                HeightTitle = string.Empty,
                WidthTitle = string.Empty
            };

            var compareProductMaster = new CompareProductMaster
            {
                CompareProductList = compareProductList
            };

            return compareProductMaster;
        }

        public static CompareProduct GetCompareProduct(string productId, ProductsFilterGroup filterItemsModel)
        {
            var productModel = SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productId);
            var productImage = string.Empty;
            var firstImage = productModel.ListingImage;
            if (firstImage != null)
            {
                productImage = SymmonsHelper.IsValidImage(firstImage) ? firstImage.Src : string.Empty;
            }

            double canPrice;
            var canPriceString = double.TryParse(productModel.CanPrice, out canPrice) ? canPrice.ToString("N") : productModel.CanPrice;

            double minPrice;
            var minPriceString = double.TryParse(productModel.PriceMin, out minPrice) ? minPrice.ToString("N") : productModel.PriceMin;

            var productCompareAttributes = new CompareProductAttributes
            {
                MainProductImage = productImage,
                Title = productModel.ProductName,
                IsNew = productModel.IsNew ? "true" : string.Empty,
                ModelNumberTitle = Constants.FieldNames.ProductModelNumber,
                ModelNumber = productModel.ProductModelNumber,
                Collection = string.Empty,
                StartingPriceTitle = Translate.Text(Constants.Dictionary.CompareProductsListPrice),
                StartingPrice = LocationsHelper.IsCaSite() ? canPriceString : minPriceString,
                Length = string.Empty,
                Height = string.Empty,
                Width = string.Empty
            };

            var compareProductModel = new CompareProduct
            {
                ProductId = productId,
                ProductURL = SymmonsHelper.GetProductUrl(SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(productModel.Id.ToString()), HttpContext.Current.Request.Url),
                CompareProductAttributes = new CompareProductAttributes()
            };

            if (productModel.ProductCollection != null)
            {
                productCompareAttributes.Collection = productModel.ProductCollection.PageTitle;
            }
            if (productModel.ProductFinishes != null)
            {
                productCompareAttributes.Finishes =
                    productModel.ProductFinishes.Select(finish => new CompareProductFinish
                    {
                        FinishName = finish.Finish.FinishName,
                        FinishIconUrl = finish.Finish.FinishIcon.Src
                    }).ToList();
            }
            var allSmartAttributes = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductSmartFeaturesRepository).GetChildren();
            var productSmartAttributes = productModel.SmartAttributes.Select(smartAttribute => smartAttribute.PageTitle).ToList();
            var smartFeaturesList = new List<CompareProductSmartFeatures>();
            foreach (var smartAttribute in allSmartAttributes)
            {
                string smartFeatureImageUrl;
                var smartFeatureItem = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.PageTitle, (Item)smartAttribute, false);
                SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL(Constants.FieldNames.SmallIcon, (Item)smartAttribute, out smartFeatureImageUrl);

                var feature = new CompareProductSmartFeatures();

                if (productSmartAttributes.Contains(smartFeatureItem))
                {
                    feature.FeatureName = smartFeatureItem;
                    feature.FeatureIconUrl = smartFeatureImageUrl;
                    smartFeaturesList.Add(feature);
                }
            }
            productCompareAttributes.SmartFeatures = smartFeaturesList;

            var allSpecialAttributes = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductAttributesRepository).GetChildren();
            var productSpecialAttributes = productModel.SpecialAttributes.Select(smartAttribute => smartAttribute.Name).ToList();
            var specialFeaturesList = new List<CompareProductSpecialAttributes>();
            foreach (var specialAttribute in allSpecialAttributes)
            {
                string specialFeatureImageUrl;
                var specialFeatureTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.Name, (Item)specialAttribute, false);
                SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL(Constants.FieldNames.SmallIcon, (Item)specialAttribute, out specialFeatureImageUrl);

                var feature = new CompareProductSpecialAttributes();
                if (productSpecialAttributes.Contains(specialFeatureTitle))
                {
                    feature.AttributeName = specialFeatureTitle;
                    feature.AttributeIconUrl = specialFeatureImageUrl;
                    specialFeaturesList.Add(feature);
                }
            }
            productCompareAttributes.SpecialFeatures = specialFeaturesList;
            productCompareAttributes.Length = productModel.Length;
            productCompareAttributes.Height = productModel.Height;
            productCompareAttributes.Width = productModel.Width;

            compareProductModel.CompareProductAttributes = productCompareAttributes;

            return compareProductModel;
        }

        // END : GetCollections *******************************************************************************************************************
        // ****************************************************************************************************************************************


        // START : Product Data Modals ************************************************************************************************************
        // ****************************************************************************************************************************************

        public class Products
        {
            public string ListingProductId { get; set; }
            public string ListingIsProductNew { get; set; }
            public string ListingProductTitle { get; set; }
            public string ListingProductUrl { get; set; }
            public string ListingProductTeaser { get; set; }
            public string ListingProductImageUrl { get; set; }
            public string ListingProductImageAlt { get; set; }
            public string ListingProductPrice { get; set; }
            public string ListingProductModelNumber { get; set; }
        }

        public class CompareProductMaster
        {
            public CompareProductList CompareProductList { get; set; }
        }

        public class CompareProductList
        {
            public string ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string CollectionTitle { get; set; }
            public string FinishesTitle { get; set; }
            public string SmartFeaturesTitle { get; set; }
            public string SpecialFeaturesTitle { get; set; }
            public string HeightTitle { get; set; }
            public string LengthTitle { get; set; }
            public string WidthTitle { get; set; }
            public List<CompareProduct> CompareProducts { get; set; }
        }

        public class CompareProduct
        {
            public string ProductId { get; set; }
            public string ProductURL { get; set; }
            public CompareProductAttributes CompareProductAttributes { get; set; }
        }

        public class CompareProductAttributes
        {
            public string ProductCategoryId { get; set; }
            public string MainProductImage { get; set; }
            public string Title { get; set; }
            public string IsNew { get; set; }
            public string StartingPriceTitle { get; set; }
            public string StartingPrice { get; set; }
            public string Collection { get; set; }
            public string ModelNumberTitle { get; set; }
            public string ModelNumber { get; set; }
            public List<CompareProductFinish> Finishes { get; set; }
            public List<CompareProductSmartFeatures> SmartFeatures { get; set; }
            public List<CompareProductSpecialAttributes> SpecialFeatures { get; set; }
            public string Height { get; set; }
            public string Length { get; set; }
            public string Width { get; set; }
        }

        public class CompareProductSmartFeatures
        {
            public string FeatureName { get; set; }
            public string FeatureIconUrl { get; set; }
        }
        public class CompareProductSpecialAttributes
        {
            public string AttributeName { get; set; }
            public string AttributeIconUrl { get; set; }
        }

        public class CompareProductFinish
        {
            public string FinishName { get; set; }
            public string FinishIconUrl { get; set; }
        }

        public class ProductsData
        {
            public string TotalRecordCount { get; set; }
            public string TotalPagesCount { get; set; }
            public List<Products> ProductListingData { get; set; }
            public string ContentListingCount { get; set; }
        }

        public class ProductsFilterType
        {
            public string FilterItemId { get; set; }
            public string FilterName { get; set; }
        }

        public class ProductsFilterGroup
        {
            public string ProductFilterId { get; set; }
            public string ProductFilterName { get; set; }
            public List<ProductsFilterType> ProductFilterType { get; set; }
        }

        public class ProductFiltersMaster
        {
            public List<ProductsFilterGroup> ProductFiltersGroup { get; set; }
        }

        // END : Product Data Modals ************************************************************************************************************
        // ****************************************************************************************************************************************

    }
}
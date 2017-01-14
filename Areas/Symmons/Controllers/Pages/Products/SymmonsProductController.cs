using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using symmons.com.Areas.Symmons.Models.Pages.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Global;
using symmons.com._Classes.Symmons.Helpers;
using Verndale.SharedSource.Helpers;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Products
{
    public class SymmonsProductController : SymmonsController
    {
        private static ProductDetails _productDetails = new ProductDetails();

        
        // Start : GetProductListing *******************************************************************************************************************
        //**********************************************************************************************************************************************
        // Function will get the list of all products based on filters passed...
        public JsonResult GetProductListing(string pageNum, string sortby, string searchkey, string styles, string finish, string price, string smartfeatures, string collections, string segment, string family, string category, string attributes, string isHybrid)
        {
            // Start : Get Products based on querystring filters...
            // Note : if any of the filters from "product browse" page is selected, consecutive request to load products will not consider any qyery string parameters...
            // Query string parameters will be considered to load the initial set of records...
            if (!String.IsNullOrEmpty(searchkey) || !String.IsNullOrEmpty(styles) || !String.IsNullOrEmpty(finish) || !String.IsNullOrEmpty(price) ||
                !String.IsNullOrEmpty(smartfeatures) || !String.IsNullOrEmpty(collections) || !String.IsNullOrEmpty(category) || !String.IsNullOrEmpty(segment) || !String.IsNullOrEmpty(family) || !String.IsNullOrEmpty(attributes))
            {
                #region Clear All Sessions

                Session[Constants.SessionConstants.FeatureId] = null;
                Session[Constants.SessionConstants.CollectionId] = null;
                Session[Constants.SessionConstants.ProductFamily] = null;
                Session[Constants.SessionConstants.ProductSegment] = null;
                Session[Constants.SessionConstants.StyleId] = null;
                Session[Constants.SessionConstants.CategoryId] = null;
                Session[Constants.SessionConstants.Keyword] = null;
                Session[Constants.SessionConstants.IsFromCallout] = null;
                #endregion
            }

            if (Session[Constants.SessionConstants.Keyword] != null)
            {
                searchkey = Session[Constants.SessionConstants.Keyword].ToString();
            }

            // Get smart feature to bind products based on that...
            // e.g: Smart Install, Smart Design, Smart Experience and others...
            if (String.IsNullOrEmpty(smartfeatures))
            {
                if (Session[Constants.SessionConstants.FeatureId] != null)
                {
                    smartfeatures = Session[Constants.SessionConstants.FeatureId].ToString();
                }
            }

            // Get smart collection to bind products based on that...
            // e.g : Naru, Allura, Elm and others...
            if (String.IsNullOrEmpty(collections))
            {
                if (Session[Constants.SessionConstants.CollectionId] != null)
                {
                    collections = Session[Constants.SessionConstants.CollectionId].ToString();
                }
            }

            // Get product styles to bind products based on that...
            // e.g : Traditional,Transitional and Contenporary...
            if (String.IsNullOrEmpty(styles))
            {
                if (Session[Constants.SessionConstants.StyleId] != null)
                {
                    styles = Session[Constants.SessionConstants.StyleId].ToString();
                }
            }

            // Get smart category to bind products based on that...
            // e.g: "Single handle Faucets","Double Handle faucets" and others...
            if (Session[Constants.SessionConstants.CategoryId] != null)
            {
                category = Session[Constants.SessionConstants.CategoryId].ToString();
            }

            // Get smart product family ("Bath/Kitchen) to bind products based on that...
            if (Session[Constants.SessionConstants.ProductFamily] != null)
            {
                family = Session[Constants.SessionConstants.ProductFamily].ToString();
            }

            // Get smart product segment ("hospitality/healthcare) to bind products based on that...
            if (Session[Constants.SessionConstants.ProductSegment] != null)
            {
                segment = Session[Constants.SessionConstants.ProductSegment].ToString();
            }

            // End : Get Products based on querystring filters...
            var isNew = String.Empty;

            #region Refinements

            var refinements = new List<Refinement>();
            // Adding field name/value for styles...
            if (styles != null)
            {
                var styleFilter = styles.Replace(',', '|');
                if (!String.IsNullOrEmpty(styleFilter))
                {
                    refinements.Add(new Refinement
                    {
                        FieldName = Constants.FieldNames.ProductStyle,
                        IsOr = true,
                        IsFacetRefinement = true,
                        Value = styleFilter
                    });
                }
            }

            // Adding field name/value for finishes...
            var finishFilter = finish.Replace(',', '|');
            if (!String.IsNullOrEmpty(finishFilter))
            {
                refinements.Add(new Refinement
                {
                    FieldName = Constants.FieldNames.ProductFinishes,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = finishFilter
                });
            }

            // Adding field name/value for smart features...
            if (smartfeatures != null)
            {
                var smartFeaturesFilter = smartfeatures.Replace(',', '|');
                if (!String.IsNullOrEmpty(smartFeaturesFilter))
                {
                    refinements.Add(new Refinement
                    {
                        FieldName = Constants.FieldNames.ProductSmartFeatures,
                        IsOr = true,
                        IsFacetRefinement = true,
                        Value = smartFeaturesFilter
                    });
                }
            }

            // Adding field name/value for collections...
            if (collections != null)
            {
                var collectionsFilter = collections.Replace(',', '|');
                if (!String.IsNullOrEmpty(collectionsFilter))
                {
                    refinements.Add(new Refinement
                    {
                        FieldName = Constants.FieldNames.ProductCollection,
                        IsOr = true,
                        IsFacetRefinement = true,
                        Value = collectionsFilter
                    });
                }
            }

            // Adding field name/value for categories...
            if (!String.IsNullOrEmpty(category))
            {
                refinements.Add(new Refinement
                {
                    FieldName = Constants.FieldNames.ProductCategory,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = category
                });
            }

            // Adding field name/value for product family...
            if (!String.IsNullOrEmpty(family))
            {
                refinements.Add(new Refinement
                {
                    FieldName = Constants.FieldNames.ProductFamily,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = family
                });
            }

            // Adding field name/value for product segment...
            if (!String.IsNullOrEmpty(segment))
            {
                refinements.Add(new Refinement
                {
                    FieldName = Constants.FieldNames.ProductSegment,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = segment
                });
            }

            if (!String.IsNullOrEmpty(sortby))
            {
                if (sortby == Constants.ConstantValues.ConstantSortByNew)
                {
                    // Adding field name/value for Is New...
                    if (!String.IsNullOrEmpty(isNew))
                    {
                        refinements.Add(new Refinement
                        {
                            FieldName = Constants.FieldNames.IsNew,
                            IsOr = true,
                            Value = isNew
                        });
                    }
                }
            }

            // Adding field name/value for product family...
            if (attributes != null)
            {
                var attributesFilter = attributes.Replace(',', '|');
                if (!String.IsNullOrEmpty(attributes))
                {
                    refinements.Add(new Refinement
                    {
                        FieldName = Constants.FieldNames.ProductAttributes,
                        IsOr = true,
                        IsFacetRefinement = true,
                        Value = attributesFilter
                    });
                }
            }

            refinements.Add(new Refinement
            {
                FieldName = Constants.FieldNames.ExcludeFromSearch,
                IsOr = true,
                Value = "0"
            });

            #endregion

            var productsListingData = ProductsHelper.GetProductsListing(searchkey, price, refinements, sortby, pageNum);
            if (isHybrid == "true")
            {
                if (Session[Constants.SessionConstants.ContentSearchCount] != null)
                {
                    productsListingData.ContentListingCount = Session[Constants.SessionConstants.ContentSearchCount].ToString();
                }
            }
            else
            {
                productsListingData.ContentListingCount = String.Empty;
            }

            return productsListingData != null ? Json(productsListingData, JsonRequestBehavior.AllowGet) : null;
        }

        // END : GetProductListing ***********************************************************************************************************************
        // ***********************************************************************************************************************************************


        // START : GetProductFilters *********************************************************************************************************************
        //************************************************************************************************************************************************
        // GetProductFilters : function will read the querystring parameter, based on which system will bind the filters...
        public ActionResult GetProductFilters()
        {
            // If SearchFilterId is present, system will use this Id to bind all the filters... 
            if (Request.QueryString[Constants.QueryString.SearchFilterId] != null)
            {
                Session[Constants.SessionConstants.SearchFilterId] = Request.QueryString[Constants.QueryString.SearchFilterId];
            }
            // If FeatureId is present, system will use this "feature" Id to bind all the filters...
            else if (Request.QueryString[Constants.QueryString.FeatureId] != null)
            {
                Session[Constants.SessionConstants.FeatureId] = Request.QueryString[Constants.QueryString.FeatureId];
            }
            // If CollectionId is present, system will use this "collection" Id to bind all the filters...
            if (Request.QueryString[Constants.QueryString.CollectionId] != null)
            {
                Session[Constants.SessionConstants.CollectionId] = Request.QueryString[Constants.QueryString.CollectionId];
            }
            // If CategoryId is present, system will use this "category" Id to bind all the filters...
            if (Request.QueryString[Constants.QueryString.CategoryId] != null)
            {
                Session[Constants.SessionConstants.CategoryId] = Request.QueryString[Constants.QueryString.CategoryId];
            }
            if (Request.QueryString[Constants.QueryString.FamilyId] != null)
            {
                Session[Constants.SessionConstants.ProductFamily] = Request.QueryString[Constants.QueryString.FamilyId];
            }
            if (Request.QueryString[Constants.QueryString.SegmentId] != null)
            {
                Session[Constants.SessionConstants.ProductSegment] = Request.QueryString[Constants.QueryString.SegmentId];
            }
            if (Request.QueryString[Constants.QueryString.StyleId] != null)
            {
                Session[Constants.SessionConstants.StyleId] = Request.QueryString[Constants.QueryString.StyleId];
            }
            if (Request.QueryString[Constants.QueryString.SpecialAttribute] != null)
            {
                Session[Constants.SessionConstants.SpecialAttributeId] = Request.QueryString[Constants.QueryString.SpecialAttribute];
            }
            if (Request.QueryString[Constants.QueryString.Keyword] != null)
            {
                Session[Constants.SessionConstants.Keyword] = Request.QueryString[Constants.QueryString.Keyword];
            }
            if (Request.QueryString[Constants.QueryString.ContentResultCount] != null)
            {
                Session[Constants.SessionConstants.ContentSearchCount] = Request.QueryString[Constants.QueryString.ContentResultCount];
            }

            return View(Constants.ViewPaths.ProductsBrowse);
        }

        // END : GetProductFilters ***********************************************************************************************************************
        // ***********************************************************************************************************************************************


        // START : GetProductFiltersList *****************************************************************************************************************
        // ***********************************************************************************************************************************************
        // GetProductFiltersList : Function will bind the filters on "Product Browse" page based on query string parameters used...
        // Query string parameters can be for "CategoryId", "FeatureId" and "CollectionId"...

        public JsonResult GetProductFiltersList()
        {
            Item searchContentItem = null;
            var searchContentId = string.Empty;
            if (Session[Constants.SessionConstants.SearchFilterId] != null)
            {
                searchContentId = Session[Constants.SessionConstants.SearchFilterId].ToString();
            }
            // Get Feature Id querystring value...
            else if (Session[Constants.SessionConstants.FeatureId] != null)
            {
                searchContentId = Session[Constants.SessionConstants.FeatureId].ToString();
            }
            // Get collection Id querystring value...
            else if (Session[Constants.SessionConstants.CollectionId] != null)
            {
                searchContentId = Session[Constants.SessionConstants.CollectionId].ToString();
            }
            // Get category Id querystring value...
            else if (Session[Constants.SessionConstants.CategoryId] != null)
            {
                searchContentId = Session[Constants.SessionConstants.CategoryId].ToString();
            }

            if (!String.IsNullOrEmpty(searchContentId))
            {
                searchContentItem = SitecoreHelper.ItemMethods.GetItemFromGUID(searchContentId);
            }

            var productBrowseFilterLocationItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductBrowse);
            Item selectedFilteredItem;
            if (searchContentItem != null)
            {
                selectedFilteredItem =
               productBrowseFilterLocationItem.Axes.GetDescendants()
                   .Where(x => x.Fields[Constants.FieldNames.ReferenceFilterItems].Value.Contains(searchContentItem.ID.ToString()))
                   .ToList()
                   .FirstOrDefault();

                // Adding a fallback filters(if there is no filters configured for a particular category...)
                if (selectedFilteredItem == null)
                {
                    selectedFilteredItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.GlobalFilters);
                    var selectedProductBrowseGlobalItem = SitecoreCurrentContext.GetItem<ProductBrowseFilters>(selectedFilteredItem.ID.ToString());
                    var filterItemsModal = ProductsHelper.GetFilterItems(selectedProductBrowseGlobalItem);
                    return Json(filterItemsModal, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                selectedFilteredItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.GlobalFilters);
            }

            if (selectedFilteredItem != null)
            {
                var selectedProductBrowseGlobalItem = SitecoreCurrentContext.GetItem<ProductBrowseFilters>(selectedFilteredItem.ID.ToString());
                var filterItemsModal = ProductsHelper.GetFilterItems(selectedProductBrowseGlobalItem);
                return Json(filterItemsModal, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        // END : GetProductFiltersList *****************************************************************************************************************
        // *********************************************************************************************************************************************


        public ActionResult GetProductDetails()
        {
            var myProductWildcardItem = SymmonsHelper.WildCardItem;
            var currentContextItem = ContextItem;
            if (myProductWildcardItem != null)
            {
                currentContextItem = myProductWildcardItem;
                SymmonsHelper.WildCardItem = null;
            }
            ProductDetails productDetailsModel = null;
            if (currentContextItem.TemplateID.ToString() == Constants.TemplateIds.ProductDetailsTemplateId.ToUpper())
            {
                productDetailsModel = SitecoreCurrentContext.GetItem<ProductDetails>(currentContextItem.ID.ToString());
                ViewBag.SelectedFinish = null;
            }
            else if (currentContextItem.TemplateID.ToString() == Constants.TemplateIds.ProductFinishTemplateId.ToUpper())
            {
                productDetailsModel = SitecoreCurrentContext.GetItem<ProductDetails>(currentContextItem.Parent.ID.ToString());
                ViewBag.SelectedFinish = SitecoreCurrentContext.GetItem<ProductFinish>(currentContextItem.ID.ToString()).Finish.FinishName;
            }

            if (productDetailsModel == null)
            {
                var errorModel = SitecoreCurrentContext.GetItem<SymmonsError>(Constants.PageIds.ErrorPage);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View(Constants.ViewPaths.SymmonsError, errorModel);
            }
            Session[Constants.ConstantValues.SessionProductModel] = GetProductJsonData(productDetailsModel);
            Session[Constants.ConstantValues.SessionProductDetailsModel] = GetProductFinishJsonDatas(productDetailsModel.ProductFinishes);

            return View(Constants.ViewPaths.ProductDetails, productDetailsModel);
        }

        // *******************************************************************************************************************

        public ActionResult GetProductDetailsForWtb()
        {
            var myProductWildcardItem = SymmonsHelper.WildCardItem;
            var currentContextItem = ContextItem;
            if (myProductWildcardItem != null)
            {
                currentContextItem = myProductWildcardItem;
                SymmonsHelper.WildCardItem = null;
            }
            ProductDetails productDetailsModel = null;
            if (currentContextItem.TemplateID.ToString() == Constants.TemplateIds.ProductDetailsTemplateId.ToUpper())
            {
                productDetailsModel =
                    SitecoreCurrentContext.GetItem<ProductDetails>(currentContextItem.ID.ToString());
            }
            if (productDetailsModel == null)
            {
                var errorModel = SitecoreCurrentContext.GetItem<SymmonsError>(Constants.PageIds.ErrorPage);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View(Constants.ViewPaths.SymmonsError, errorModel);
            }
            return View(Constants.ViewPaths.WtbProductRetailExclusive, productDetailsModel);
        }

        // *******************************************************************************************************************

        public JsonResult GetCompareProductJsonResult(string product, string category)
        {
            var compareProductList = ProductsHelper.GetEmptyCompareProducts();
            if (!string.IsNullOrEmpty(product))
            {
                compareProductList = ProductsHelper.GetCompareProducts(product);
            }
            return Json(compareProductList, JsonRequestBehavior.AllowGet);
        }

        // *******************************************************************************************************************

        public JsonResult ProductFinishJsonResult(string finishType, string showPlumbingOption)
        {
            var productDetails = (List<ProductFinishJsonData>)Session[Constants.ConstantValues.SessionProductDetailsModel];
            var productFinishJsonData = productDetails.FirstOrDefault(x => x.FinishName == finishType);

            if (productFinishJsonData != null)
            {
                productFinishJsonData.ShowPlumbingOption = showPlumbingOption;
            }

            return Json(productFinishJsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductJsonResult(string finishType, string showPlumbingOption)
        {
            var productDetails = (List<ProductFinishJsonData>)Session[Constants.ConstantValues.SessionProductModel];
            var productFinishJsonData = productDetails.FirstOrDefault();
            
            return Json(productFinishJsonData, JsonRequestBehavior.AllowGet);
        }

        private List<ProductFinishJsonData> GetProductFinishJsonDatas(IEnumerable<ProductFinish> productFinishes)
        {
            var productFinishJsonDatas = new List<ProductFinishJsonData>();
            foreach (var productFinish in productFinishes)
            {
                #region Product finish price for CA
                if (LocationsHelper.IsCaSite())
                {
                    // If CA site, then update the Available,plumbing and other option prices accordingly...
                    productFinish.AvailableValve1Price = productFinish.AvailableValve1CanPrice;
                    productFinish.AvailableValve2Price = productFinish.AvailableValve2CanPrice;
                    productFinish.AvailableValve3Price = productFinish.AvailableValve3CanPrice;

                    productFinish.PlumbingOption1Price = productFinish.PlumbingOption1CanPrice;
                    productFinish.PlumbingOption2Price = productFinish.PlumbingOption2CanPrice;
                    productFinish.PlumbingOption3Price = productFinish.PlumbingOption3CanPrice;

                    productFinish.OtherOption1Price = productFinish.OtherOption1CanPrice;
                    productFinish.OtherOption2Price = productFinish.OtherOption2CanPrice;
                    productFinish.OtherOption3Price = productFinish.OtherOption3CanPrice;
                }
                #endregion
                #region Available Valve
                var lstAvailableValve = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = productFinish.AvailableValve1Title,
                        OptionSku = productFinish.AvailableValve1SKU,
                        OptionPrice = productFinish.AvailableValve1Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.AvailableValve2Title,
                        OptionSku = productFinish.AvailableValve2SKU,
                        OptionPrice = productFinish.AvailableValve2Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.AvailableValve3Title,
                        OptionSku = productFinish.AvailableValve3SKU,
                        OptionPrice = productFinish.AvailableValve3Price
                    },
                };

                var i = 0;
                var productFinishJsonData = new ProductFinishJsonData();
                foreach (var availableValve in lstAvailableValve.Where(x => !string.IsNullOrEmpty(x.Title)))
                {
                    if (i == 0)
                    {
                        productFinishJsonData.AvailableValve1Title = availableValve.Title;
                        productFinishJsonData.AvailableValve1SKU = availableValve.OptionSku;
                        productFinishJsonData.AvailableValve1Price = availableValve.OptionPrice;
                    }
                    if (i == 1)
                    {
                        productFinishJsonData.AvailableValve2Title = availableValve.Title;
                        productFinishJsonData.AvailableValve2SKU = availableValve.OptionSku;
                        productFinishJsonData.AvailableValve2Price = availableValve.OptionPrice;
                    }
                    if (i == 2)
                    {
                        productFinishJsonData.AvailableValve3Title = availableValve.Title;
                        productFinishJsonData.AvailableValve3SKU = availableValve.OptionSku;
                        productFinishJsonData.AvailableValve3Price = availableValve.OptionPrice;
                    }
                    ++i;
                }
                #endregion
                #region Plumbing Option
                var lstPlumbingOption = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = productFinish.PlumbingOption1Title,
                        OptionSku = productFinish.PlumbingOption1SKU,
                        OptionPrice = productFinish.PlumbingOption1Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.PlumbingOption2Title,
                        OptionSku = productFinish.PlumbingOption2SKU,
                        OptionPrice = productFinish.PlumbingOption2Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.PlumbingOption3Title,
                        OptionSku = productFinish.PlumbingOption3SKU,
                        OptionPrice = productFinish.PlumbingOption3Price
                    },
                };

                i = 0;
                foreach (var plumbingOption in lstPlumbingOption.Where(x => !string.IsNullOrEmpty(x.Title)))
                {
                    if (i == 0)
                    {
                        productFinishJsonData.PlumbingOption1Title = plumbingOption.Title;
                        productFinishJsonData.PlumbingOption1SKU = plumbingOption.OptionSku;
                        productFinishJsonData.PlumbingOption1Price = plumbingOption.OptionPrice;
                    }
                    else if (i == 1)
                    {
                        productFinishJsonData.PlumbingOption2Title = plumbingOption.Title;
                        productFinishJsonData.PlumbingOption2SKU = plumbingOption.OptionSku;
                        productFinishJsonData.PlumbingOption2Price = plumbingOption.OptionPrice;
                    }
                    if (i == 2)
                    {
                        productFinishJsonData.PlumbingOption3Title = plumbingOption.Title;
                        productFinishJsonData.PlumbingOption3SKU = plumbingOption.OptionSku;
                        productFinishJsonData.PlumbingOption3Price = plumbingOption.OptionPrice;
                    }
                    ++i;
                }
                #endregion
                #region Other option
                var lstOtherOption = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = productFinish.OtherOption1Title,
                        OptionSku = productFinish.OtherOption1SKU,
                        OptionPrice = productFinish.OtherOption1Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.OtherOption2Title,
                        OptionSku = productFinish.OtherOption2SKU,
                        OptionPrice = productFinish.OtherOption2Price
                    },
                    new ProductFinishOption
                    {
                        Title = productFinish.OtherOption3Title,
                        OptionSku = productFinish.OtherOption3SKU,
                        OptionPrice = productFinish.OtherOption3Price
                    },
                };

                i = 0;
                foreach (var otherOption in lstOtherOption.Where(x => !string.IsNullOrEmpty(x.Title)))
                {
                    if (i == 0)
                    {
                        productFinishJsonData.OtherOption1Title = otherOption.Title;
                        productFinishJsonData.OtherOption1SKU = otherOption.OptionSku;
                        productFinishJsonData.OtherOption1Price = otherOption.OptionPrice;
                    }
                    else if (i == 1)
                    {
                        productFinishJsonData.OtherOption2Title = otherOption.Title;
                        productFinishJsonData.OtherOption2SKU = otherOption.OptionSku;
                        productFinishJsonData.OtherOption2Price = otherOption.OptionPrice;
                    }
                    else if (i == 2)
                    {
                        productFinishJsonData.OtherOption3Title = otherOption.Title;
                        productFinishJsonData.OtherOption3SKU = otherOption.OptionSku;
                        productFinishJsonData.OtherOption3Price = otherOption.OptionPrice;
                    }
                    ++i;
                }
                #endregion

                productFinishJsonData.FinishName = productFinish.Finish.FinishName;

                productFinishJsonData.ShowPlumbingOption = string.Empty;

                #region Has options
                productFinishJsonData.HasPlumbingOption = string.Empty;
                productFinishJsonData.HasOtherOption = string.Empty;
                productFinishJsonData.HasPlumbingDetails = string.Empty;
                productFinishJsonData.HasValves = string.Empty;

                if (!string.IsNullOrEmpty(productFinishJsonData.AvailableValve1Title) ||
                    !string.IsNullOrEmpty(productFinishJsonData.AvailableValve2Title) ||
                    !string.IsNullOrEmpty(productFinishJsonData.AvailableValve3Title))
                {
                    productFinishJsonData.HasValves = "true";
                }

                if (!string.IsNullOrEmpty(productFinishJsonData.PlumbingOption1Title) || !string.IsNullOrEmpty(productFinishJsonData.PlumbingOption2Title) ||
                     !string.IsNullOrEmpty(productFinishJsonData.PlumbingOption3Title))
                {
                    productFinishJsonData.HasPlumbingOption = "true";
                }

                if (!string.IsNullOrEmpty(productFinishJsonData.OtherOption1Title) ||
                    !string.IsNullOrEmpty(productFinishJsonData.OtherOption2Title) ||
                    !string.IsNullOrEmpty(productFinishJsonData.OtherOption3Title))
                {
                    productFinishJsonData.HasOtherOption = "true";
                }

                if (productFinishJsonData.HasPlumbingOption == "true" || productFinishJsonData.HasOtherOption == "true")
                {
                    productFinishJsonData.HasPlumbingDetails = "true";
                }
                #endregion

                #region Images
                if (productFinish.Images.Any())
                {
                    productFinishJsonData.SliderImages = from finishImage in productFinish.Images
                                                         select new Images
                                                         {
                                                             ImagePath = finishImage.Src,
                                                             ImageAlt = finishImage.Alt
                                                         };
                }
                #endregion

                productFinishJsonDatas.Add(productFinishJsonData);
            }
            return productFinishJsonDatas;
        }

        private List<ProductFinishJsonData> GetProductJsonData(ProductDetails productDetails)
        {
            var productFinishJsonDatas = new List<ProductFinishJsonData>();

            #region Available Valve
            var lstAvailableValve = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    }
                };

            var i = 0;
            var productFinishJsonData = new ProductFinishJsonData();
            foreach (var availableValve in lstAvailableValve.Where(x => !string.IsNullOrEmpty(x.Title)))
            {
                if (i == 0)
                {
                    productFinishJsonData.AvailableValve1Title = string.Empty;
                    productFinishJsonData.AvailableValve1SKU = string.Empty;
                    productFinishJsonData.AvailableValve1Price = 0;
                }
                if (i == 1)
                {
                    productFinishJsonData.AvailableValve2Title = string.Empty;
                    productFinishJsonData.AvailableValve2SKU = string.Empty;
                    productFinishJsonData.AvailableValve2Price = 0;
                }
                if (i == 2)
                {
                    productFinishJsonData.AvailableValve3Title = string.Empty;
                    productFinishJsonData.AvailableValve3SKU = string.Empty;
                    productFinishJsonData.AvailableValve3Price = 0;
                }
                ++i;
            }
            #endregion
            #region Plumbing Option
            var lstPlumbingOption = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                };

            i = 0;
            foreach (var plumbingOption in lstPlumbingOption.Where(x => !string.IsNullOrEmpty(x.Title)))
            {
                if (i == 0)
                {
                    productFinishJsonData.PlumbingOption1Title = string.Empty;
                    productFinishJsonData.PlumbingOption1SKU = string.Empty;
                    productFinishJsonData.PlumbingOption1Price = 0;
                }
                else if (i == 1)
                {
                    productFinishJsonData.PlumbingOption2Title = string.Empty;
                    productFinishJsonData.PlumbingOption2SKU = string.Empty;
                    productFinishJsonData.PlumbingOption2Price = 0;
                }
                if (i == 2)
                {
                    productFinishJsonData.PlumbingOption3Title = string.Empty;
                    productFinishJsonData.PlumbingOption3SKU = string.Empty;
                    productFinishJsonData.PlumbingOption3Price = 0;
                }
                ++i;
            }
            #endregion
            #region Other option
            var lstOtherOption = new List<ProductFinishOption>
                {
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                    new ProductFinishOption
                    {
                        Title = string.Empty,
                        OptionSku = string.Empty,
                        OptionPrice = 0
                    },
                };

            i = 0;
            foreach (var otherOption in lstOtherOption.Where(x => !string.IsNullOrEmpty(x.Title)))
            {
                if (i == 0)
                {
                    productFinishJsonData.OtherOption1Title = string.Empty;
                    productFinishJsonData.OtherOption1SKU = string.Empty;
                    productFinishJsonData.OtherOption1Price = 0;
                }
                else if (i == 1)
                {
                    productFinishJsonData.OtherOption2Title = string.Empty;
                    productFinishJsonData.OtherOption2SKU = string.Empty;
                    productFinishJsonData.OtherOption2Price = 0;
                }
                else if (i == 2)
                {
                    productFinishJsonData.OtherOption3Title = string.Empty;
                    productFinishJsonData.OtherOption3SKU = string.Empty;
                    productFinishJsonData.OtherOption3Price = 0;
                }
                ++i;
            }
            #endregion

            productFinishJsonData.FinishName = string.Empty;

            productFinishJsonData.ShowPlumbingOption = string.Empty;

            #region Has options
            productFinishJsonData.HasPlumbingOption = string.Empty;
            productFinishJsonData.HasOtherOption = string.Empty;
            productFinishJsonData.HasPlumbingDetails = string.Empty;
            productFinishJsonData.HasValves = string.Empty;

            if (!string.IsNullOrEmpty(productFinishJsonData.AvailableValve1Title) ||
                !string.IsNullOrEmpty(productFinishJsonData.AvailableValve2Title) ||
                !string.IsNullOrEmpty(productFinishJsonData.AvailableValve3Title))
            {
                productFinishJsonData.HasValves = "true";
            }

            if (!string.IsNullOrEmpty(productFinishJsonData.PlumbingOption1Title) || !string.IsNullOrEmpty(productFinishJsonData.PlumbingOption2Title) ||
                 !string.IsNullOrEmpty(productFinishJsonData.PlumbingOption3Title))
            {
                productFinishJsonData.HasPlumbingOption = "true";
            }

            if (!string.IsNullOrEmpty(productFinishJsonData.OtherOption1Title) ||
                !string.IsNullOrEmpty(productFinishJsonData.OtherOption2Title) ||
                !string.IsNullOrEmpty(productFinishJsonData.OtherOption3Title))
            {
                productFinishJsonData.HasOtherOption = "true";
            }

            if (productFinishJsonData.HasPlumbingOption == "true" || productFinishJsonData.HasOtherOption == "true")
            {
                productFinishJsonData.HasPlumbingDetails = "true";
            }
            #endregion
            
            #region Images
            if (productDetails.Images.Any())
            {
                productFinishJsonData.SliderImages = from productImage in productDetails.Images
                                                     select new Images
                                                     {
                                                         ImagePath = productImage.Src,
                                                         ImageAlt = productImage.Alt
                                                     };
            }
            #endregion

            productFinishJsonDatas.Add(productFinishJsonData);

            return productFinishJsonDatas;
        }
        // *******************************************************************************************************************

        public ActionResult GetApprovedProducts()
        {
            var approvedProductsModel = SitecoreCurrentContext.GetCurrentItem<ApprovedProducts>();
            return View(Constants.ViewPaths.ApprovedProductsLanding, approvedProductsModel);
        }

        // *******************************************************************************************************************

        public ActionResult GetProductCompareDetails()
        {
            return View(Constants.ViewPaths.ProductCompare);
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
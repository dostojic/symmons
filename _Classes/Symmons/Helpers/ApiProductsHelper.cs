using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Global;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Verndale.SharedSource.Helpers;
using System.Net;
using System.Web.WebPages;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using Sitecore.Mvc.Diagnostics;

namespace symmons.com._Classes.Symmons.Helpers
{
    public class ApiProductsHelper : ProductsHelper
    {
        /// <summary>
        /// product list
        /// </summary>
        /// <param name="lstProducts"></param>
        /// <returns></returns>
        public static APIProductCollection GetAllProductCollection()
        {

            var lstProducts = GetProducts();

            var products = new APIProductCollection();

            if (!lstProducts.Any() || lstProducts == null) return null;

            try
            {
                foreach (var itm in lstProducts)
                {
                    if (itm.Fields["Product Name"].Value != "*")
                    {
                        products.Add(GeteachApiProductItem(itm));
                    }
                }
            }

            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                    String.Format("Products API: Error in GetAllProductCollection. Exception message:{0}",
                        exception.Message), new object());
            }
            return products;
        }

        /// <summary>
        /// converts each api product item into Productapimodel 
        /// </summary>
        /// <param name="itm">product detail iem</param>
        /// <returns>productapimodel</returns>
        private static ProductsApiModel GeteachApiProductItem(Item itm)
        {
            
            var product = new ProductsApiModel();
            try
            {
                product.ProductId = itm.ID.ToString();
                product.ProductURL = SymmonsHelper.GetProductUrl(
                    SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(itm.ID.ToString()),
                    HttpContext.Current.Request.Url);
                product.IsNew =
                    SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName(
                        Constants.FieldNames.IsNew, itm);

                product.SmartAttributes = GetMultilistFieldValue(itm.Fields["Smart Features"]);

                product.ProductCategory = GetMultilistFieldValue(itm.Fields["Product Category"]);
                product.ProductFinishesMultiList = GetMultilistFieldValue(itm.Fields["Product Finishes"]);
                product.ProductSegment = GetMultilistFieldValue(itm.Fields["Product Segment"]);
                product.SpecialAttributes =
                    GetSpecialAttributes(
                        SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Special Attributes",
                            itm));
                product.Documents =
                    GetDocumentsLinks(
                        SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Documents Links", itm));
                product.Images =
                    GetImageUrlList(SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Images",
                        itm));
                product.CollectionProductLinks =
                    GetProductCollectionLinksList(
                        SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName(
                            "Collection Product Links", itm));



                //Product Field Values 
                string imgUrl;
                if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Listing Image", itm,
                    out imgUrl))
                {
                    imgUrl = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host +
                             imgUrl;
                }
                var mainContent = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Main Content", itm,
                    false);
                var productName =
                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductName,
                        itm, false);
                var skuNo =
                    SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                        Constants.FieldNames.ProductModelNumber, itm, false);
                var priceMin = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Price Min", itm, false);
                var canPrice = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("CAN Price", itm, false);
                var leadTime = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Lead Time", itm, false);
                var priceMax = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Price Max", itm, false);
                var seoTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Title", itm, false);
                var featureTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Feature Title", itm,
                    false);
                var featureDesc = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(
                    "Feature Description", itm, false);
                DateTime updatedDate;
                var lastupdatedDate = string.Empty;
                if (SitecoreHelper.ItemRenderMethods.TryGetDateTimeByFieldName(itm, "__Updated", out updatedDate))
                {
                    lastupdatedDate = updatedDate.Date.ToString("yy-MM-dd");

                }

                // END: Product Field Values 

                product.ListingImage = !(string.IsNullOrEmpty(imgUrl)) ? imgUrl : string.Empty;
                product.MainContent = !string.IsNullOrEmpty(mainContent)
                    ? WebUtility.HtmlEncode(mainContent).Replace("\n", "").Replace("\r", "")
                    : string.Empty;
                product.ProductName = !string.IsNullOrEmpty(productName) ? productName : string.Empty;

                product.ProductModelNumber = !string.IsNullOrEmpty(skuNo) ? skuNo : string.Empty;
                product.PriceMin = !string.IsNullOrEmpty(priceMin) ? priceMin : string.Empty;
                product.CanPrice = !string.IsNullOrEmpty(canPrice) ? canPrice : string.Empty;
                product.LeadTime = !string.IsNullOrEmpty(leadTime) ? leadTime : string.Empty;
                product.PriceMax = !string.IsNullOrEmpty(priceMax) ? priceMax : string.Empty;
                product.SeoTitle = !string.IsNullOrEmpty(seoTitle)
                    ? WebUtility.HtmlEncode(seoTitle).Replace("\n", "").Replace("\r", "")
                    : string.Empty;
                product.FeatureTitle = !string.IsNullOrEmpty(featureTitle) ? featureTitle : string.Empty;
                product.FeatureDescription = !string.IsNullOrEmpty(featureDesc)
                    ? WebUtility.HtmlEncode(featureDesc).Replace("\n", "").Replace("\r", "")
                    : string.Empty;
                product.LastUpdatedDate = !string.IsNullOrEmpty(lastupdatedDate)
                    ? lastupdatedDate
                    : string.Empty;

                //Product family
                ReferenceField collectionField = itm.Fields["Product Collection"];
                if (collectionField != null && collectionField.TargetItem != null)
                {
                    var target = collectionField.TargetItem;
                    product.ProductCollection = target.DisplayName;
                }

                //Product collection
                ReferenceField productFamilyField = itm.Fields["Product Family"];
                if (productFamilyField != null && productFamilyField.TargetItem != null)
                {
                    var target = productFamilyField.TargetItem;
                    product.ProductFamily = target.DisplayName;
                }
                //Product Style
                ReferenceField styleField = itm.Fields["Product Style"];
                if (styleField != null && styleField.TargetItem != null)
                {
                    var target = styleField.TargetItem;
                    product.ProductStyle = target.Name;
                }

            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GeteachApiProductItem. Exception message:{0}",
                        exception.Message), new object());
            }

            return product;
        }

        /// <summary>
        /// gets a product feature list
        /// </summary>
        /// <param name="lstfeatureProducts"></param>
        /// <returns></returns>
        public static APIProductCollection GetAllFinishApiProductCollection()
        {
            List<Item> lstfeatureProducts = GetFinishProducts();
            var featureProducts = new APIProductCollection();

            if (!lstfeatureProducts.Any() || lstfeatureProducts == null) return null;
            try
            {
                foreach (var itm in lstfeatureProducts)
                {
                    if (itm.Fields["Finish SKU"].Value != "*")
                    {

                        featureProducts.Add(GeteachFinishProductApiItem(itm));
                    }
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    String.Format("Products API: Error in GetAllFeatureApiProductCollection. Exception message:{0}",
                        exception.Message), new object());

            }
            return featureProducts;
        }

        /// <summary>
        /// converts each api product feature item into Productapimodel 
        /// </summary>
        /// <param name="itm">product feature iem</param>
        /// <returns>productapimodel</returns>
        private static ProductsApiModel GeteachFinishProductApiItem(Item itm)
        {
            
            var product = new ProductsApiModel();
            try
            { 

                product.ProductId = itm.ID.ToString();

                product.ProductURL = SymmonsHelper.GetProductUrl(
                    SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(itm.ID.ToString()),
                    HttpContext.Current.Request.Url);


                product.IsNew =
                    SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName(Constants.FieldNames.IsNew, itm.Parent);

                product.SmartAttributes = GetMultilistFieldValue(itm.Parent.Fields["Smart Features"]);
                product.ProductCategory = GetMultilistFieldValue(itm.Parent.Fields["Product Category"]);
              //  product.ProductFinishesMultiList = GetMultilistFieldValue(itm.Parent.Fields["Product Finishes"]);
                product.ProductSegment = GetMultilistFieldValue(itm.Parent.Fields["Product Segment"]);
                product.SpecialAttributes =
                    GetSpecialAttributes(
                        SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Special Attributes", itm.Parent));
                product.Documents =
                    GetDocumentsLinks(SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Documents Links",
                        itm.Parent));
                product.Images =
                    GetImageUrlList(SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Images", itm));
                product.CollectionProductLinks =
                    GetProductCollectionLinksList(
                        SitecoreHelper.ItemRenderMethods.GetMultilistValueByFieldName("Collection Product Links",
                            itm.Parent));

                

                //Product Field Values 
                string imgUrl;
                if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Listing Image", itm.Parent,
                     out imgUrl))
                {
                    imgUrl = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + imgUrl;
                }
                var mainContent = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Main Content", itm.Parent, false);
                var productName = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName(Constants.FieldNames.ProductName, itm.Parent, false);
                var skuNo = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Finish SKU", itm, false);
                var leadTime = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Lead Time", itm.Parent, false);
                var priceMax = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Price Max", itm.Parent, false);
                var seoTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("SEO Title", itm.Parent, false);
                var featureTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Feature Title", itm.Parent, false);
                var featureDesc = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Feature Description", itm.Parent, false);
                DateTime updatedDate;
                var lastupdatedDate = string.Empty;
                if (SitecoreHelper.ItemRenderMethods.TryGetDateTimeByFieldName(itm, "__Updated", out updatedDate))
                {
                    lastupdatedDate = updatedDate.Date.ToString("yy-MM-dd");
                }

                // END: Product Field Values 
                product.ListingImage = !(string.IsNullOrEmpty(imgUrl)) ? imgUrl : string.Empty;
                product.MainContent = !string.IsNullOrEmpty(mainContent) ? WebUtility.HtmlEncode(mainContent).Replace("\n", "").Replace("\r", "") : string.Empty;
                product.ProductName = !string.IsNullOrEmpty(productName) ? string.Format("{0} - Finish Product", productName) : string.Empty;
                product.ProductModelNumber = !string.IsNullOrEmpty(skuNo) ? skuNo.Replace(" ", "-") : string.Empty;

                product.LeadTime = !string.IsNullOrEmpty(leadTime) ? leadTime : string.Empty;
                product.PriceMax = !string.IsNullOrEmpty(priceMax) ? priceMax : string.Empty;
                product.SeoTitle = !string.IsNullOrEmpty(seoTitle) ? WebUtility.HtmlEncode(seoTitle).Replace("\n", "").Replace("\r", "") : string.Empty;
                product.FeatureTitle = !string.IsNullOrEmpty(featureTitle) ? featureTitle : string.Empty;
                product.FeatureDescription = !string.IsNullOrEmpty(featureDesc) ? WebUtility.HtmlEncode(featureDesc).Replace("\n", "").Replace("\r", "") : string.Empty;
                product.LastUpdatedDate = !string.IsNullOrEmpty(lastupdatedDate) ? lastupdatedDate : string.Empty;

                //Product Price 
                if (product.ProductModelNumber.ToLower() == itm.Parent.Fields["Product Model Number"].Value.ToLower())
                {
                    product.PriceMin = itm.Parent.Fields["Price Min"].Value;
                    product.CanPrice = itm.Parent.Fields["CAN price"].Value;
                }
                else
                {
                    product.PriceMin = itm.Fields["Finish Price"].Value;
                    product.CanPrice = itm.Fields["Finish CAN Price"].Value;
                }

                //Product collection
                ReferenceField collectionField = itm.Parent.Fields["Product Collection"];
                if (collectionField != null && collectionField.TargetItem != null)
                {
                    var target = collectionField.TargetItem;
                    product.ProductCollection = target.DisplayName;
                }

                //Product Family
                ReferenceField productFamilyField = itm.Parent.Fields["Product Family"];
                if (productFamilyField != null && productFamilyField.TargetItem != null)
                {
                    var target = productFamilyField.TargetItem;
                    product.ProductFamily = target.DisplayName;
                }
                //Product Style
                ReferenceField styleField = itm.Parent.Fields["Product Style"];
                if (styleField != null && styleField.TargetItem != null)
                {
                    var target = styleField.TargetItem;
                    product.ProductStyle = target.Name;
                }

                //Finish Product Product Finish 
                ReferenceField finishField = itm.Fields["Finish"];
                if (finishField != null && finishField.TargetItem != null)
                {
                    var target = finishField.TargetItem;
                    product.ProductFinishesMultiList = target.Name;
                }
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                 string.Format("Products API: Error in GeteachApiProductItem. Exception message:{0}",
                     exception.Message), new object());
            }

            return product;
        }
        /// <summary>
        /// Gets Multilist fields values as comma seperated list 
        /// </summary>
        /// <param name="multilistField">multilist field</param>
        /// <returns>comma seperated list of itemnames</returns>
        public static string GetMultilistFieldValue(MultilistField multilistField)
        {
            var markupBuilder = new StringBuilder("");
            if (multilistField == null || multilistField.GetItems().Length <= 0) return string.Empty;

            try
            {
                foreach (var item in multilistField.GetItems())
                {
                    markupBuilder.Append(multilistField.GetItems().Length > 1
                        ? string.Format("{0},", item.Name)
                        : string.Format("{0}", item.Name));
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    String.Format("Products API: Error in GetMultilistFieldValue. Exception message:{0}",
                        exception.Message), new object());
            }
            return markupBuilder.ToString()
                .LastIndexOf(",", StringComparison.Ordinal)
                .ToString()
                .Equals((markupBuilder.ToString().Length - 1).ToString())
                ? markupBuilder.ToString()
                    .Remove(markupBuilder.ToString().LastIndexOf(",", StringComparison.Ordinal))
                : markupBuilder.ToString();
        }

        /// <summary>
        /// Gets a comma seperated list of special attributes 
        /// </summary>
        /// <param name="lstSpecialAttributes">comma seperated list of special attribute name and icon list </param>
        /// <returns></returns>
        public static string GetSpecialAttributes(List<Item> lstSpecialAttributes)
        {
            if (lstSpecialAttributes == null || !lstSpecialAttributes.Any()) return string.Empty;
            var markupBuilder = new StringBuilder("");
            var iconImgUrl = string.Empty;
            var img = iconImgUrl;
            string attributeName;
            try
            {
                foreach (var item in lstSpecialAttributes)
                {
                    if (SitecoreHelper.ItemRenderMethods.GetMediaImageFriendlyURL("Icon", item, out iconImgUrl))
                    {
                        if (!string.IsNullOrEmpty(iconImgUrl))
                        {
                            img = Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host + iconImgUrl;
                        }
                    }

                    attributeName = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Name", item, false);

                    markupBuilder.Append(lstSpecialAttributes.Count > 1
                        ? string.Format("{0}Name:{1}, URL:{2}{3},", "{",attributeName,img, "}")
                        : string.Format("{0}Name:{1}, URL:{2}{3}", "{",attributeName,img, "}"));
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(string.Format("Products API: Error in GetSpecialAttributes. Exception message:{0}", exception.Message), new object());
            }
            return markupBuilder.ToString()
                .LastIndexOf(",", StringComparison.Ordinal)
                .ToString()
                .Equals((markupBuilder.ToString().Length - 1).ToString())
                ? markupBuilder.ToString()
                    .Remove(markupBuilder.ToString().LastIndexOf(",", StringComparison.Ordinal))
                : markupBuilder.ToString();
        }

        /// <summary>
        /// gets the comma seperated list of documents links   
        /// </summary>
        /// <param name="lstDocuments"></param>
        /// <returns></returns>
        public static string GetDocumentsLinks(List<Item> lstDocuments)
        {
            if (!lstDocuments.Any()) return string.Empty;
            var markupBuilder = new StringBuilder("");

            try
            {
                foreach (var documentAndDownload in lstDocuments)
                {
                    var documentItem = SitecoreHelper.ItemMethods.GetItemFromGUID(documentAndDownload.ID.ToString());
                    string documentUrl;
                    SitecoreHelper.ItemRenderMethods.GetMediaURL(documentItem, out documentUrl);

                    if (!string.IsNullOrEmpty(documentItem.DisplayName) || !string.IsNullOrEmpty(documentUrl))
                    {
                        markupBuilder.Append(lstDocuments.Count > 1
                       ? string.Format("{0}Name:{1}, URL:{2}{3},", "{", SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Title", documentItem, false), documentUrl, "}")
                       : string.Format("{0}Name:{1}, URL:{2}{3}", "{", SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName("Title", documentItem, false), documentUrl, "}"));
                       
                    }
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(string.Format("Products API: Error in GetDocumentsLinks. Exception message:{0}", exception.Message), new object());
            }
            return markupBuilder.ToString()
               .LastIndexOf(",", StringComparison.Ordinal)
               .ToString()
               .Equals((markupBuilder.ToString().Length - 1).ToString())
               ? markupBuilder.ToString()
                   .Remove(markupBuilder.ToString().LastIndexOf(",", StringComparison.Ordinal))
               : markupBuilder.ToString();
        }

        /// <summary>
        /// get comma seperated list of image urls
        /// </summary>
        /// <param name="lstImagesItems">mrdia urls with a comma seperated list </param>
        /// <returns></returns>
        public static string GetImageUrlList(List<Item> lstImagesItems)
        {
            if (lstImagesItems == null || !lstImagesItems.Any()) return string.Empty;
            var markupBuilder = new StringBuilder("");
            try
            {
                foreach (var item in lstImagesItems)
                {
                    markupBuilder.Append(lstImagesItems.Count > 1
                        ? string.Format("{0}{1}{2},", Constants.ConstantValues.HttpProtocol, HttpContext.Current.Request.Url.Host,
                            Sitecore.StringUtil.EnsurePrefix('/',
                                MediaManager.GetMediaUrl(item, new MediaUrlOptions { IncludeExtension = true })))
                        : string.Format("{0}{1}{2}", Constants.ConstantValues.HttpProtocol, HttpContext.Current.Request.Url.Host,
                            Sitecore.StringUtil.EnsurePrefix('/',
                                MediaManager.GetMediaUrl(item, new MediaUrlOptions { IncludeExtension = true }))));
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(string.Format("Products API: Error in GetImageUrlList. Exception message:{0}", exception.Message), new object());
            }
            return markupBuilder.ToString()
                .LastIndexOf(",", StringComparison.Ordinal)
                .ToString()
                .Equals((markupBuilder.ToString().Length - 1).ToString())
                ? markupBuilder.ToString()
                    .Remove(markupBuilder.ToString().LastIndexOf(",", StringComparison.Ordinal))
                : markupBuilder.ToString();
        }

        /// <summary>
        /// List of comma seperated products links 
        /// </summary>
        /// <param name="lstSelectedProductsLinks"></param>
        /// <returns></returns>
        public static string GetProductCollectionLinksList(List<Item> lstSelectedProductsLinks)
        {
            if (!lstSelectedProductsLinks.Any()) return string.Empty;
            var markupBuilder = new StringBuilder("");
            try
            {
                foreach (var item in lstSelectedProductsLinks)
                {
                    var prodItem = SymmonsHelper.GetProductUrl(
                        SymmonsController.SitecoreCurrentContext.GetItem<ProductDetails>(item.ID.ToString()),
                        HttpContext.Current.Request.Url);

                    if (!string.IsNullOrEmpty(prodItem))
                    {
                        markupBuilder.Append(lstSelectedProductsLinks.Count > 1
                            ? String.Format("{0},", prodItem)
                            : String.Format("{0}", prodItem));
                    }
                }
            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(string.Format("Products API: Error in GetProductCollectionLinksList. Exception message:{0}", exception.Message), new object());
            }
            return markupBuilder.ToString()
                .LastIndexOf(",", StringComparison.Ordinal)
                .ToString()
                .Equals((markupBuilder.ToString().Length - 1).ToString())
                ? markupBuilder.ToString()
                    .Remove(markupBuilder.ToString().LastIndexOf(",", StringComparison.Ordinal))
                : markupBuilder.ToString();
        }

        /// <summary>
        /// converts apiproduct collection into ilist
        /// </summary>
        /// <param name="products">apiproduct collection</param>
        /// <returns>apiproduct list</returns>
        public static List<ProductsApiModel> GetAllApiProducts(APIProductCollection products)
        {
            List<ProductsApiModel> resultList = new List<ProductsApiModel>(products.Count);
            try
            {
                if (products.Count == 0)
                {
                    return null;
                }
                resultList.AddRange(from ProductsApiModel item in products
                                    select new ProductsApiModel()
                                    {
                                        CanPrice = item.CanPrice,
                                        CollectionProductLinks = item.CollectionProductLinks,
                                        Documents = item.Documents,
                                        FeatureDescription = item.FeatureDescription,
                                        FeatureTitle = item.FeatureTitle,
                                        Images = item.Images,
                                        IsNew = item.IsNew,
                                        LastUpdatedDate = item.LastUpdatedDate,
                                        LeadTime = item.LeadTime,
                                        ListingImage = item.ListingImage,
                                        PriceMin = item.PriceMin,
                                        MainContent = item.MainContent,
                                        SpecialAttributes = item.SpecialAttributes,
                                        SmartAttributes = item.SmartAttributes,
                                        ProductModelNumber = item.ProductModelNumber,
                                        SeoTitle = item.SeoTitle,
                                        ProductId = item.ProductId,
                                        ProductFinishesMultiList = item.ProductFinishesMultiList,
                                        ProductSegment = item.ProductSegment,
                                        ProductName = item.ProductName,
                                        PriceMax = item.PriceMax,
                                        ProductStyle = item.ProductStyle,
                                        ProductFamily = item.ProductFamily,
                                        ProductURL = item.ProductURL,
                                        ProductCollection = item.ProductCollection,
                                        ProductCategory = item.ProductCategory,
                                        Property = item.Property,
                                        UPC = item.UPC,
                                        MAPPricing = item.MAPPricing
                                    });
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                   string.Format("Products API: Error in GetAllApiProducts. Exception message:{0}",
                       exception.Message), new object());
            }

            return resultList;
        }

        /// <summary>
        /// converts object of api products models into ilist
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProductsApiModel> ConvertAllAPiProductstoIList()
        {
            var resultsetApiProductCollections = new List<ProductsApiModel>();
            try
            {
                var productLst = GetAllProductCollection();
                var finishProducts = GetAllFinishApiProductCollection();

                productLst.AddRange(finishProducts);

                if (productLst.Count <= 0) return null;
                resultsetApiProductCollections = GetAllApiProducts(productLst);
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                  string.Format("Products API: Error in ConvertAllAPiProductstoIList. Exception message:{0}",
                      exception.Message), new object());
            }
            return resultsetApiProductCollections;
        }

        /// <summary>
        /// gets list of all available products
        /// </summary>
        /// <returns>Product list</returns>
        public static List<Item> GetProducts()
        {
            var lst = new List<Item>();
            var prodList = SitecoreHelper.ItemMethods.GetItemFromGUID("{FE967D82-B6D3-46F9-8C78-5D3D8D1F3EDC}");
            if (prodList == null)
                return null;
            try
            {
                lst =
                    prodList.Axes.GetDescendants()
                        .Where(x => x.TemplateName.ToString().Equals("Product Details"))
                        .ToList();
            }

            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GetProducts. Exception message:{0}",
                        exception.Message), new object());
            }
            return lst;
            //   return products.Count > 0 ? products : null;

        }

        /// <summary>
        /// gets list f all available products
        /// </summary>
        /// <returns>Product list</returns>
        public static APIProductCollection GetProductsbyUpdatedDate(string date)
        {
            var lstProducts = new List<Item>();
            var products = new APIProductCollection();

            var prodList = SitecoreHelper.ItemMethods.GetItemFromGUID("{FE967D82-B6D3-46F9-8C78-5D3D8D1F3EDC}");
            if (prodList == null)
                return null;
            try
            {
                lstProducts =
                    prodList.Axes.GetDescendants()
                        .Where(x => x.TemplateName.ToString().Equals("Product Details") && SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(x.Fields["__Updated"].Value).Date.ToString("yyyy-MM-dd").Equals(date))
                        .ToList();
                if (!lstProducts.Any() || lstProducts == null) return null;
                foreach (var itm in lstProducts)
                {
                    if (itm.Fields["Product Name"].Value != "*")
                    {
                        products.Add(GeteachApiProductItem(itm));
                    }
                }
            }

            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GetProductsbyUpdatedDate. Exception message:{0}",
                        exception.Message), new object());
            }
            return products;


        }

        /// <summary>
        /// Gets all list of finish product
        /// </summary>
        /// <returns>finish product list</returns>
        public static List<Item> GetFinishProducts()
        {
            // var items = Context.Database.SelectItems("/sitecore/content/Global/Product Items//*[@@templatename = 'Product Finish']");
            var lst = new List<Item>();
            var prodList = SitecoreHelper.ItemMethods.GetItemFromGUID("{FE967D82-B6D3-46F9-8C78-5D3D8D1F3EDC}");
            if (prodList == null) return null;
            try
            {
                lst =
                         prodList.Axes.GetDescendants()
                             .Where(x => x.TemplateName.ToString().Equals("Product Finish"))
                             .ToList();


            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GetFinishProducts. Exception message:{0}",
                        exception.Message), new object());
            }
            return lst;

        }


        /// <summary>
        /// Gets all list of finish product ny updated date
        /// </summary>
        /// <returns>finish product list</returns>
        public static APIProductCollection GetFinishProductsbyUpdatedDate(string date)
        {
            var lstProducts = new List<Item>();
            var products = new APIProductCollection();

            var prodList = SitecoreHelper.ItemMethods.GetItemFromGUID("{FE967D82-B6D3-46F9-8C78-5D3D8D1F3EDC}");
            if (prodList == null) return null;
            try
            {

                lstProducts =
            prodList.Axes.GetDescendants()
                .Where(x => x.TemplateName.ToString().Equals("Product Finish") && SitecoreHelper.ItemRenderMethods.GetDateFromSitecoreIsoDate(x.Fields["__Updated"].Value).Date.ToString("yyyy-MM-dd").Equals(date))
                .ToList();

                if (!lstProducts.Any() || lstProducts == null) return null;
                foreach (var itm in lstProducts)
                {
                    if (itm.Fields["Finish SKU"].Value != "*")
                    {
                        products.Add(GeteachFinishProductApiItem(itm));
                    }
                }


            }
            catch (Exception exception)

            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GetFinishProductsbyUpdatedDate. Exception message:{0}",
                        exception.Message), new object());
            }
            return products;

        }
        
        /// <summary>
        /// get productapimodel item by product id
        /// </summary>
        /// <param name="guid">item id</param>
        /// <returns>ProductsApiModel</returns>
        public static ProductsApiModel GetProductByProdId(string guid)
        {
            ProductsApiModel resultProd = null;
            if (string.IsNullOrEmpty(guid) || string.IsNullOrWhiteSpace(guid))
                return null;
            try
            {
                var item = SitecoreHelper.ItemMethods.GetItemFromGUID(guid);
                if (item == null)
                    return null;
                if (item.TemplateName.Equals("Product Finish"))
                {
                    resultProd = GeteachFinishProductApiItem(item);
                }
                else if (item.TemplateName.Equals("Product Details"))
                {
                    resultProd = GeteachApiProductItem(item);
                }
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                      string.Format("Products API: Error in GetProductByProdId. Exception message:{0}",
                          exception.Message), new object());
            }

            return resultProd;
        }

        /// <summary>
        /// Gets a list of products updated by passed date
        /// </summary>
        /// <param name="date">updateddate</param>
        /// <returns></returns>
        public static IList<ProductsApiModel> GetProductsLstByUpdatedDate(string date)
        {
            var resultsetApiProductCollections = new List<ProductsApiModel>();
            if (string.IsNullOrEmpty(date) || string.IsNullOrWhiteSpace(date))
                return null;
            try
            {
                var productLst = GetProductsbyUpdatedDate(date);
                var finishProducts = GetFinishProductsbyUpdatedDate(date);
           
                if (productLst == null && finishProducts == null)
                    return null;
              
               if (productLst != null && productLst.Count > 0)
                {
                    resultsetApiProductCollections.AddRange(GetAllApiProducts(productLst));
                    if (finishProducts.Count > 0)
                    {
                        resultsetApiProductCollections.AddRange(GetAllApiProducts(finishProducts));
                    }
                }
               
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                    string.Format("Products API: Error in GetProductsLstByUpdatedDate. Exception message:{0}",
                        exception.Message), new object());
            }
            return resultsetApiProductCollections;
        }


    }

}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using Sitecore.Web;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Global.Base;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;

namespace symmons.com._Classes.Symmons.Global
{
    public class SymmonsHelper
    {
        // *****************************************************************************************************************

        public static bool IsValidLink(Link link)
        {
            if (link == null || string.IsNullOrWhiteSpace(link.Url))
                return false;

            return true;
        }

        // *****************************************************************************************************************

        public static bool IsValidImage(Image image)
        {
            if (image == null || string.IsNullOrWhiteSpace(image.Src))
                return false;

            return true;
        }

        // *****************************************************************************************************************

        public static string GetMediaUrl(string fieldName, Item item)
        {
            MediaItem mediaItem = item.Fields[fieldName].Item;
            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
        }

        // *****************************************************************************************************************

        public static string GetPageUrl(string pageId)
        {
            if (!String.IsNullOrEmpty(pageId))
            {
                var pageItem = SitecoreHelper.ItemMethods.GetItemFromGUID(pageId);
                if (pageItem != null)
                {
                    return Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host.ToLower() +
                           SitecoreHelper.ItemRenderMethods.GetItemUrl(pageItem);
                }
            }
            return string.Empty;
        }

        // *****************************************************************************************************************

        public static string GetProductUrl(ProductDetails product, Uri uri)
        {
            var idx = product.Url.LastIndexOf('/');
            var targetItemName = product.Url.Substring(idx + 1);
            var host = new object();
            if (uri != null)
            {
                host = uri.Host.ToLower();
            }
            var productUrl = Constants.ConstantValues.HttpProtocol + host + Constants.ConstantValues.ConstantProductsUrl + targetItemName;
            return productUrl;
        }

        // *****************************************************************************************************************

        public static string GetWheretoBuyProductUrl(ProductDetails product)
        {
            var idx = product.Url.LastIndexOf('/');
            var targetItemName = product.Url.Substring(idx + 1);
            var wtbProductUrl = GetPageUrl(Constants.PageIds.WhereToBuy) + "/" + targetItemName;
            return wtbProductUrl;
        }

        // *****************************************************************************************************************

        private static Item _wildCardItem;

        public static Item WildCardItem
        {
            get
            {
                if (_wildCardItem == null)
                {
                    var rawUrl = WebUtil.GetRawUrl().TrimEnd('/');
                    var productLastIndex = rawUrl.LastIndexOf('/');
                    var queryLastIndex = rawUrl.LastIndexOf('?');

                    var productItemName = string.Empty;

                    if (queryLastIndex > productLastIndex)
                    {
                        productItemName = rawUrl.Substring(productLastIndex + 1, queryLastIndex - productLastIndex - 1).Replace('-', ' ');
                    }
                    else
                    {
                        productItemName = rawUrl.Substring(productLastIndex + 1).Replace('-', ' ');
                    }

                    var productItems = SearchHelper.GetItems(Constants.SearchIndex.Products, Sitecore.Context.Language.ToString(), Constants.TemplateIds.ProductDetailsTemplateId,
                        Constants.FolderIds.ProductsRepository, String.Empty, null);
                    _wildCardItem = productItems.FirstOrDefault(x => String.Equals(x.Name, productItemName, StringComparison.CurrentCultureIgnoreCase));

                    if (_wildCardItem == null)
                    {
                        var finishLastIndex = rawUrl.LastIndexOf('/');
                        var finishName = rawUrl.Substring(finishLastIndex + 1).Replace('-', ' ');

                        var productName = rawUrl.Replace(productItemName, string.Empty).TrimEnd('/');
                        productLastIndex = productName.LastIndexOf('/');
                        productItemName = productName.Substring(productLastIndex + 1).Replace('-', ' ');
                        var productItem = productItems.FirstOrDefault(x => String.Equals(x.Name, productItemName, StringComparison.CurrentCultureIgnoreCase));

                        if (productItem == null) return _wildCardItem;
                        _wildCardItem = productItem.Axes.GetDescendants().FirstOrDefault(x => String.Equals(x.Name, finishName, StringComparison.CurrentCultureIgnoreCase));
                    }
                }
                return _wildCardItem;
            }
            set
            {
                _wildCardItem = value;
            }
        }

        // *****************************************************************************************************************

        public static string GetUrl(Item item)
        {
            if (item != null)
            {
                return Constants.ConstantValues.HttpProtocol + HttpContext.Current.Request.Url.Host.ToLower() +
                       SitecoreHelper.ItemRenderMethods.GetItemUrl(item);
            }
            return string.Empty;
        }

        // *****************************************************************************************************************

        public static bool IsMediaItem(Item item)
        {
            if (item == null) return false;

            var lstMediaTemplateIds = new List<String>
            {
                Constants.TemplateIds.Pdf,
                Constants.TemplateIds.Image,
                Constants.TemplateIds.File,
                Constants.TemplateIds.Doc,
                Constants.TemplateIds.Docx
            };
            var itemTemplateId = item.TemplateID.ToString().ToUpper();
            return lstMediaTemplateIds.Contains(itemTemplateId);
        }

        // *****************************************************************************************************************

        public static string GetTeaser(Item item)
        {
            var modal = SymmonsController.SitecoreCurrentContext.GetItem<ISymmonsBasePage>(item.ID.ToString());
            return modal != null ? modal.Teaser : null;
        }

        // *****************************************************************************************************************

        public static string GetMediaUrl(Item item)
        {
            if (item != null && IsMediaItem(item))
            {
                string documentUrl;
                SitecoreHelper.ItemRenderMethods.GetMediaURL(item, out documentUrl);
                return documentUrl;
            }
            return string.Empty;
        }

        // *****************************************************************************************************************

        public static string GetNavigationTitle(Item item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var navigationTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName
                (Constants.FieldNames.NavigationTitle, item, false);

            if (String.IsNullOrEmpty(navigationTitle))
            {
                navigationTitle = SitecoreHelper.ItemRenderMethods.GetRawValueByFieldName
                (Constants.FieldNames.PageTitle, item, false);
            }
            return navigationTitle;
        }

        // *****************************************************************************************************************

        public static string GetMediaTitle(Item mediaItem)
        {
            if (mediaItem.Fields[Constants.FieldNames.Title] != null)
            {
                var mediaTitle = mediaItem.Fields[Constants.FieldNames.Title].Value;
                if (!String.IsNullOrEmpty(mediaTitle))
                {
                    return mediaTitle;
                }
                // If there is no title configured, sytem will display the "Display Name" value of media Item...
                return mediaItem.DisplayName;
            }
            return String.Empty;
        }

        // *****************************************************************************************************************

        public static string GetMediaDescription(Item mediaItem)
        {
            if (mediaItem.Fields[Constants.FieldNames.Description] != null)
            {
                var mediaDescription = mediaItem.Fields[Constants.FieldNames.Description].Value;
                if (!String.IsNullOrEmpty(mediaDescription))
                {
                    return mediaDescription;
                }
            }
            // If there is no description configured, returns empty string...
            return String.Empty;
        }

        // *****************************************************************************************************************
        // *****************************************************************************************************************
    }
}
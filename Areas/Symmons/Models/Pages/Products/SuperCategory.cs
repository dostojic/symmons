using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Models.Global.Base;
using System.Collections.Generic;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using symmons.com._Classes.Shared.Global;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // *************************************************************************************************************************
    
    [SitecoreType(TemplateId = "{C7F35CA2-3084-498A-93CF-C9E7221914BB}", AutoMap = true)]
    public class SuperCategory : ISymmonsBasePage
    {
        public virtual Guid Id { get; set; }

        public virtual int Version { get; set; }

        public virtual string Url { get; set; }

        public virtual string PageTitle { get; set; }

        public virtual string PageSubTitle { get; set; }

        public virtual string NavigationTitle { get; set; }

        public virtual string MainContent { get; set; }

        public virtual Link Link { get; set; }

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

        [SitecoreField(FieldName = "Product Family")]
        public virtual ProductFamily ProductFamily { get; set; }

        public virtual string ProductSection
        {
            get
            {
                if (ProductSegment == null)
                {
                    return string.Empty;
                }
                if (ProductSegment.Id.ToString() == Constants.ItemIds.ResidentialSegment.ToLower())
                {
                    // Category is Home based...
                    return Translate.Text(Constants.Dictionary.Home);
                }
                // Category is "Commercial"...
                return Translate.Text(Constants.Dictionary.Commercial);
            }
        }

        public string BannerOverlayUpperText
        {
            get
            {
                if (ProductSegment == null)
                {
                    return string.Empty;
                }
                if (ProductSegment.Id.ToString() == Constants.ItemIds.ResidentialSegment.ToLower())
                {
                    // Category is Home based...
                    return Translate.Text(Constants.Dictionary.SymmonsHome);
                }
                // Category is "Commercial"...
                return Translate.Text(Constants.Dictionary.SymmonsCommercial);
            }
        }

        [SitecoreField(FieldName = "Banner Overlay Icon")]
        public virtual Image BannerOverlayIcon { get; set; }

        [SitecoreField(FieldName = "Product Segments")]
        public virtual ProductSegment ProductSegment { get; set; }

        public virtual string BannerOverlayLowerText
        {
            get
            {
                if (ProductFamily == null)
                {
                    return ProductSegment == null ? String.Empty : ProductSegment.SegmentName;
                }
                return String.IsNullOrWhiteSpace(ProductFamily.FamilyName) ? ProductSegment == null ? string.Empty : ProductSegment.SegmentName : ProductFamily.FamilyName;
            }
        }

        [SitecoreField(FieldName = "Additional Information Description")]
        public virtual string AdditionalInformationDescription { get; set; }

        [SitecoreField(FieldName = "Additional Information Link List")]
        public virtual IEnumerable<GenericLinks> AdditionalInformationLinkList { get; set; }

        [SitecoreField(FieldName = "Section Image")]
        public virtual Image SectionImage { get; set; }

        [SitecoreField(FieldName = "Section Icon")]
        public virtual Image SectionIcon { get; set; }

        [SitecoreField(FieldName = "Bath Products Category")]
        public virtual IEnumerable<ProductCategory> BathProductsCategory { get; set; }

        [SitecoreField(FieldName = "Kitchen Products Category")]
        public virtual IEnumerable<ProductCategory> KitchenProductsCategory { get; set; }

        [SitecoreField(FieldName = "Commercial Products Category")]
        public virtual IEnumerable<ProductCategory> CommercialProductsCategory { get; set; }

        [SitecoreField(FieldName = "Style")]
        public virtual IEnumerable<ProductStyle> Style { get; set; }

        [SitecoreField(FieldName = "Collection")]
        public virtual IEnumerable<ProductCollection> Collection { get; set; }

        [SitecoreField(FieldName = "Smart Feature")]
        public virtual IEnumerable<ProductSmartFeatures> SmartFeature { get; set; }

        [SitecoreField(FieldName = "New Products")]
        public virtual IEnumerable<ProductDetails> NewProducts { get; set; }

        [SitecoreField(FieldName = "Navigation Icon")]
        public virtual Image NavigationIcon { get; set; }

        [SitecoreField(FieldName = "Navigation Image")]
        public virtual Image NavigationImage { get; set; }

        [SitecoreField(FieldName = "CTA", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link Cta { get; set; }

        // *************************************************************************************************************************
        // *************************************************************************************************************************

        public virtual Image ListingImage { get; set; }
    }
}
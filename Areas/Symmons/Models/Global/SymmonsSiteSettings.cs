using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Modules.Callouts;
using symmons.com.Areas.Symmons.Models.Modules.Global.Navigation;
using symmons.com.Areas.Symmons.Models.Modules.Products;

namespace symmons.com.Areas.Symmons.Models.Global
{
    [SitecoreType(TemplateId = "{CE907B88-D62C-48AD-90B2-584E575C884E}", AutoMap = true)]
    public class SymmonsSiteSettings
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Footer Logo")]
        public virtual Image FooterLogo { get; set; }

        [SitecoreField(FieldName = "About Symmons Title")]
        public virtual string AboutSymmonsTitle { get; set; }

        [SitecoreField(FieldName = "About Symmons Teaser")]
        public virtual string AboutSymmonsTeaser { get; set; }

        [SitecoreField(FieldName = "Copyright Text")]
        public virtual string CopyrightText { get; set; }

        [SitecoreField(FieldName = "Footer Navigation List")]
        public virtual IEnumerable<NavigationSection> FooterNavigationList { get; set; }

        [SitecoreField(FieldName = "Social Media")]
        public virtual SocialMediaModule SocialMedia { get; set; }

        [SitecoreField(FieldName = "Contact Info List")]
        public virtual IEnumerable<ContactInfoCallout> ContactInfoList { get; set; }

        [SitecoreField(FieldName = "Share This Publisher Id")]
        public virtual string ShareThisPublisherId { get; set; }

        [SitecoreField(FieldName = "SEO Global Google Analytics Snippet")]
        public virtual string SeoGlobalGoogleAnalyticsSnippet { get; set; }

        [SitecoreField(FieldName = "Global Google Analytics Body")]
        public virtual string GlobalGoogleAnalyticsBody { get; set; }

        [SitecoreField(FieldName = "Analytics Tag manager Id")]
        public virtual string AnalyticsTagManagerId { get; set; }

        [SitecoreField(FieldName = "Header Logo")]
        public virtual Image HeaderLogo { get; set; }

        [SitecoreField(FieldName = "Header Logo URL", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link HeaderLogoUrl { get; set; }

        [SitecoreField(FieldName = "Utility Navigation List")]
        public virtual IEnumerable<NavigationLink> UtilityNavigationList { get; set; }

        [SitecoreField(FieldName = "Primary Navigation List")]
        public virtual IEnumerable<ProductSection> PrimaryNavigationList { get; set; }

        [SitecoreField(FieldName = "Primary Navigation Support")]
        public virtual NavigationSection PrimaryNavigationSupport { get; set; }

        [SitecoreField(FieldName = "Customer Support List")]
        public virtual IEnumerable<NavigationLink> CustomerSupportList { get; set; }

        [SitecoreField(FieldName = "Commercial Customers List")]
        public virtual IEnumerable<NavigationLink> CommercialCustomersList { get; set; }

        [SitecoreField(FieldName = "Bath Products Category")]
        public virtual IEnumerable<ProductCategory> BathProductsCategory { get; set; }

        [SitecoreField(FieldName = "Product Bath Browse Link List")]
        public virtual IEnumerable<ProductCategory> ProductBathBrowseLinkList { get; set; }

        [SitecoreField(FieldName = "Product Kitchen Browse Link List")]
        public virtual IEnumerable<ProductCategory> ProductKitchenBrowseLinkList { get; set; }

        [SitecoreField(FieldName = "Super Category Bath Browse Link List")]
        public virtual IEnumerable<ProductBrowseByType> SuperCategoryBathBrowseLinkList { get; set; }

        [SitecoreField(FieldName = "Super Category Kitchen Browse Link List")]
        public virtual IEnumerable<ProductBrowseByType> SuperCategoryKitchenBrowseLinkList { get; set; }

        [SitecoreField(FieldName = "Scripts")]
        public virtual string trackingScripts { get; set; }

        [SitecoreField(FieldName = "GTM Script")]
        public virtual string GTMScript { get; set; }
    }
}
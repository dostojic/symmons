using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Global.Base;
using symmons.com.Areas.Symmons.Models.Modules.Callouts;
using symmons.com.Areas.Symmons.Models.Modules.Products;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{F51DE184-E561-43EC-B00D-A9AAFA2A8C2B}", AutoMap = true)]
    public class ProductDetails : ISymmonsBasePage
    {
        public virtual Guid Id { get; set; }

        public virtual int Version { get; set; }

        public virtual string Url { get; set; }

        public virtual string PageTitle { get; set; }

        public virtual string PageSubTitle { get; set; }

        public virtual string NavigationTitle { get; set; }

        public virtual string MainContent { get; set; }

        public virtual Image ListingImage { get; set; }

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

        [SitecoreField(FieldName = "Product Name")]
        public virtual string ProductName { get; set; }

        [SitecoreField(FieldName = "Product Model Number")]
        public virtual string ProductModelNumber { get; set; }

        [SitecoreField(FieldName = "Product Finishes")]
        public virtual IEnumerable<ProductFinish> ProductFinishesMultiList { get; set; }

        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<ProductFinish> ProductFinishes { get; set; }

        [SitecoreField(FieldName = "Is New")]
        public virtual bool IsNew { get; set; }

        [SitecoreField(FieldName = "Has No Finishes")]
        public virtual bool HasNoFinishes { get; set; }

        [SitecoreField(FieldName = "Is Starter Idea")]
        public virtual bool IsStarterIdea { get; set; }

        [SitecoreField(FieldName = "Useful Links")]
        public virtual IEnumerable<UsefulLinks> UsefulLinks { get; set; }

        [SitecoreField(FieldName = "Where To Buy Link")]
        public virtual Link WhereToBuyLink { get; set; }

        [SitecoreField(FieldName = "Product Collection")]
        public virtual ProductCollection ProductCollection { get; set; }

        [SitecoreField(FieldName = "Feature Title")]
        public virtual string FeatureTitle { get; set; }

        [SitecoreField(FieldName = "Feature Description")]
        public virtual string FeatureDescription { get; set; }

        [SitecoreField(FieldName = "Special Attributes")]
        public virtual IEnumerable<SpecialAttributes> SpecialAttributes { get; set; }

        [SitecoreField(FieldName = "Special Attributes Description")]
        public virtual string SpecialAttributesDescription { get; set; }

        [SitecoreField(FieldName = "Smart Features")]
        public virtual IEnumerable<ProductSmartFeatures> SmartAttributes { get; set; }

        [SitecoreField(FieldName = "Dimensions Title")]
        public virtual string DimensionsTitle { get; set; }

        [SitecoreField(FieldName = "Length")]
        public virtual string Length { get; set; }

        [SitecoreField(FieldName = "Width")]
        public virtual string Width { get; set; }

        [SitecoreField(FieldName = "Height")]
        public virtual string Height { get; set; }

        [SitecoreField(FieldName = "Dimension Image")]
        public virtual Image DimensionImage { get; set; }

        [SitecoreField(FieldName = "Interactive Tour Title")]
        public virtual string InteractiveTourTitle { get; set; }

        [SitecoreField(FieldName = "Interactive Tour Teaser")]
        public virtual string InteractiveTourTeaser { get; set; }

        [SitecoreField(FieldName = "Interactive Tour Link")]
        public virtual Link InteractiveTourLink { get; set; }

        [SitecoreField(FieldName = "Interactive Tour Image")]
        public virtual Image InteractiveTourImage { get; set; }

        [SitecoreField(FieldName = "Documents Title")]
        public virtual string DocumentsTitle { get; set; }

        [SitecoreField(FieldName = "Documents Links")]
        public virtual IEnumerable<Guid> DocumentsLinks { get; set; }

        [SitecoreField(FieldName = "Support Title")]
        public virtual string SupportTitle { get; set; }

        [SitecoreField(FieldName = "Support Description")]
        public virtual string SupportDescription { get; set; }

        [SitecoreField(FieldName = "Support Links")]
        public virtual IEnumerable<Support> SupportLinks { get; set; }

        [SitecoreField(FieldName = "Related Collection Title")]
        public virtual string RelatedCollectionTitle { get; set; }

        [SitecoreField(FieldName = "Lead Time")]
        public virtual string LeadTime { get; set; }

        [SitecoreField(FieldName = "Collection Product Links")]
        public virtual IEnumerable<ProductDetails> CollectionProductLinks { get; set; }

        [SitecoreField(FieldName = "Price Min")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string PriceMin { get; set; }

        [SitecoreField(FieldName = "Price Max")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string PriceMax { get; set; }

        [SitecoreField(FieldName = "CAN Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string CanPrice { get; set; }

        [SitecoreField(FieldName = "Product Category")]
        public virtual IEnumerable<ProductCategory> ProductCategory { get; set; }

        [SitecoreField(FieldName = "Retail Exclusive")]
        public virtual IEnumerable<RetailExclusive> RetailExclusive { get; set; }

        [SitecoreField(FieldName = "RTF Callouts")]
        public virtual IEnumerable<RichTextBlockCallout> DesignStudioRtfCallouts { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
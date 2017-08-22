using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration.Fluent;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using Sitecore.Configuration;
using Sitecore.ContentSearch.ComputedFields;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    public class ProductsApiModel
    {
        public virtual string ProductId { get; set; }
        public virtual string ListingImage { get; set; }
        public virtual string ProductURL { get; set; }

        public virtual string MainContent { get; set; }
        [SitecoreField(FieldName = "Product Name")]
        public virtual string ProductName { get; set; }


        [SitecoreField(FieldName = "Product Model Number")]
        public virtual string ProductModelNumber { get; set; }

        public virtual string ParentProductModelNumber { get; set; }

        [SitecoreField(FieldName = "Product Family")]
        public virtual string ProductFamily { get; set; }

        [SitecoreField(FieldName = "Product Segment")]
        public virtual string ProductSegment { get; set; }

        [SitecoreField(FieldName = "Product Style")]
        public virtual string ProductStyle { get; set; }

        [SitecoreField(FieldName = "Product Finishes")]
        public virtual string ProductFinishesMultiList { get; set; }
        [SitecoreField(FieldName = "Product Category")]
        public virtual string ProductCategory { get; set; }
        [SitecoreField(FieldName = "Is New")]
        public virtual bool IsNew { get; set; }

        [SitecoreField(FieldName = "Price Min")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string PriceMin { get; set; }

        [SitecoreField(FieldName = "Price Max")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string PriceMax { get; set; }

        [SitecoreField(FieldName = "CAN Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual string CanPrice { get; set; }

        [SitecoreField(FieldName = "Lead Time")]
        public virtual string LeadTime { get; set; }

        public virtual string Images { get; set; }

        [SitecoreField(FieldName = "Product Collection")]
        public virtual string ProductCollection { get; set; }

        [SitecoreField(FieldName = "SEO Title")]
        public virtual string SeoTitle { get; set; }

        [SitecoreField(FieldName = "Feature Title")]
        public virtual string FeatureTitle { get; set; }

        [SitecoreField(FieldName = "Feature Description")]
        public virtual string FeatureDescription { get; set; }

        [SitecoreField(FieldName = "Special Attributes")]
        public virtual string SpecialAttributes { get; set; }


        [SitecoreField(FieldName = "Smart Features")]
        public virtual string SmartAttributes { get; set; }


        [SitecoreField(FieldName = "Documents")]
        public virtual string Documents { get; set; }

        [SitecoreField(FieldName = "Collection Product Links")]
        public virtual string CollectionProductLinks { get; set; }

        public virtual string CollectionProductModelNumbers { get; set; }

        public virtual string LastUpdatedDate { get; set; }

        public virtual string Property { get; set;  }

        public virtual string UPC { get; set; }

        public virtual string MAPPricing { get; set; }

    }
    public class APIProductCollection : ArrayList
    {
        public APIProductCollection() : base() { }
        public APIProductCollection(ICollection c) : base(c) { }
    }

}
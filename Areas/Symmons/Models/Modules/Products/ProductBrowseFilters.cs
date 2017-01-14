using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    public class ProductBrowseFilters
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Smart Features")]
        public virtual IEnumerable<ProductSmartFeatures> SmartFeatures { get; set; }

        [SitecoreField(FieldName = "Collections")]
        public virtual IEnumerable<ProductCollection> Collections { get; set; }

        [SitecoreField(FieldName = "Finishes")]
        public virtual IEnumerable<ProductFinishData> Finishes { get; set; }

        [SitecoreField(FieldName = "Styles")]
        public virtual IEnumerable<ProductStyle> Styles { get; set; }

        [SitecoreField(FieldName = "Price Range")]
        public virtual IEnumerable<PriceRange> PriceRange { get; set; }

        [SitecoreField(FieldName = "CA Price Range")]
        public virtual IEnumerable<PriceRange> CaPriceRange { get; set; }

        [SitecoreField(FieldName = "Product Family")]
        public virtual IEnumerable<ProductFamily> ProductFamily { get; set; }

        [SitecoreField(FieldName = "Product Segment")]
        public virtual IEnumerable<ProductSegment> ProductSegment { get; set; }

        [SitecoreField(FieldName = "Product Category")]
        public virtual IEnumerable<ProductCategory> ProductCategory { get; set; }

        [SitecoreField(FieldName = "Special Attributes")]
        public virtual IEnumerable<SpecialAttributes> SpecialAttributes { get; set; }

        [SitecoreField(FieldName = "Compare Filters")]
        public virtual IEnumerable<CompareProductFilter> CompareFilters { get; set; }
    }
}
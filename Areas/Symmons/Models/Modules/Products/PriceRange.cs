using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    public class PriceRange
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Price Range between")]
        public virtual string PriceRangebetween { get; set; }

        [SitecoreField(FieldName = "Price Min")]
        public virtual string PriceMin { get; set; }

        [SitecoreField(FieldName = "Price Max")]
        public virtual string PriceMax { get; set; }
    }
}
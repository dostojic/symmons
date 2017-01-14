using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Products
{
    public class ApprovedProductsCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Approved Product Title")]
        public virtual string ProductsTitle { get; set; }

        [SitecoreField(FieldName = "Approved Products")]
        public virtual IEnumerable<ProductDetails> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Modules.Products;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Products
{
    public class FamilyCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Product Family")]
        public virtual ProductFamily ProductFamily { get; set; }

        [SitecoreField(FieldName = "Products")]
        public virtual IEnumerable<ProductDetails> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Products
{
    public class CollectionCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Product Collection")]
        public virtual ProductCollection ProductCollection { get; set; }

        [SitecoreField(FieldName = "Bath Products")]
        public virtual IEnumerable<ProductDetails> BathProducts { get; set; }

        [SitecoreField(FieldName = "Kitchen Products")]
        public virtual IEnumerable<ProductDetails> KitchenProducts { get; set; }
    }
}
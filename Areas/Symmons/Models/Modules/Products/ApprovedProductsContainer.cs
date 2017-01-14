
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{B5512FFF-DF8C-4CF1-A11D-C3E52065D1F6}", AutoMap = true)]
    public class ApprovedProductsContainer
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Listing Items")]
        public IEnumerable<ApprovedProducts> ListingItems { get; set; }
    }

}
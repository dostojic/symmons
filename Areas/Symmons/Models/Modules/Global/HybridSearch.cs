using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com._Classes.Symmons.Helpers;

namespace symmons.com.Areas.Symmons.Models.Modules.Global
{
    [SitecoreType(TemplateId = "{F507600F-1AA7-4456-A2D7-15B6E9CDFDF5}", AutoMap = true)]
    public class HybridSearch
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Search Results Text")]
        public virtual string SearchResultsText { get; set; }

        [SitecoreField(FieldName = "No Result Text")]
        public virtual string NoResultText { get; set; }

        public string SearchParam { get; set; }
        public int ProductResultCount { get; set; }
        public int ContentResultCount { get; set; }

        public List<ProductsHelper.Products> ProductList { get; set; }
        public List<Content> ContentList { get; set; }
    }

    public class Content
    {
        public string ContentId { get; set; } 
        public string ContentTitle { get; set; }
        public string ContentShowUrl { get; set; }
        public string ContentUrl { get; set; }
        public string ContentTeaser { get; set; }
    }
}
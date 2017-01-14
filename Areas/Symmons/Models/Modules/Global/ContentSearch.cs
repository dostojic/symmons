
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Global;

namespace symmons.com.Areas.Symmons.Models.Modules.Global
{
    [SitecoreType(TemplateId = "{F507600F-1AA7-4456-A2D7-15B6E9CDFDF5}", AutoMap = true)]
    public class ContentSearchModel
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Search Results Text")]
        public virtual string SearchResultsText { get; set; }

        [SitecoreField(FieldName = "No Result Text")]
        public virtual string NoResultText { get; set; }
        
        public virtual string SearchParam { get; set; }
        public virtual int ProductResultCount { get; set; }
        public virtual List<Item> ContentSearchList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Pages.Global
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{2820E618-622D-46CF-8495-BA83D89CCEA7}", AutoMap = true)]
    public class SymmonsGenericListingContainer
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public string Title { get; set; }

        [SitecoreField(FieldName = "Listing Items")]
        public IEnumerable<SymmonsGeneric> ListingItems { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
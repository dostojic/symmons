using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.Navigation
{
    [SitecoreType(TemplateId = "{DF5379CC-CA14-424B-9040-09D69B84387D}", AutoMap = true)]
    public class ProductBrowseByType
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Browse Title")]
        public virtual string BrowseTitle { get; set; }

        [SitecoreField(FieldName = "Product Browse Category Value")]
        public virtual string ProductBrowseCategoryValue { get; set; }
    }
}
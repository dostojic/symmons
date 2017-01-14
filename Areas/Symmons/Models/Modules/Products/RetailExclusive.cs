


using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{73F28C5C-1F83-409F-82BB-6155C92BA6D4}", AutoMap = true)]
    public class RetailExclusive
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Image")]
        public virtual Image Image { get; set; }

        [SitecoreField(FieldName = "Url")]
        public virtual Link Url { get; set; }

    }
}
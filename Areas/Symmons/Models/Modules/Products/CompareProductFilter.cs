using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{23DCC09E-6D8E-4524-BE08-E5058F6C9B86}", AutoMap = true)]
    public class CompareProductFilter
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Compare Filter Title")]
        public virtual string CompareFilterTitle { get; set; }
    }
}
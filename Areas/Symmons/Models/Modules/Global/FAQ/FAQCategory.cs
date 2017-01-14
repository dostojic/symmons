using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.FAQ
{
    [SitecoreType(TemplateId = "{EF924895-9AB6-48FF-A714-CAE9635710E3}", AutoMap = true)]
    public class FAQCategory
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Category Name")]
        public virtual string CategoryName { get; set; }
    }
}
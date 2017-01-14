using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{240B85B5-21F2-4A13-88F4-C13CFD2ACE4E}", AutoMap = true)]
    public class Support
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Support Link")]
        public virtual Link SupportLink { get; set; }
    }
}
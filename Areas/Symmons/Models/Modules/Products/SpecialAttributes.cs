using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{0B442736-55BF-40CD-8AE3-25B19F2FA582}", AutoMap = true)]
    public class SpecialAttributes
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Icon")]
        public virtual Image Icon { get; set; }

        [SitecoreField(FieldName = "Name")]
        public virtual string Name { get; set; }

        [SitecoreField(FieldName = "Small Icon")]
        public virtual Image SmallIcon { get; set; }
    }
}
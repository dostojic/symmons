
using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{3DF6176E-282C-481D-AE0D-773EC2A38022}", AutoMap = true)]
    public class ProductFamily
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Family Name")]
        public virtual string FamilyName { get; set; }
    }
}
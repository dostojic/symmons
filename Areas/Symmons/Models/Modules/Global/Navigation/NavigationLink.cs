using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.Navigation
{
    [SitecoreType(TemplateId = "{A29486F3-6F78-475F-AFA4-FEB311755979}", AutoMap = true)]
    public class NavigationLink
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Navigation Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link NavigationLinkUrl { get; set; }
    }
}
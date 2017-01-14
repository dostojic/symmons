using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Home
{
    [SitecoreType(TemplateId = "{C4F79799-DD15-4A5E-AEC7-5F5D648D0E31}", AutoMap = true)]
    public class IconCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Callout Headline")]
        public virtual string CalloutHeadline { get; set; }

        [SitecoreField(FieldName = "Icon")]
        public virtual Image Icon { get; set; }

        [SitecoreField(FieldName = "Callout Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link CalloutLink { get; set; }
    }
}
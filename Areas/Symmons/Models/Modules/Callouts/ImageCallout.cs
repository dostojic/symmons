using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{E619B101-9A39-4BD5-B946-21EE6F1545A7}", AutoMap = true)]
    public class ImageCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Image")]
        public virtual Image Image { get; set; }

        [SitecoreField(FieldName = "Description")]
        public virtual string Description { get; set; }

        [SitecoreField(FieldName = "CTA", FieldType = SitecoreFieldType.GeneralLink, FieldId = "{BFD32F5F-2CB4-4CA8-BC25-0A7E8888FA88}")]
        public virtual Link CTA { get; set; }
    }
}
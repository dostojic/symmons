using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    public class ContactInfoCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Image")]
        public virtual Image Image { get; set; }

        [SitecoreField(FieldName = "Contact Link Url", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link ContactLinkUrl { get; set; }
    }
}
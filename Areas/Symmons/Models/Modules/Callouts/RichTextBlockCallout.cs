using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{471D807A-4EE6-49BA-A935-15589DE91B7E}", AutoMap = true)]
    public class RichTextBlockCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Headline")]
        public virtual string Headline { get; set; }

        [SitecoreField(FieldName = "Main Content")]
        public virtual string MainContent { get; set; }
    }
}
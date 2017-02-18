using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Design_Studio
{
    [SitecoreType(TemplateId = "{49AD63DD-9292-4EC2-A4C8-5B3C14F7D3B8}", AutoMap = true)]
    public class DesignStudioCarouselItem
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Text")]
        public virtual string Text { get; set; }

        [SitecoreField(FieldName = "Link")]
        public virtual Link Link { get; set; }

        [SitecoreField(FieldName = "Image")]
        public virtual Image Image { get; set; }
    }
}
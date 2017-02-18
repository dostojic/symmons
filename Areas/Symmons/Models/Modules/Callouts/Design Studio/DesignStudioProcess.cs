using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Design_Studio
{
    [SitecoreType(TemplateId = "{F269A3D3-0CA1-4A4D-94A8-9AA7F9D39441}", AutoMap = true)]
    public class DesignStudioProcess
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Text")]
        public virtual string Text { get; set; }

        [SitecoreField(FieldName = "Image")]
        public virtual Image Image { get; set; }

        [SitecoreField(FieldName = "Link")]
        public virtual Link Link { get; set; }
    }
}
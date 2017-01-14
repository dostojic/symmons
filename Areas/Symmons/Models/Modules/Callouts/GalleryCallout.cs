using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{3552D413-707F-4962-B145-C328062E938E}", AutoMap = true)]
    public class Gallery
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Thumbnail Description Headline")]
        public virtual Link Headline { get; set; }

        [SitecoreField(FieldName = "Thumbnail Description")]
        public virtual string Description { get; set; }

        [SitecoreField(FieldName = "Video Image Thumbnail")]
        public virtual Image Thumbnail { get; set; }

        [SitecoreField(FieldName = "High Resolution Image")]
        public virtual Image Image { get; set; }

        [SitecoreField(FieldName = "Youtube Video Id")]
        public virtual string Video { get; set; }
    }
}
using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{6D3CDDC0-C3C9-4991-809B-8B057A101F6E}", AutoMap = true)]
    public class SymmonsVideo
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Sub Title")]
        public virtual string SubTitle { get; set; }

        [SitecoreField(FieldName = "Video Url")]
        public virtual string VideoUrl { get; set; }
    }
}
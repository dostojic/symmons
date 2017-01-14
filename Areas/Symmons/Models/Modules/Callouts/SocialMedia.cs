using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{3858ECE9-2990-4E7F-84F0-6D023E98F008}", AutoMap = true)]
    public class SocialMedia
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Link Url", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SocialLinkUrl { get; set; }

        [SitecoreField(FieldName = "Social Media Image")]
        public virtual Image SocialMediaImage { get; set; }

        [SitecoreField(FieldName = "Social Media hover Image")]
        public virtual Image SocialMediaHoverImage { get; set; }
    }
}
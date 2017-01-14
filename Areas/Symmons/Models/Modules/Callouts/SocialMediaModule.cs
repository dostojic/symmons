using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    public class SocialMediaModule
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Callout Title")]
        public virtual string CalloutTitle { get; set; }

        [SitecoreField(FieldName = "Social Media Links List")]
        public virtual IEnumerable<SocialMedia> SocialMediaLinksList { get; set; }
    }
}
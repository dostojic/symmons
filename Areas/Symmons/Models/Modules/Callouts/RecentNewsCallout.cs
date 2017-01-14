using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Pages;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{FC65FF14-A561-4E1C-9CF7-F48307CB1BE0}", AutoMap = true)]
    public class RecentNewsCallout
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Recent News Headline")]
        public virtual string RecentNewsHeadline { get; set; }

        [SitecoreField(FieldName = "All News Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link AllNewsLink { get; set; }

        [SitecoreField(FieldName = "Recent News")]
        public virtual IEnumerable<News> RecentNews { get; set; }
    }
}
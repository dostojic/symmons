using System;
using System.Collections;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{3F6499C5-E9EB-433C-A804-24BB2C14831E}", AutoMap = true)]
    public class TimelineModule
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Description")]
        public virtual string Description { get; set; }

        [SitecoreField(FieldName = "Symmons Timelines List")]
        public virtual IEnumerable<TimelineCallout> SymmonsTimelinesList { get; set; }
    }
}
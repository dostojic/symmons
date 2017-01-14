using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{D8BF5469-F338-47A6-A046-84A5CA647BED}", AutoMap = true)]
    public class UpcomingEventsCallout
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Upcoming Events Title")]
        public virtual string UpcomingEventsTitle { get; set; }

        [SitecoreField(FieldName = "Upcoming Events")]
        public virtual IEnumerable<Events> UpcomingEvents { get; set; }
    }
}
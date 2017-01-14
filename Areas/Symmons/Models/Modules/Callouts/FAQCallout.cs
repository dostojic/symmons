using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Modules.Global.FAQ;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{C80EC8D6-68B5-4891-8D86-9CF1479A7F6A}", AutoMap = true)]
    public class FaqCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "FAQ Headline")]
        public virtual string FaqHeadline { get; set; }

        [SitecoreField(FieldName = "FAQs")]
        public virtual IEnumerable<FAQCategory> Faqs { get; set; }
    }
}
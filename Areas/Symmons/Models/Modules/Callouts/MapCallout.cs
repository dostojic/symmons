using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{60AACE19-32A7-4EB5-998A-594727F91ECB}", AutoMap = true)]
    public class MapCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "SubTitle")]
        public virtual string SubTitle { get; set; }
        
        [SitecoreField(FieldName = "Contact Info List")]
        public virtual IEnumerable<ContactInfoCallout> ContactInfoList { get; set; }

        [SitecoreField(FieldName = "Name")]
        public virtual string Name { get; set; }

        [SitecoreField(FieldName = "Address")]
        public virtual string Address { get; set; }

        [SitecoreField(FieldName = "Url")]
        public virtual string Url { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.Navigation
{
    [SitecoreType(TemplateId = "{938B4C4C-F8F6-4B26-98AC-E4C4834C2836}", AutoMap = true)]
    public class NavigationSection
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Section Title")]
        public virtual string SectionName { get; set; }

        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<NavigationLink> NavigationLinks { get; set; }
    }
}
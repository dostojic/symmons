
using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{31FFBE04-111B-4071-96DA-247DAE55E779}", AutoMap = true)]
    public class ProductGenericCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Teaser")]
        public virtual string Teaser { get; set; }

        [SitecoreField(FieldName = "Search Default Text")]
        public virtual string SearchDefaultText { get; set; }

        [SitecoreField(FieldName = "Target Url")]
        public virtual string TargetUrl { get; set; }
    }
}
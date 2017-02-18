using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Design_Studio
{
    [SitecoreType(TemplateId = "{B9C63BB1-790B-4BB4-A35C-27B01E51194A}", AutoMap = true)]
    public class DesignStudioCarousel
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Text")]
        public virtual string Text { get; set; }

        public virtual IEnumerable<DesignStudioCarouselItem> Children { get; set; }
    }
}
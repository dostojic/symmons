using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Home
{
    [SitecoreType(TemplateId = "{8962B123-775B-4BC1-8A5C-C69FF9A14928}", AutoMap = true)]
    public class HeroSlideModule
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Hero Slides")]
        public virtual IEnumerable<HeroSlide> HeroSlides { get; set; }
    }
}
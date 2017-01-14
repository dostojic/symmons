using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Home
{
    [SitecoreType(TemplateId = "{C594B807-B90A-4EA3-BC61-757034C98A8B}", AutoMap = true)]
    public class StyleHeroSlideModule
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Bath Hero Slides")]
        public virtual IEnumerable<HeroSlide> BathHeroSlides { get; set; }

        [SitecoreField(FieldName = "Kitchen Hero Slides")]
        public virtual IEnumerable<HeroSlide> KitchenHeroSlides { get; set; }
    }
}
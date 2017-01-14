using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Home
{
    [SitecoreType(TemplateId = "{0333261E-EB56-482E-A9FD-11FB9FF8BF54}", AutoMap = true)]
    public class HeroSlide
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Slide Title")]
        public virtual string SlideTitle { get; set; }

        [SitecoreField(FieldName = "Slide Description")]
        public virtual string SlideDescription { get; set; }

        [SitecoreField(FieldName = "Slide Image")]
        public virtual Image SlideImage { get; set; }

        [SitecoreField(FieldName = "Slide Mobile Image")]
        public virtual Image SlideMobileImage { get; set; }

        [SitecoreField(FieldName = "Learn More Link", FieldType = SitecoreFieldType.GeneralLink, FieldId = "{546B4F1C-B932-4183-8F5C-991A7C07C9CC}")]
        public virtual Link LearnMoreLink { get; set; }
    }
}
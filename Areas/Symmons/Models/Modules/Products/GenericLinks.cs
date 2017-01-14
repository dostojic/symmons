using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{1D5A3468-34A2-4317-942A-3A803F13D939}", AutoMap = true)]
    public class GenericLinks
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Teaser")]
        public virtual string Teaser { get; set; }

        [SitecoreField(FieldName = "Icon")]
        public virtual Image Icon { get; set; }

        [SitecoreField(FieldName = "Link")]
        public virtual Link Link { get; set; }

    }
}
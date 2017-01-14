using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using symmons.com.Areas.Symmons.Models.Pages;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    public class FeaturedNews
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Featured Text")]
        public virtual string FeaturedText { get; set; }

        [SitecoreField(FieldName = "Featured Article")]
        public virtual News FeaturedArticle { get; set; }
    }
}
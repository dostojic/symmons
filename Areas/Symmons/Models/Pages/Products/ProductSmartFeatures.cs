using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{0B442736-55BF-40CD-8AE3-25B19F2FA582}", AutoMap = true)]
    public class ProductSmartFeatures : CollectionStyleFeature
    {
        [SitecoreField(FieldName = "Icon")]
        public virtual Image Icon { get; set; }

        [SitecoreField(FieldName = "Off Icon")]
        public virtual Image OffIcon { get; set; }

        [SitecoreField(FieldName = "Small Feature Image")]
        public virtual Image SmallFeatureImage { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
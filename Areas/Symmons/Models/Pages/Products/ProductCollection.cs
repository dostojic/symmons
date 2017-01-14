using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    [SitecoreType(TemplateId = "{D94F0F1B-389C-42C6-A029-A07F473DA984}", AutoMap = true)]
    public class ProductCollection : CollectionStyleFeature
    {
        [SitecoreField(FieldName = "Small Collection Image")]
        public virtual Image SmallCollectionImage { get; set; }
    }
}
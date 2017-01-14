using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{5E0BB988-8400-4683-847E-5AF2CDEC93D1}", AutoMap = true)]
    public class ProductCategory
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Category Name")]
        public virtual string CategoryName { get; set; }

        [SitecoreField(FieldName = "Category Image")]
        public virtual Image CategoryImage { get; set; }

        [SitecoreField(FieldName = "Small Category Image")]
        public virtual Image SmallCategoryImage { get; set; }
    }
}
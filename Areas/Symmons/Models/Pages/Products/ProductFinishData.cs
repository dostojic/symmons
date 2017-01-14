using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{C463E6FE-408E-4A05-8350-23E672A4F197}", AutoMap = true)]
    public class ProductFinishData
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Product Finish Name")]
        public virtual string FinishName { get; set; }

        [SitecoreField(FieldName = "Product Finish Icon")]
        public virtual Image FinishIcon { get; set; }
    }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
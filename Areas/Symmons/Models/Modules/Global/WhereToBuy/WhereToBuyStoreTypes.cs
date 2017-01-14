using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy
{
    [SitecoreType(TemplateId = "{821B10B6-E2E3-44E0-81DE-FED4AFC79490}", AutoMap = true)]
    public class WhereToBuyStoreTypes
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Help Description")]
        public virtual string HelpDescription { get; set; }

        [SitecoreField(FieldName = "TemplateId")]
        public string TemplateId { get; set; }
    }
}

using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.FAQ
{
    [SitecoreType(TemplateId = "{FFA05991-709E-49CD-9229-2091CC81B13C}", AutoMap = true)]
    public class FAQ
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Question")]
        public virtual string Question { get; set; }

        [SitecoreField(FieldName = "Answer")]
        public virtual string Answer { get; set; }
    }
}
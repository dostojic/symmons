using System;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global
{
    //**********************************************************************************************************************

    [SitecoreType(TemplateId = "{FFA05991-709E-49CD-9229-2091CC81B13C}", AutoMap = true)]
    public class RedirectType
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName="Status")]
        public virtual string Status { get; set; }
    }

    //**********************************************************************************************************************
    //**********************************************************************************************************************
}
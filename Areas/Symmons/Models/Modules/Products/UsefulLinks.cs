using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{D23AF2CA-4A88-430D-ADB5-CDBE5DBA2BB3}", AutoMap = true)]
    public class UsefulLinks
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Useful Link")]
        public virtual Link UsefulLink { get; set; }
    }
}
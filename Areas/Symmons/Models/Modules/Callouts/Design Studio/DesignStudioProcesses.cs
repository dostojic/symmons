using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts.Design_Studio
{
    [SitecoreType(TemplateId = "{00E653EE-297E-46D2-A08A-3049CA848AE1}", AutoMap = true)]
    public class DesignStudioProcesses
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Text")]
        public virtual string Text { get; set; }

        [SitecoreField(FieldName = "Link")]
        public virtual Link Link { get; set; }

        public virtual IEnumerable<DesignStudioProcess> Children { get; set; }
    }
}
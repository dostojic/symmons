using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Products
{
    [SitecoreType(TemplateId = "{1875EAA6-552B-416D-8077-EF073637788A}", AutoMap = true)]
    public class ProductSegment
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Segment Name")]
        public virtual string SegmentName { get; set; }

        [SitecoreField(FieldName = "Segment Icon")]
        public virtual Image SegmentIcon { get; set; }

        [SitecoreField(FieldName = "Segment Link", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SegmentLink { get; set; }
    } 
}
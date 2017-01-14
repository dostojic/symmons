using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using symmons.com.Areas.Symmons.Models.Pages.Products;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{1B8BB402-6E10-4DA8-BACD-67AAB844AA79}", AutoMap = true)]
    public class TimelineCallout
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Title")]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = "Timeline Icon")]
        public virtual Image TimelineIcon { get; set; }

        [SitecoreField(FieldName = "Timeline Thumbnail Image")]
        public virtual Image TimelineThumbnailImage { get; set; }

        [SitecoreField(FieldName = "Timeline Image")]
        public virtual Image TimelineImage { get; set; }

        [SitecoreField(FieldName = "Timeline Video URL")]
        public virtual string TimelineVideoUrl { get; set; }

        [SitecoreField(FieldName = "Description")]
        public virtual string Description { get; set; }

        [SitecoreField(FieldName = "Lead Time")]
        public virtual string LeadTime { get; set; }

        [SitecoreField(FieldName = "Minimum Order Quantity")]
        public virtual string MinimumOrderQuantity { get; set; }

        [SitecoreField(FieldName = "Finishes")]
        public virtual IEnumerable<ProductFinishData> Finishes { get; set; }

        [SitecoreField(FieldName = "Custom Finish")]
        public virtual string CustomFinish { get; set; }

        [SitecoreField(FieldName = "Timeline CTA", FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link TimelineCta { get; set; }
    }
}
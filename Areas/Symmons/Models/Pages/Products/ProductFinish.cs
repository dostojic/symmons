using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    // **************************************************************************************************************

    [SitecoreType(TemplateId = "{D387AA7C-28BB-4BBA-A5A2-66E9E4279425}", AutoMap = true)]
    public class ProductFinish
    {
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Finish")]
        public virtual ProductFinishData Finish { get; set; }

        [SitecoreField(FieldName = "Finish Price")]
        public virtual float FinishPrice { get; set; }

        [SitecoreField(FieldName = "Finish CAN Price")]
        public virtual float FinishCanPrice { get; set; }

        [SitecoreField(FieldName = "Finish SKU")]
        public virtual string FinishSKU { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }

        [SitecoreField(FieldName = "Available Valve 1 Title")]
        public virtual string AvailableValve1Title { get; set; }

        [SitecoreField(FieldName = "Available Valve 1 SKU")]
        public virtual string AvailableValve1SKU { get; set; }

        [SitecoreField(FieldName = "Available Valve 1 Price")]
        public virtual float AvailableValve1Price { get; set; }

        [SitecoreField(FieldName = "Available Valve 1 CAN Price")]
        public virtual float AvailableValve1CanPrice { get; set; }

        [SitecoreField(FieldName = "Available Valve 2 Title")]
        public virtual string AvailableValve2Title { get; set; }

        [SitecoreField(FieldName = "Available Valve 2 SKU")]
        public virtual string AvailableValve2SKU { get; set; }

        [SitecoreField(FieldName = "Available Valve 2 Price")]
        public virtual float AvailableValve2Price { get; set; }

        [SitecoreField(FieldName = "Available Valve 2 CAN Price")]
        public virtual float AvailableValve2CanPrice { get; set; }

        [SitecoreField(FieldName = "Available Valve 3 Title")]
        public virtual string AvailableValve3Title { get; set; }

        [SitecoreField(FieldName = "Available Valve 3 SKU")]
        public virtual string AvailableValve3SKU { get; set; }

        [SitecoreField(FieldName = "Available Valve 3 Price")]
        public virtual float AvailableValve3Price { get; set; }

        [SitecoreField(FieldName = "Available Valve 3 CAN Price")]
        public virtual float AvailableValve3CanPrice { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 1 Title")]
        public virtual string PlumbingOption1Title { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 1 SKU")]
        public virtual string PlumbingOption1SKU { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 1 Price")]
        public virtual float PlumbingOption1Price { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 1 CAN Price")]
        public virtual float PlumbingOption1CanPrice { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 2 Title")]
        public virtual string PlumbingOption2Title { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 2 SKU")]
        public virtual string PlumbingOption2SKU { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 2 Price")]
        public virtual float PlumbingOption2Price { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 2 CAN Price")]
        public virtual float PlumbingOption2CanPrice { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 3 Title")]
        public virtual string PlumbingOption3Title { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 3 SKU")]
        public virtual string PlumbingOption3SKU { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 3 Price")]
        public virtual float PlumbingOption3Price { get; set; }

        [SitecoreField(FieldName = "Plumbing Option 3 CAN Price")]
        public virtual float PlumbingOption3CanPrice { get; set; }

        [SitecoreField(FieldName = "Other Option 1 Title")]
        public virtual string OtherOption1Title { get; set; }

        [SitecoreField(FieldName = "Other Option 1 SKU")]
        public virtual string OtherOption1SKU { get; set; }

        [SitecoreField(FieldName = "Other Option 1 Price")]
        public virtual float OtherOption1Price { get; set; }

        [SitecoreField(FieldName = "Other Option 1 CAN Price")]
        public virtual float OtherOption1CanPrice { get; set; }

        [SitecoreField(FieldName = "Other Option 2 Title")]
        public virtual string OtherOption2Title { get; set; }

        [SitecoreField(FieldName = "Other Option 2 SKU")]
        public virtual string OtherOption2SKU { get; set; }

        [SitecoreField(FieldName = "Other Option 2 Price")]
        public virtual float OtherOption2Price { get; set; }

        [SitecoreField(FieldName = "Other Option 2 CAN Price")]
        public virtual float OtherOption2CanPrice { get; set; }

        [SitecoreField(FieldName = "Other Option 3 Title")]
        public virtual string OtherOption3Title { get; set; }

        [SitecoreField(FieldName = "Other Option 3 SKU")]
        public virtual string OtherOption3SKU { get; set; }

        [SitecoreField(FieldName = "Other Option 3 Price")]
        public virtual float OtherOption3Price { get; set; }

        [SitecoreField(FieldName = "Other Option 3 CAN Price")]
        public virtual float OtherOption3CanPrice { get; set; }

     }

    // **************************************************************************************************************
    // **************************************************************************************************************
}
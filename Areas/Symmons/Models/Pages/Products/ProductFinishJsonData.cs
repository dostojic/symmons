using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace symmons.com.Areas.Symmons.Models.Pages.Products
{
    public class ProductFinishJsonData
    {
        public virtual string FinishName { get; set; }

        public virtual string AvailableValve1Title { get; set; }

        public virtual string AvailableValve1SKU { get; set; }

        public virtual float AvailableValve1Price { get; set; }

        public virtual float AvailableValve1CanPrice { get; set; }

        public virtual string AvailableValve2Title { get; set; }

        public virtual string AvailableValve2SKU { get; set; }

        public virtual float AvailableValve2Price { get; set; }

        public virtual float AvailableValve2CanPrice { get; set; }

        public virtual string AvailableValve3Title { get; set; }

        public virtual string AvailableValve3SKU { get; set; }

        public virtual float AvailableValve3Price { get; set; }

        public virtual float AvailableValve3CanPrice { get; set; }

        public virtual string PlumbingOption1Title { get; set; }

        public virtual string PlumbingOption1SKU { get; set; }

        public virtual float PlumbingOption1Price { get; set; }

        public virtual float PlumbingOption1CanPrice { get; set; }

        public virtual string PlumbingOption2Title { get; set; }

        public virtual string PlumbingOption2SKU { get; set; }

        public virtual float PlumbingOption2Price { get; set; }

        public virtual float PlumbingOption2CanPrice { get; set; }

        public virtual string PlumbingOption3Title { get; set; }

        public virtual string PlumbingOption3SKU { get; set; }

        public virtual float PlumbingOption3Price { get; set; }

        public virtual float PlumbingOption3CanPrice { get; set; }

        public virtual string OtherOption1Title { get; set; }

        public virtual string OtherOption1SKU { get; set; }

        public virtual float OtherOption1Price { get; set; }

        public virtual float OtherOption1CanPrice { get; set; }

        public virtual string OtherOption2Title { get; set; }

        public virtual string OtherOption2SKU { get; set; }

        public virtual float OtherOption2Price { get; set; }

        public virtual float OtherOption2CanPrice { get; set; }

        public virtual string OtherOption3Title { get; set; }

        public virtual string OtherOption3SKU { get; set; }

        public virtual float OtherOption3Price { get; set; }

        public virtual float OtherOption3CanPrice { get; set; }

        public virtual string ShowPlumbingOption { get; set; }

        public virtual string HasValves { get; set; }

        public virtual string HasPlumbingOption { get; set; }

        public virtual string HasOtherOption { get; set; }

        public virtual string HasPlumbingDetails { get; set; }

        public virtual IEnumerable<Images> SliderImages { get; set; }
    }

    public class ProductFinishOption
    {
        public string Title { get; set; }

        public string OptionSku { get; set; }

        public float OptionPrice { get; set; }
    }

    public class Images
    {
        public string ImagePath { get; set; }

        public string ImageAlt { get; set; }
    }
}
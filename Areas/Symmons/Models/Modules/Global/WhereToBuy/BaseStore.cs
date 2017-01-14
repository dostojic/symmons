using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy
{
    [SitecoreType(TemplateId = "{E482D0BE-96E9-4D68-BB22-CFE7C2337908}", AutoMap = true)]
    public class BaseStore
    {
        [SitecoreField(FieldName = "Store Name")]
        public virtual string StoreName { get; set; }

        [SitecoreField(FieldName = "Address 1")]
        public virtual string Address1 { get; set; }

        [SitecoreField(FieldName = "Address 2")]
        public virtual string Address2 { get; set; }

        [SitecoreField(FieldName = "City")]
        public virtual string City { get; set; }

        [SitecoreField(FieldName = "Zip")]
        public virtual string Zip { get; set; }

        [SitecoreField(FieldName = "Phone")]
        public virtual string Phone { get; set; }

        [SitecoreField(FieldName = "Fax")]
        public virtual string Fax { get; set; }

        [SitecoreField(FieldName = "Email")]
        public virtual string Email { get; set; }

        [SitecoreField(FieldName = "URL")]
        public virtual string Url { get; set; }

        [SitecoreField(FieldName = "Manager")]
        public virtual string Manager { get; set; }

        [SitecoreField(FieldName = "Latitude")]
        public virtual string Latitude { get; set; }

        [SitecoreField(FieldName = "Longitude")]
        public virtual string Longitude { get; set; }
    }
}
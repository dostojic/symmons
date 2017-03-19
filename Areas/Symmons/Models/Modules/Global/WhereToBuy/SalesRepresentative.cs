
using System;
using System.Linq;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy
{
    [SitecoreType(TemplateId = "{65B236E8-4771-4479-80A3-893ECC4300B4}", AutoMap = true)]
    public class SalesRepresentative
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Store Name")]
        public string Name { get; set; }

        [SitecoreField(FieldName = "Address 1")]
        public string Address1 { get; set; }

        [SitecoreField(FieldName = "Address 2")]
        public string Address2 { get; set; }

        [SitecoreField(FieldName = "Phone")]
        public string Phone { get; set; }

        [SitecoreField(FieldName = "Email")]
        public string Email { get; set; }

        [SitecoreField(FieldName = "Fax")]
        public string Fax { get; set; }

        [SitecoreField(FieldName = "Zip")]
        public string Zip { get; set; }

        [SitecoreField(FieldName = "City")]
        public string City { get; set; }

        [SitecoreField(FieldName = "State")]
        public string State { get; set; }

        [SitecoreField(FieldName = "Manager")]
        public string Manager { get; set; }

        [SitecoreField(FieldName = "Latitude")]
        public string Latitude { get; set; }

        [SitecoreField(FieldName = "Longitude")]
        public string Longitude { get; set; }

        [SitecoreField(FieldName = "Url")]
        public string Url { get; set; }

        public string FirstName
        {
            get
            {
                var names = Name.Split(' ');
                return names[0];
            }
        }

        public string LastName
        {
            get
            {
                var names = Name.Split(' ');
                if (names.Count() > 1)
                {
                    return Name.Substring(names[0].Length + 1, Name.Length - names[0].Length - 1);
                }
                return string.Empty;
            }
        }
    }
}
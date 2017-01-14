using System.Collections.Generic;
using System.Web.Mvc;

namespace symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy
{
    public class StoreTypeList
    {
        public IEnumerable<Store> Type { get; set; }
    }

    public class StoreType
    {
        public List<Store> WhereToBuys { get; set; }
        public string StoreTypeName { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
    }

    public class Store
    {
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StorePhoneNumber { get; set; }
        public string StoreType { get; set; }
    }
}
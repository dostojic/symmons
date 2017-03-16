using Glass.Mapper.Sc;
using symmons.com._Classes.Shared.Global;
using symmons.com.Areas.Symmons.Models.Modules.Global.WhereToBuy;
using symmons.com.Areas.Symmons.Models.Pages.Where_to_buy;
using System;
using System.Collections.Generic;
using System.Linq;
using Verndale.SharedSource.Helpers;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class ApiSuppliersHelper
    {
        public static IEnumerable<SupplierApiModel> ConvertAllAPiSupplierstoIList()
        {
            var retVal = new List<SupplierApiModel>();

            try
            {
                var sitecoreContext = new SitecoreContext();
                
                var salesRepItems = SearchHelper.GetItems(Constants.SearchIndex.Stores, Sitecore.Context.Language.ToString(),
                    Constants.TemplateIds.SalesRepresntative, string.Empty, string.Empty, null).ToList();
                var salesRepresentatives = salesRepItems.Select(item => sitecoreContext.GetItem<SalesRepresentative>(item.ID.ToString()));

                foreach (var item in salesRepresentatives)
                {
                    retVal.Add(new SupplierApiModel
                    {
                        Name = item.Name,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        City = item.City,
                        State = item.State,
                        Zip = item.Zip,
                        Phone = item.Phone,
                        Email = item.Email,
                        Fax = item.Fax,
                        Url = item.Url,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        Manager = item.Manager
                    });
                }
            }
            catch (Exception exception)
            {
                Sitecore.Diagnostics.Log.Error(
                  string.Format("Suppliers API: Error in ConvertAllAPiSupplierstoIList. Exception message:{0}",
                      exception.Message), new object());
            }

            return retVal;
        }
    }
}
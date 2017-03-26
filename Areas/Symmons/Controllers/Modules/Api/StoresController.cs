using Newtonsoft.Json;
using symmons.com._Classes.Custom.API;
using symmons.com._Classes.Symmons.Helpers;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Where_to_buy;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Store = symmons.com._Classes.Symmons.Global.Store;


namespace symmons.com.Areas.Symmons.Controllers.Modules.Api
{
    public class StoresController : SymmonsController
    {
        [System.Web.Mvc.HttpGet]
        public JsonResult GetAllStoresJson()
        {
            JsonResult returnJsonResult = null;

            var getStoresJson = StoresHelper.GetAllStores();
            var suppliersApiModels = getStoresJson as IList<Store> ?? getStoresJson.ToList();

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(suppliersApiModels, Formatting.Indented), Encoding.UTF8);
                response.StatusCode = HttpStatusCode.OK;

                if (suppliersApiModels.Any())
                {
                    response.StatusCode = HttpStatusCode.OK;
                    var data = new ApiResult
                    {
                        statusCode = HttpStatusCode.OK,
                        result = suppliersApiModels
                    };

                    returnJsonResult = Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.OK,
                        description = "No Stores",
                        result = new[] { new object() }
                    };

                    returnJsonResult = Json(error, JsonRequestBehavior.AllowGet);
                }

            }
            catch (WebException ex)
            {
                response.StatusCode = (HttpStatusCode)ex.Status;

                var error = new ApiResult
                {
                    statusCode = (HttpStatusCode)ex.Status,
                    description = ex.Message,
                    result = new[] { new object() }
                };

                returnJsonResult = Json(error, JsonRequestBehavior.AllowGet);
                Sitecore.Diagnostics.Log.Error("Error in GetAllSuppliers(). Response Detail", response);
                Sitecore.Diagnostics.Log.Error("Error in GetAllSuppliers()", ex.Message + ex.StackTrace);
            }

            returnJsonResult.MaxJsonLength = int.MaxValue;
            return returnJsonResult;
        }

        [System.Web.Mvc.HttpGet]
        public FileContentResult GetAllStoresCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name|Address1|Address2|City|State|Zip|Phone|Fax|Email|Url|Manager|Latitude|Longitude|IsSymmonsPreferred|Type");

            var stores = StoresHelper.GetAllStores();

            foreach (var store in stores)
            {
                sb.AppendLine(string.Format(
                    "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                    store.StoreName,
                    store.Address1,
                    store.Address2,
                    store.City,
                    store.State,
                    store.Zip,
                    store.PhoneNo,
                    store.Fax,
                    store.Email,
                    store.Url,
                    store.Manager,
                    store.Latitude,
                    store.Longitude,
                    store.IsSymmonsPreferred,
                    store.Type));
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "Stores.csv");
        }
    }
}
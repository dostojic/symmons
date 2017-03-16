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

namespace symmons.com.Areas.Symmons.Controllers.Modules.Api
{
    public class SuppliersController : SymmonsController
    {
        [System.Web.Mvc.HttpGet]
        public JsonResult GetAllSuppliersJson()
        {
            JsonResult returnJsonResult = null;

            var getSuppliersJson = ApiSuppliersHelper.ConvertAllAPiSupplierstoIList();
            var suppliersApiModels = getSuppliersJson as IList<SupplierApiModel> ?? getSuppliersJson.ToList();

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
                        description = "No Suppliers",
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
        public FileContentResult GetAllSuppliersCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name|Address1|Address2|City|State|Zip|Phone|Fax|Email|Manager|Url|Latitude|Longitude");

            var getSuppliersJson = ApiSuppliersHelper.ConvertAllAPiSupplierstoIList();
            var suppliersApiModels = getSuppliersJson as IList<SupplierApiModel> ?? getSuppliersJson.ToList();

            foreach (var supplier in suppliersApiModels)
            {
                sb.AppendLine(string.Format(
                    "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                    supplier.Name,
                    supplier.Address1,
                    supplier.Address2,
                    supplier.City,
                    supplier.State,
                    supplier.Zip,
                    supplier.Phone,
                    supplier.Fax,
                    supplier.Email,
                    supplier.Manager,
                    supplier.Url,
                    supplier.Latitude,
                    supplier.Longitude));
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "Suppliers.csv");
        }
    }
}
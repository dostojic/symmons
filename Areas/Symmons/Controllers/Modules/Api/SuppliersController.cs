using Newtonsoft.Json;
using symmons.com._Classes.Custom.API;
using symmons.com._Classes.Symmons.Helpers;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Where_to_buy;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace symmons.com.Areas.Symmons.Controllers.Modules.Api
{
    public class SuppliersController : SymmonsController
    {
        [HttpGet]
        public JsonResult GetAllSuppliers()
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
    }
}
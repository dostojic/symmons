using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Custom.API;
using symmons.com._Classes.Symmons.Helpers;

namespace symmons.com.Areas.Symmons.Controllers.Modules.Api
{
    public class ProductsController : SymmonsController
    {

        //public HttpResponse GetProducts()
        //{
        //    //HttpWebResponse wr = new HttpWebResponse();

        //    var getProductsJson = GetAllProducts();
        //    var response = System.Web.HttpContext.Current.Response;

        //    var productsApiModels = getProductsJson as IList<ProductsApiModel> ?? getProductsJson.ToList();
        //    try
        //    {
        //        JsonSerializer serializer = new JsonSerializer();

        //        if (productsApiModels.Any())
        //        {
        //            System.Diagnostics.Debugger.Break();
        //            response.StatusCode = (int)(HttpStatusCode.OK);
        //            response.ContentType = "application/json";
        //            response.ContentEncoding = Encoding.UTF8;

        //            using (StreamWriter sw = new StreamWriter(response.OutputStream, response.ContentEncoding))
        //            using (JsonTextWriter writer = new JsonTextWriter(sw))
        //            {
        //                writer.WriteStartArray();
        //                foreach (ProductsApiModel item in productsApiModels)
        //                {
        //                    JObject obj = JObject.FromObject(item, serializer);
        //                    obj.WriteTo(writer);
        //                    writer.Flush();
        //                }
        //                writer.WriteEndArray();

        //            }

        //        }
        //        else
        //        {
        //            response.StatusDescription = "No Products";
        //            response.Status = "200";
        //        }



        //    }
        //    catch (Exception ex)
        //    {

        //        response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        response.StatusDescription = HttpStatusCode.BadRequest.ToString();
        //        Sitecore.Diagnostics.Log.Error("Error in GetAllProducts(). Response Detail", response);
        //        Sitecore.Diagnostics.Log.Error("Error in GetAllProducts()", ex.Message + ex.StackTrace);
        //    }
        //    //string url = string.Format("{0}{1}", Request.Url.Host, "=;/Products/GetProducts");
        //    //var myRequest = WebRequest.CreateHttp(url);

        //    //using (var theResponse = myRequest.GetResponse())
        //    //{
        //    //    var dataStream = theResponse.GetResponseStream();
        //    //    StreamReader reader = new StreamReader(dataStream);
        //    //    object objResponse = reader.ReadToEnd();
        //    //    var myUser = JsonConvert.DeserializeObject<User>(objResponse.ToString());
        //    //    dataStream.Close();
        //    //    theResponse.Close();
        //    //}
        //    return response;
        //}


        // GET: /products/getproducts
        [HttpGet]
        public JsonResult GetAllProducts()
        {
               JsonResult returnJsonResult = null;
              var getProductsJson = ApiProductsHelper.ConvertAllAPiProductstoIList();
           var productsApiModels = getProductsJson as IList<ProductsApiModel> ?? getProductsJson.ToList();
            
            HttpResponseMessage response = new HttpResponseMessage();
           try
           {
               response.Content = new StringContent(
                   JsonConvert.SerializeObject(productsApiModels, Formatting.Indented), Encoding.UTF8);
               response.StatusCode = HttpStatusCode.OK;
               if (productsApiModels.Any())
               {
                    response.StatusCode = HttpStatusCode.OK;
                   var data = new ApiResult
                   {
                       statusCode = HttpStatusCode.OK,
                       result = productsApiModels
                   };
                    returnJsonResult = Json(data, JsonRequestBehavior.AllowGet);
                }
               else
               {
                    response.StatusCode = HttpStatusCode.OK;
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.OK,
                        description = "No Products",
                        result = new[] { new object() }
                    };
                    returnJsonResult = Json(error, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (WebException ex)

            {
                response.StatusCode = (HttpStatusCode) ex.Status;
                var error = new ApiResult
                {
                    statusCode = (HttpStatusCode) ex.Status,
                    description = ex.Message,
                    result = new[] {new object()}
                };  
                returnJsonResult = Json(error, JsonRequestBehavior.AllowGet);
                Sitecore.Diagnostics.Log.Error("Error in GetAllProducts(). Response Detail", response);
                Sitecore.Diagnostics.Log.Error("Error in GetAllProducts()", ex.Message + ex.StackTrace);
            }
            returnJsonResult.MaxJsonLength = int.MaxValue;
            return returnJsonResult;  
        }

        // GET: products/getproductbyid/'guid'
     [HttpGet]
        public JsonResult GetProductbyId(string id)
         { 
            JsonResult returnJsonResult = null;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                //check for the valid guid
                Guid guidOutput;
                bool isValid = Guid.TryParse(id, out guidOutput);
                if (!isValid)
                {
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        description = "Not valid ProductId",
                        result = new[] { new object() }
                    };
                    response.StatusCode = HttpStatusCode.BadRequest;
                     return Json(error, JsonRequestBehavior.AllowGet);
                }
                // Check if it is null than send 404 error code
                var prod = ApiProductsHelper.GetProductByProdId(id);
                if (prod == null)
                {
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.OK,
                        description = "No Product available",
                        result = new[] { new object() }
                    };
                    response.StatusCode = HttpStatusCode.NotFound;
                    return Json(error, JsonRequestBehavior.AllowGet);
                }

                var data = new ApiResult
                {
                    statusCode = HttpStatusCode.OK,
                    result = prod
                };
                response.StatusCode =HttpStatusCode.OK;
                returnJsonResult = Json(data, "application/json", JsonRequestBehavior.AllowGet);
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
                Sitecore.Diagnostics.Log.Error("Error in GetProductbyId(). Response Detail", response);
                Sitecore.Diagnostics.Log.Error("Error in GetProductbyId()", ex.Message + ex.StackTrace);
            }
             
            return returnJsonResult;
        }

        [HttpGet]
        public JsonResult GetNewProductsSince(string date)
        {
             
            JsonResult returnJsonResult = null;
            HttpResponseMessage response = new HttpResponseMessage();
            IList<ProductsApiModel> productsLst = new List<ProductsApiModel>();

            //Validate Date
            DateTime fromDateValue;
           var format = "yyyy-MM-dd";
            try
            {
                if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
               {
                    productsLst = ApiProductsHelper.GetProductsLstByUpdatedDate(date);

                   if (productsLst == null || productsLst.Count == 0)
                   {
                        var error = new ApiResult
                        {
                            statusCode = HttpStatusCode.OK,
                            description = "No Product available",
                            result = new[] { new object() }
                        };
                        response.StatusCode = HttpStatusCode.OK;
                        return Json(error, JsonRequestBehavior.AllowGet);
                    }

                    
                    var data = new ApiResult
                    {
                        statusCode = HttpStatusCode.OK,
                    result = productsLst
                    };
                    response.StatusCode = HttpStatusCode.OK;
                  
                    returnJsonResult = Json(data, JsonRequestBehavior.AllowGet);
                  
                }
            else
            {
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.BadRequest,
                    description = "Not Valid Date",
                        result = new[] { new object() }
                    };
                    returnJsonResult = Json(error, JsonRequestBehavior.AllowGet);
                }
                //if the date is blank than its a 404 error code 
                if (string.IsNullOrEmpty(date) || string.IsNullOrWhiteSpace(date))
                {
                    var error = new ApiResult
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        description = "Not Valid Date",
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
                returnJsonResult = Json(error, JsonRequestBehavior.AllowGet); Sitecore.Diagnostics.Log.Error("Error in GetNewProductsSince(). Response Detail", response);
                Sitecore.Diagnostics.Log.Error("Error in GetNewProductsSince()", ex.Message + ex.StackTrace);
            }
            returnJsonResult.MaxJsonLength = int.MaxValue;
            return  returnJsonResult ;
        }
    }
}

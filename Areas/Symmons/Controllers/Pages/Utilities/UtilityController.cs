using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Shared.Global;
using symmons.com._Classes.Symmons.Helpers;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;


namespace symmons.com.Areas.Symmons.Controllers.Pages.Utilities
{
    public class UtilityController : Controller
    {
        //
        // GET: /Symmons/Utility/
        public ActionResult Index()
        {
            return View("/Areas/Symmons/Views/Renderings/Pages/Utilities/BulkUpdate.cshtml");
        }

        public ActionResult Export()
        {
            
            var products = ApiProductsHelper.GetAllProductCollection();

            var finishProducts = ApiProductsHelper.GetAllFinishApiProductCollection();

            products.AddRange(finishProducts);

            if (products.Count > 0)
            {
                var xlsStr = BuildProductExcel(products);

                if (xlsStr != "")
                {
                    // Clear Response buffer
                    Response.Clear();

                    //Force Download
                    Response.AppendHeader("content-disposition", "attachment; filename=ProductPriceList.csv");

                    // Set ContentType to the ContentType of our file
                    Response.ContentType = "text/csv";

                    Response.Write(xlsStr);

                    // End the page
                    Response.End();
                }
            }
            return null;
        }

        [HttpPost]
        public void Upload(HttpPostedFileBase file)
        {
            
            System.Web.HttpContext.Current.Session["ErrorMessage"] = string.Empty;
            System.Web.HttpContext.Current.Session["SuccessMessage"] = string.Empty;
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("file", "Please Upload Your file");
                }
                else
                {
                    var allowedFileExtensions = new[] { ".csv" };

                    if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("file",
                            "Please upload file of type: " + string.Join(", ", allowedFileExtensions));
                    }
                    else
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                        file.SaveAs(path);
                        var rLines = ReadTextFile(file.InputStream);
                        var clearedLines = new ArrayList();

                        foreach (var rLine in rLines)
                        {
                            var myLine = deQoute(rLine.ToString(), 0);
                            clearedLines.Add(myLine);
                        }

                        UpdateProducts(clearedLines);
                        UpdateFinishProducts(clearedLines);

                        LoadTheLog(System.Web.HttpContext.Current);
                    }
                }

                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.Aggregate(string.Empty, (current1, modelState) => modelState.Errors.Aggregate(current1, (current, error) => current + error.ErrorMessage));
                    System.Web.HttpContext.Current.Session["ErrorMessage"] = errorMessage;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["SuccessMessage"] = "File uploaded!";
                }
            }
            Response.Redirect("/Utilities/BulkUpdate");
        }

        private void LoadTheLog(HttpContext context)
        {
            TextReader tr = null;
            string str;

            try
            {
                var logFileServerPath = context.Server.MapPath("\\_Documents\\temp\\UpdatePriceLog.txt");

                tr = new StreamReader(logFileServerPath);
                str = tr.ReadToEnd();

            }
            finally
            {
                if (tr != null)
                    tr.Close();
            }

            if (str != "")
            {
                // Clear Response buffer
                Response.Clear();

                //Force Download
                Response.AppendHeader("content-disposition", "attachment; filename=PriceUpdateResult.txt");

                // Set ContentType to the ContentType of our file
                Response.ContentType = "text/plain";

                Response.Write(str);

                // End the page
                Response.End();
            }
        }

        private bool UpdateFinishProducts(ArrayList sourceProducts)
        {
            var isUpdated = false;
            //    var master = Factory.GetDatabase("master");
            // var items = master.SelectItems("/sitecore/content/Global/Product Items//*[@@templatename = 'Product Finish']");

            var items = ApiProductsHelper.GetFinishProducts();
            var dicKeyword = "Finish SKU"; //Title in Finish Product is its SKU

            var itemDirectory = new Dictionary<string, Item>();
            foreach (var itm in items.Where(itm => !string.IsNullOrEmpty(itm.Fields[dicKeyword].Value.ToLower())).Where(itm => !itemDirectory.ContainsKey(itm.Fields[dicKeyword].Value.Replace(" ", "-").ToLower())))
            {
                itemDirectory.Add(itm.Fields[dicKeyword].Value.Replace(" ", "-").ToLower(), itm);
            }

            UpdatePriceLog(System.Web.HttpContext.Current, "Begin", 1);

            var searchKeyword = (int)PAttrs.Sku;
            foreach (var product in sourceProducts)
            {
                //Get product information from text file, line by line and split by comma (,)
                var prodProperties = product.ToString().Split(',');

                //Number of item properties must be 7
                if (prodProperties.Length == 7)
                {
                    //Search the dictionary of items if the code can find the item by its Title (Navigation Title)
                    //SKU must be the first element of the csv file
                    if (itemDirectory.ContainsKey(prodProperties[searchKeyword].ToLower()))
                    {
                        var itm = itemDirectory[prodProperties[searchKeyword].ToLower()];

                        //If the item in the excel import data is not a product finish then skip it, because this is only to update the finished product 
                        if (itm.TemplateName.ToLower() != "product finish" || !prodProperties[4].Contains("- Finish Product")) { continue; }

                        //Update the us price
                        if (!String.IsNullOrEmpty(prodProperties[(int)PAttrs.PriceUs]))
                        {
                            if (prodProperties[(int)PAttrs.PriceUs] != itm.Fields["Finish Price"].Value)
                            {
                                UpdateItem(itm, "Finish Price", prodProperties[(int)PAttrs.PriceUs]);
                                UpdatePriceLog(System.Web.HttpContext.Current, string.Format("UPDATING: {0} -- {1} = {2}",
                                                                        itm.Name, "Finish Price", prodProperties[(int)PAttrs.PriceUs]), 1);
                                isUpdated = true;
                            }
                        }

                        //Update the can price
                        if (!String.IsNullOrEmpty(prodProperties[(int)PAttrs.PriceCan]))
                        {
                            if (prodProperties[(int)PAttrs.PriceCan] != itm.Fields["Finish CAN Price"].Value)
                            {
                                UpdateItem(itm, "Finish CAN Price", prodProperties[(int)PAttrs.PriceCan]);
                                UpdatePriceLog(System.Web.HttpContext.Current, string.Format("UPDATING: {0} -- {1} = {2}",
                                                                       itm.Name, "Finish CAN Price", prodProperties[(int)PAttrs.PriceCan]), 1);
                                isUpdated = true;
                            }
                        }

                        //Update SKU in Sitecore
                        if (!string.Equals(prodProperties[(int)PAttrs.Sku], itm.Fields["Finish SKU"].Value, StringComparison.CurrentCultureIgnoreCase))
                        {
                            UpdateItem(itm, "Finish SKU", prodProperties[(int)PAttrs.Sku]);
                            UpdatePriceLog(System.Web.HttpContext.Current, string.Format("UPDATING: {0} -- {1} = {2}",
                                                                   itm.Name, "Finish SKU", prodProperties[(int)PAttrs.Sku]), 1);
                            isUpdated = true;
                        }

                        if (!isUpdated) UpdatePriceLog(System.Web.HttpContext.Current, string.Format("PASS: {0} ----- {1} -----",
                                                                       itm.Name, ""), 1);
                    }
                }
            }

            UpdatePriceLog(System.Web.HttpContext.Current, "End", 2);
            return isUpdated;
        }

        private string deQoute(string s, int index)
        {
            if (index >= s.Length) return s;
            int dequoteIndex;
            if ((dequoteIndex = s.IndexOf('"', index)) > 0)
            {
                var deqouteIndex2 = s.IndexOf('"', dequoteIndex + 1);
                if (deqouteIndex2 <= 0)
                {
                    s = s.Remove(dequoteIndex, 1);
                }
                else
                {
                    var commaIndex = s.IndexOf(',', dequoteIndex, deqouteIndex2 - dequoteIndex);
                    if (commaIndex > 0)
                    {
                        s = s.Remove(commaIndex, 1);
                        deQoute(s, index);
                    }
                    else
                    {
                        deQoute(s, deqouteIndex2 + 1);
                    }
                }
            }
            else
            {
                return s;
            }
            return s;
        }

        protected ArrayList ReadTextFile(Stream s)
        {
            var lines = new ArrayList();
            var sr = new StreamReader(s);
            string inputLine;
            while (((inputLine = sr.ReadLine()) != null) && inputLine.Length != 0)
            {
                lines.Add(inputLine);
            }
            sr.Close();
            return lines;
        }

        private static bool UpdateProducts(ArrayList sourceProducts)
        {
           var isUpdated = false;

            //    var master = Factory.GetDatabase("master");

            var items = ApiProductsHelper.GetProducts();

            var dicKeyword = "Product Model Number";

            var itemDirectory = new Dictionary<string, Item>();
            foreach (var itm in items.Where(itm => !string.IsNullOrEmpty(itm.Fields[dicKeyword].Value.ToLower())))
            {
                if (!itemDirectory.ContainsKey(itm.Fields[dicKeyword].Value.ToLower()))
                {
                    itemDirectory.Add(itm.Fields[dicKeyword].Value.ToLower(), itm);
                }
            }

            UpdatePriceLog(System.Web.HttpContext.Current, "Begin", 0);

            var searchKeyword = (int)PAttrs.Sku;
            foreach (var product in sourceProducts)
            {
                //Get product information from text file, line by line and split by comma (,)
                var prodProperties = product.ToString().Split(',');

                //Number of item properties must be 25
                if (prodProperties.Length == 7)
                {
                    //Search the dictionary of items if the code can find the item by its Title (Navigation Title)
                    //SKU must be the first element of the csv file
                    if (itemDirectory.ContainsKey(prodProperties[searchKeyword].ToLower()))
                    {
                        var itm = itemDirectory[prodProperties[searchKeyword].ToLower()];

                        //If the item in the excel import data is a product finish then skip it, because this is only to update the parent items
                        if (itm.TemplateName.ToLower() == "product finish" || prodProperties[4].Contains("- Finish Product")) { continue; }

                        //Update the us price
                        if (!string.IsNullOrEmpty(prodProperties[(int)PAttrs.PriceUs]))
                        {
                            if (prodProperties[(int)PAttrs.PriceUs] != itm.Fields["Price Min"].Value)
                            {
                                UpdateItem(itm, "Price Min", prodProperties[(int)PAttrs.PriceUs]);
                                UpdatePriceLog(System.Web.HttpContext.Current, string.Format("UPDATING: {0} -- {1} = {2}",
                                                                        itm.Name, "Price Min", prodProperties[(int)PAttrs.PriceUs]), 1);
                                isUpdated = true;
                            }
                        }

                        //Update the can price
                        if (!string.IsNullOrEmpty(prodProperties[(int)PAttrs.PriceCan]))
                        {
                            if (prodProperties[(int)PAttrs.PriceCan] != itm.Fields["CAN price"].Value)
                            {
                                UpdateItem(itm, "CAN Price", prodProperties[(int)PAttrs.PriceCan]);
                                UpdatePriceLog(System.Web.HttpContext.Current, string.Format("UPDATING: {0} -- {1} = {2}",
                                                                       itm.Name, "CAN Price", prodProperties[(int)PAttrs.PriceCan]), 1);
                                isUpdated = true;
                            }
                        }

                        if (!isUpdated) UpdatePriceLog(System.Web.HttpContext.Current, string.Format("PASS: {0} ----- {1} -----",
                                                                       itm.Name, ""), 1);
                    }
                }
            }

            UpdatePriceLog(System.Web.HttpContext.Current, "End", 2);
            return isUpdated;
        }

        public static void UpdateItem(Item itemToUpdate, string field, string value)
        {
          //  System.Diagnostics.Debugger.Break();
            using (new SecurityDisabler())
            {
                itemToUpdate.Editing.BeginEdit();

                var iField = itemToUpdate.Fields[field];
                if (iField != null)
                {
                    var val = System.Web.HttpContext.Current.Server.HtmlDecode(value);
                    itemToUpdate[field] = val;
                }
                itemToUpdate.Editing.EndEdit();
                PublishItem(itemToUpdate);
            }
        }

        private static void UpdatePriceLog(HttpContext context, string text, int mode)
        {
            TextWriter tw = null;

            try
            {
                string logFileServerPath = context.Server.MapPath("\\_Documents\\temp\\UpdatePriceLog.txt");

                switch (mode)
                {
                    case 0: //Open
                        tw = new StreamWriter(logFileServerPath, false);
                        tw.WriteLine(DateTime.Now + " " + "=============BEGIN============");
                        break;
                    case 1: //Append
                        tw = new StreamWriter(logFileServerPath, true);
                        tw.WriteLine(DateTime.Now + " " + text);
                        break;
                    case 2: //Close
                        tw = new StreamWriter(logFileServerPath, true);
                        tw.WriteLine(DateTime.Now + " " + "==============END============");
                        break;
                    default:
                        tw = new StreamWriter(logFileServerPath, false);
                        tw.WriteLine(DateTime.Now + " " + text);
                        break;
                }
            }
            finally
            {
                if (tw != null)
                    tw.Close();
            }

        }

        private static string BuildProductExcel(APIProductCollection products)
        {
            var outputStr = string.Empty;

            outputStr += "Model(SKU)," +
                         "Product Family/Collection," +
                         "Product Category," +
                         "Product Type," +
                         "Product Title," +
                         "Price US," +
                         "Price CAN," +
                         
                         Environment.NewLine;

            foreach (ProductsApiModel product in products)
            {
                var mystr = string.IsNullOrEmpty(product.ProductModelNumber) ? "" : product.ProductModelNumber.Replace(",", "");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.ProductFamily) ? "" : product.ProductFamily.Replace(",", "");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.ProductCategory) ? "" : product.ProductCategory.Replace(",", "|");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.ProductStyle) ? "" : product.ProductStyle.Replace(",", "");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.ProductName) ? "" : product.ProductName.Replace(",", "");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.PriceMin) ? "" : product.PriceMin.Replace(",", "");
                mystr += ",";
                mystr += string.IsNullOrEmpty(product.CanPrice) ? "" : product.CanPrice.Replace(",", "");
               
             mystr += Environment.NewLine;

                outputStr += mystr;
            }

            return outputStr;
        }


        public void Logout()
        {
            var fromurl = Request.QueryString["url"] ?? "/";

            Sitecore.Security.Authentication.AuthenticationManager.Logout();

            Response.Redirect(fromurl);
        }
        private enum PAttrs
        {
            Sku,
            family,
            category,
            type,
            title,
            PriceUs,
            PriceCan
        }

        /// <summary>
        /// Publish Insurance Agents Item to web db
        /// </summary>
        /// <param name="inItem">Sitecore insurance item / archive item</param>
        public static void PublishItem(Item inItem)
        {

            Sitecore.Diagnostics.Log.Info(String.Format("Product AutoPublish Begin {0} {1}", inItem.Name, DateTime.Now), new object());

            Item item = inItem;
            Sitecore.Diagnostics.Assert.IsNotNull(item, "item");
            PublishingTarget[] targets = PublishingTarget.GetPublishingTargets(item);
            Sitecore.Globalization.Language[] languages = Factory.GetDatabase(Constants.ConstantValues.DatabaseMaster).Languages;

            foreach (PublishingTarget target in targets)
            {

                Sitecore.Publishing.PublishManager.PublishItem(item, new[] { target.TargetDatabase }, languages,
                   true, true);

            }
            Sitecore.Diagnostics.Log.Info(String.Format("Product AutoPublish End {0} {1}", inItem.Name, DateTime.Now), new object());

        }

    }
}
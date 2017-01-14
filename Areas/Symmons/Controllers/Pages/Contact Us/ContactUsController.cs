using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;

namespace symmons.com.Areas.Symmons.Controllers.Pages
{
    public class ContactUsController : SymmonsController
    {
        // *******************************************************************************************************************

        // Render Contact Us Page...
        public ActionResult RenderContactUs()
        {
            return View(Constants.ViewPaths.ContactUsPage);
        }

        // *******************************************************************************************************************

        [HttpPost]
        public void SendContactUsMail(FormCollection collection, IEnumerable<HttpPostedFileBase> files)
        {
            var contactUsItem = SitecoreCurrentContext.GetItem<ContactUs>(Constants.PageIds.ContactUsPage);
            #region Bind Admin ContactUs Mail body

            var adminBodyString = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Symmons Mailer</title></head>" +
                                 "<body style='padding:0;margin: 0 auto;margin-bottom: 60px;'><table cellpadding='0' cellspacing='0' width='90%' border='0' style='font-family:\"Helvetica Neue\", Helvetica, Arial, sans-serif;  font-size:14px'>" +
                                  "<tr><td colspan='2' style='padding:10px;'><a href='http://www.symmons.com' target='_blank'><img src='http://www.symmons.com/~/media/Images/Symmons/Global/Settings/Logo/logo.gif' alt='Symmons Logo'> </a></td></tr>" +
                                  "<tr> <td colspan='2' bgcolor='#cf7b0f' style='font-size: 0; line-height: 0; height:2px'>&nbsp;</td></tr>" +
                                  "<tr><td colspan='2' style='padding:20px;'>&nbsp;</td></tr>" +
                                  "<tr> <td colspan='2' bgcolor='#57ae55' style='color: #FFFFFF;font-size: 16px;padding:10px;'>Contact Details from <strong>$$FirstName$$, $$LastName$$</strong></td></tr>" +
                                  "$$ProductSKU$$" +
                                  "<tr><td  width='20%' valign='top' bgcolor='#eeeeee' style='padding:10px;'>Email ID: </td><td style='padding:10px;' width='80%' bgcolor='#eeeeee'><a href='mailto:$$EmailId$$' target='_blank'>$$EmailId$$</a></td></tr>" +
                                 "<tr><td valign='top' bgcolor='#eeeeee' style='padding:10px;'>Phone #:</td><td bgcolor='#eeeeee' style='padding:10px;'>$$PhoneNo$$</td></tr>" +
                                  "<tr><td valign='top' bgcolor='#eeeeee' style='padding: 10px;'>Message:</td><td bgcolor='#eeeeee' style='padding:10px;'>$$MessageBody$$</td></tr>$$Attachments$$";

            const string productSkuvalue = "<tr> <td valign='top' bgcolor='#eeeeee' style='padding:10px;'>Product SKU: </td>  <td  bgcolor='#eeeeee' style='padding:10px;'>$$ProductSKUValue$$</td></tr>";
            if (!String.IsNullOrEmpty(collection["txtProductSKU"]))
            {
                adminBodyString = adminBodyString.Replace("$$ProductSKU$$", productSkuvalue);
                adminBodyString = adminBodyString.Replace("$$ProductSKUValue$$", collection["txtProductSKU"]);
            }
            const string txtDesignStudioValue = "<tr> <td valign='top' bgcolor='#eeeeee' style='padding:10px;'>Design Studio: </td>  <td  bgcolor='#eeeeee' style='padding:10px;'>$$DesignStudioValue$$</td></tr>";
            var hasDesignStudioVal = !String.IsNullOrEmpty(collection["txtDesignStudioVal"]);
            if (hasDesignStudioVal)
            {
                adminBodyString = adminBodyString.Replace("$$ProductSKU$$", txtDesignStudioValue);
                adminBodyString = adminBodyString.Replace("$$DesignStudioValue$$", collection["txtDesignStudioVal"]);
                contactUsItem.ContactUsEmailId = Constants.ConstantValues.DesignStudioContactId;
            }
            adminBodyString = adminBodyString.Replace("$$FirstName$$", collection["txtFirstName"]);
            adminBodyString = adminBodyString.Replace("$$LastName$$", collection["txtLastName"]);
            adminBodyString = adminBodyString.Replace("$$EmailId$$", collection["txtEmail"]);
            adminBodyString = adminBodyString.Replace("$$PhoneNo$$", collection["txtPhoneNumber"]);
            adminBodyString = adminBodyString.Replace("$$MessageBody$$", collection["txtMessage"]);

            var siteUrl = "http://" + Request.Url.Host.ToLower();
            var attachments = "<tr><td valign='top' bgcolor='#eeeeee' style='padding:10px;'>Files Attached:</td><td bgcolor='#eeeeee' style='padding:10px;'>";
            var isFilesAttached = false;
            foreach (var file in files)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName != null)
                        {
                            isFilesAttached = true;
                            var path = Path.Combine(Server.MapPath("~/Resources/ContactUs/Attachments"), fileName);
                            var fileUrl = siteUrl + "/Resources/ContactUs/Attachments/" + fileName;
                            attachments += "<a href='" + fileUrl + "' target='_blank'>" + fileName + "</a> <br><br>";
                            file.SaveAs(path);
                        }
                    }
                }
            }
            adminBodyString = isFilesAttached ? adminBodyString.Replace("$$Attachments$$", attachments + "</td></tr>") : adminBodyString.Replace("$$Attachments$$", "<tr><td valign='top' bgcolor='#eeeeee' style='padding:10px;'>Files Attached:</td><td bgcolor='#eeeeee' style='padding:10px;'>No files attached...</td></tr>");
            var emailAdminBodyText = adminBodyString + "</table></body></html>";

            #endregion


            #region Bind Admin ContactUs Mail body

            var userBodyString = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Symmons Mailer</title></head>" +
                                  "<body style='padding:0;margin: 0 auto;margin-bottom: 60px;'><table cellpadding='0' cellspacing='0' width='90%' border='0' style='font-family:\"Helvetica Neue\", Helvetica, Arial, sans-serif; font-size:14px'>" +
                                  "<tr><td colspan='2' style='padding:10px;'><a href='http://www.symmons.com' target='_blank'><img src='http://www.symmons.com/~/media/Images/Symmons/Global/Settings/Logo/logo.gif' alt='Symmons Logo'> </a></td></tr>" +
                                  "<tr> <td colspan='2' bgcolor='#cf7b0f' style='font-size: 0; height:2px; line-height: 0;'>&nbsp;</td></tr>" +
                                    "<tr><td colspan='2' style='padding:20px;'>&nbsp;</td></tr>" +
                                  "<tr> <td colspan='2' bgcolor='#57ae55' style='color: #FFFFFF;font-size: 16px; padding:10px'>Hello <strong>$$FirstName$$, $$LastName$$</strong></td></tr>" +
                                 "<tr><td bgcolor='#eeeeee' style='padding:10px;'> <p style='line-height: normal;margin: 8px 0;'>" + Translate.Text(Constants.Dictionary.SymmonsMailAddress) + "</p></td></tr></table></body></html>";

            userBodyString = userBodyString.Replace("$$FirstName$$", collection["txtFirstName"]);
            userBodyString = userBodyString.Replace("$$LastName$$", collection["txtLastName"]);
            var emailUserBodyText = userBodyString;

            #endregion

            // Send Email to admin witha attachment details
            if (_Classes.Shared.Utilities.EmailUtility.SendEmail(emailAdminBodyText, contactUsItem.ContactUsEmailId,
                contactUsItem.WebmasterEmailId, contactUsItem.ContactUsMailSubject, String.Empty, String.Empty, String.Empty))
            {
                // Send email to user
                if (_Classes.Shared.Utilities.EmailUtility.SendEmail(emailUserBodyText, collection["txtEmail"],
                    contactUsItem.WebmasterEmailId, contactUsItem.ContactUsMailSubject, String.Empty, String.Empty, String.Empty))
                {
                    // Redirect to success page...
                    var contactUsSuccessPageItem = hasDesignStudioVal
                        ? SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.DesignStudioContactUsSuccessPage)
                        : SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.ContactUsSuccessPage);
                    
                    Response.Redirect("http://" + Request.Url.Host.ToLower() + SitecoreHelper.ItemRenderMethods.GetItemUrl(contactUsSuccessPageItem));
                }
            }
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
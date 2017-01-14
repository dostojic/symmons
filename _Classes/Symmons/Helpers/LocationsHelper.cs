using System;
using System.Data;
using System.Net;
using System.Web;
using System.Xml;
using symmons.com._Classes.Shared.Global;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class LocationsHelper
    {
        // **************************************************************************************************************************
        // Get the country code from freegeoip.net based on ip address...
        public static DataTable GetGeoLocation(string ipaddress)
        {
            var request = WebRequest.Create("http://freegeoip.net/xml/" + ipaddress);
            var proxy = new WebProxy("http://freegeoip.net/xml/" + ipaddress, true);
            request.Proxy = proxy;
            request.Timeout = 0x7d0;
            try
            {
                var reader = new XmlTextReader(request.GetResponse().GetResponseStream());
                var set = new DataSet();
                set.ReadXml(reader);
                return set.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        // **************************************************************************************************************************

        // **************************************************************************************************************************
        // To check if user is accessing CA site...
        public static bool IsCaSite()
        {
            if (Sitecore.Context.Site.Name == Constants.ConstantValues.CaSiteKey)
            {
                return true;
            }
            return false;
        }

        // **************************************************************************************************************************

        // **************************************************************************************************************************
        // To Get the location os user (CA or US)...

        public static string GetLocation(HttpRequest req, string sitename)
        {
            var flag = false;
            var currentIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			
			if (
                        String.IsNullOrEmpty(currentIpAddress))
							{
							currentIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
							}
            // This is temporary...
            //if (String.IsNullOrEmpty(currentIpAddress))
            //{
            //currentIpAddress = Constants.ConstantValues.TestCanadaIpAddress;//(test canada ip)
            //}

            if (!String.IsNullOrEmpty(currentIpAddress))
            {
                var geoLocation = GetGeoLocation(currentIpAddress);
                if (geoLocation != null)
                {
                    if (geoLocation.Rows.Count > 0)
                    {
                        var countryName = geoLocation.Rows[0]["CountryName"].ToString();
                        var countryCode = geoLocation.Rows[0]["CountryCode"].ToString();
                        if ((countryName == "Canada") || (countryCode == "CA"))
                        {
                            flag = true;
                        }
                    }
                }
            }

            if (flag)
            {
                if (sitename == Constants.ConstantValues.CaSiteKey)
                {
                    return "CA";
                }
                return "REDIRECT";
            }
            return "US";
        }

        // **************************************************************************************************************************

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sitecore.Modules.SitemapXML
{
    public class SitemapHandler
    {
        public void RefreshSitemap(object sender, EventArgs args)
        {
            SitemapManager sitemapManager = new SitemapManager();
            sitemapManager.SubmitSitemapToSearchenginesByHttp();  
            if (!(ConfigurationManager.AppSettings["SITEMAP_OutPutRobots_txt"].ToString().Trim() == "1"))
                return;
            sitemapManager.RegisterSitemapToRobotsFile();
        }
    }
}
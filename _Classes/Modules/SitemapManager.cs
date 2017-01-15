// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.SitemapXML.SitemapManager
// Assembly: symmons.com, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F07DA85-A335-44EB-BBAB-D70698058439
// Assembly location: D:\Temp\symmons.com.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Security.Accounts;
using Sitecore.Sites;
using Sitecore.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Verndale.SharedSource.Helpers;

namespace Sitecore.Modules.SitemapXML
{
    public class SitemapManager
    {
        private int MAX_URLS = int.Parse(ConfigurationManager.AppSettings["SITEMAP_MAX_URLS"]);
        private const string SITEMAP_SUB_FOLDER = "sitemaps";
        private static StringDictionary m_Sites;

        public Database Db
        {
            get
            {
                return Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            }
        }

        public SitemapManager()
        {
            SitemapManager.m_Sites = SitemapManagerConfiguration.GetSites();
            foreach (DictionaryEntry site in SitemapManager.m_Sites)
                this.BuildSiteMap(site.Key.ToString(), site.Value.ToString());
        }

        private void BuildSiteMap(string sitename, string sitemapUrlNew)
        {
            Site site = SiteManager.GetSite(sitename);
            List<Item> sitemapItems = this.GetSitemapItems(Factory.GetSite(sitename).StartPath);
            string path1 = MainUtil.MapPath("/" + "sitemaps");
            if (Directory.Exists(path1))
            {
                foreach (string file in Directory.GetFiles(path1))
                    System.IO.File.Delete(file);
            }
            if (sitemapItems.Count > this.MAX_URLS)
            {
                List<string> filenames = new List<string>();
                int num = 0;
                while (num < sitemapItems.Count)
                {
                    filenames.Add("sitemaps\\" + (object)num + sitemapUrlNew);
                    num += this.MAX_URLS;
                }
                string path2 = MainUtil.MapPath("/" + sitemapUrlNew);
                string str1 = this.BuildSitemapIndex(filenames, site);
                StreamWriter streamWriter1 = new StreamWriter(path2, false);
                streamWriter1.Write(str1);
                streamWriter1.Close();
                while (sitemapItems.Count > 0)
                {
                    List<Item> list = sitemapItems.Take<Item>(this.MAX_URLS).ToList<Item>();
                    sitemapItems.RemoveRange(0, list.Count<Item>());
                    string path3 = MainUtil.MapPath("/" + filenames[0]);
                    filenames.RemoveAt(0);
                    string str2 = this.BuildSitemapXML(list, site);
                    StreamWriter streamWriter2 = new StreamWriter(path3, false);
                    streamWriter2.Write(str2);
                    streamWriter2.Close();
                }
            }
            else
            {
                string path2 = MainUtil.MapPath("/" + sitemapUrlNew);
                string str = this.BuildSitemapXML(sitemapItems, site);
                StreamWriter streamWriter = new StreamWriter(path2, false);
                streamWriter.Write(str);
                streamWriter.Close();
            }
        }

        public bool SubmitSitemapToSearchenginesByHttp()
        {
            if (!SitemapManagerConfiguration.IsProductionEnvironment)
                return false;
            bool flag = false;
            Item obj1 = this.Db.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
            if (obj1 != null)
            {
                string str = obj1.Fields["Search engines"].Value;
                char[] chArray = new char[1] { '|' };
                foreach (string index in str.Split(chArray))
                {
                    Item obj2 = this.Db.Items[index];
                    if (obj2 != null)
                    {
                        string engine = obj2.Fields["HttpRequestString"].Value;
                        foreach (string sitemapUrl in (IEnumerable)SitemapManager.m_Sites.Values)
                            this.SubmitEngine(engine, sitemapUrl);
                    }
                }
                flag = true;
            }
            return flag;
        }

        public void RegisterSitemapToRobotsFile()
        {
            string path = MainUtil.MapPath("/" + "robots.txt");
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            if (System.IO.File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                stringBuilder.Append(streamReader.ReadToEnd());
                streamReader.Close();
            }
            StreamWriter streamWriter = new StreamWriter(path, false);
            foreach (string str1 in (IEnumerable)SitemapManager.m_Sites.Values)
            {
                string str2 = "Sitemap: " + str1;
                if (!stringBuilder.ToString().Contains(str2))
                    stringBuilder.AppendLine(str2);
            }
            streamWriter.Write(stringBuilder.ToString());
            streamWriter.Close();
        }

        private string BuildSitemapIndex(List<string> filenames, Site site)
        {
            string serverUrlBySite = SitemapManagerConfiguration.GetServerUrlBySite(site.Name);
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlDeclaration = (XmlNode)xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", (string)null);
            xmlDocument.AppendChild(xmlDeclaration);
            XmlNode element1 = (XmlNode)xmlDocument.CreateElement("sitemapindex");
            XmlAttribute attribute = xmlDocument.CreateAttribute("xmlns");
            attribute.Value = SitemapManagerConfiguration.XmlnsTpl;
            element1.Attributes.Append(attribute);
            xmlDocument.AppendChild(element1);
            foreach (string filename in filenames)
            {
                string text1 = string.Format("http://{0}/{1}", (object)serverUrlBySite, (object)SitemapManager.HtmlEncode(filename.Replace("\\", "/")));
                string text2 = SitemapManager.HtmlEncode(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                XmlNode element2 = (XmlNode)xmlDocument.CreateElement("sitemap");
                element1.AppendChild(element2);
                XmlNode element3 = (XmlNode)xmlDocument.CreateElement("loc");
                element2.AppendChild(element3);
                element3.AppendChild((XmlNode)xmlDocument.CreateTextNode(text1));
                XmlNode element4 = (XmlNode)xmlDocument.CreateElement("lastmod");
                element2.AppendChild(element4);
                element4.AppendChild((XmlNode)xmlDocument.CreateTextNode(text2));
            }
            return xmlDocument.OuterXml;
        }

        private string BuildSitemapXML(List<Item> items, Site site)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode xmlDeclaration = (XmlNode)doc.CreateXmlDeclaration("1.0", "UTF-8", (string)null);
            doc.AppendChild(xmlDeclaration);
            XmlNode element = (XmlNode)doc.CreateElement("urlset");
            XmlAttribute attribute = doc.CreateAttribute("xmlns");
            attribute.Value = SitemapManagerConfiguration.XmlnsTpl;
            element.Attributes.Append(attribute);
            doc.AppendChild(element);
            items = SitecoreHelper.RemoveItemsWithoutLanguageVersion(items);
            foreach (Item obj in items)
            {
                if (obj != null && SitecoreHelper.ItemRenderMethods.GetCheckBoxValueByFieldName("Show On Xml SiteMap", obj))
                    doc = this.BuildSitemapItem(doc, obj, (Item)null, site, false);
            }
            return doc.OuterXml;
        }

        private XmlDocument BuildSitemapItem(XmlDocument doc, Item item, Item parentItem, Site site, bool isProduct)
        {
            string text1 = SitemapManager.HtmlEncode(this.GetItemUrl(item, false, site));
            string text2 = SitemapManager.HtmlEncode(item.Statistics.Updated.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            XmlNode lastChild = doc.LastChild;
            XmlNode element1 = (XmlNode)doc.CreateElement("url");
            lastChild.AppendChild(element1);
            XmlNode element2 = (XmlNode)doc.CreateElement("loc");
            element1.AppendChild(element2);
            if (item.TemplateID.ToString().Equals(symmons.com._Classes.Shared.Global.Constants.TemplateIds.ProductDetailsTemplateId))
                text1 = this.SetFullUrl(string.Format("/product/{0}", (object)item.Name.ToLower().Replace(' ', '-')), site);
            element2.AppendChild((XmlNode)doc.CreateTextNode(text1));
            if (item.Fields["Priority"] != null && !string.IsNullOrEmpty(item["Priority"]))
            {
                double result = 0.0;
                if (double.TryParse(item["Priority"], out result) && result >= 0.0 && result <= 1.0)
                {
                    XmlNode element3 = (XmlNode)doc.CreateElement("priority");
                    element1.AppendChild(element3);
                    element3.AppendChild((XmlNode)doc.CreateTextNode(item["Priority"]));
                }
            }
            if (item.Fields["Change Frequency"] != null && !string.IsNullOrEmpty(item["Change Frequency"]))
            {
                XmlNode element3 = (XmlNode)doc.CreateElement("changefreq");
                element1.AppendChild(element3);
                element3.AppendChild((XmlNode)doc.CreateTextNode(item["Change Frequency"]));
            }
            XmlNode element4 = (XmlNode)doc.CreateElement("lastmod");
            element1.AppendChild(element4);
            element4.AppendChild((XmlNode)doc.CreateTextNode(text2));
            return doc;
        }

        private string GetItemUrl(Item item, bool addAspxExtension, Site site)
        {
            UrlOptions defaultUrlOptions = LinkManager.GetDefaultUrlOptions();
            defaultUrlOptions.SiteResolving = Settings.Rendering.SiteResolving;
            defaultUrlOptions.Site = SiteContext.GetSite(site.Name);
            return this.SetFullUrl(LinkManager.GetItemUrl(item, defaultUrlOptions), site);
        }

        private string SetFullUrl(string url, Site site)
        {
            string serverUrlBySite = SitemapManagerConfiguration.GetServerUrlBySite(site.Name);
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(serverUrlBySite))
            {
                if (url.Contains("://") && !url.Contains("http"))
                {
                    stringBuilder.Append("http://");
                    stringBuilder.Append(serverUrlBySite);
                    if (url.IndexOf("/", 3) > 0)
                        stringBuilder.Append(url.Substring(url.IndexOf("/", 3)));
                }
                else
                {
                    stringBuilder.Append("http://");
                    stringBuilder.Append(serverUrlBySite);
                    stringBuilder.Append(url);
                }
            }
            else if (!string.IsNullOrEmpty(site.Properties["hostname"]))
            {
                stringBuilder.Append("http://");
                stringBuilder.Append(site.Properties["hostname"]);
                stringBuilder.Append(url);
            }
            else if (url.Contains("://") && !url.Contains("http"))
            {
                stringBuilder.Append("http://");
                stringBuilder.Append(url);
            }
            else
                stringBuilder.Append(WebUtil.GetFullUrl(url));
            return stringBuilder.ToString();
        }

        private static string HtmlEncode(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        private void SubmitEngine(string engine, string sitemapUrl)
        {
            if (sitemapUrl.Contains("http://localhost"))
                return;
            string requestUriString = engine + SitemapManager.HtmlEncode(sitemapUrl);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            try
            {
                if (((HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == HttpStatusCode.OK)
                    return;
                Log.Error(string.Format("Cannot submit sitemap to \"{0}\"", (object)engine), (object)this);
            }
            catch
            {
                Log.Warn(string.Format("The searchengine \"{0}\" returns an 404 error", (object)requestUriString), (object)this);
            }
        }

        private List<Item> GetSitemapItems(string rootPath)
        {
            string disabledTemplates1 = SitemapManagerConfiguration.DisabledTemplates;
            Item obj = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase).Items[rootPath];
            Item[] descendants;
            using (new UserSwitcher(User.FromName("extranet\\Anonymous", true)))
                descendants = obj.Axes.GetDescendants();
            List<Item> list1 = ((IEnumerable<Item>)descendants).ToList<Item>();
            list1.Insert(0, obj);
            List<string> disabledTemplates = this.BuildListFromString(disabledTemplates1, '|');
            List<Item> list2 = list1.Where<Item>((Func<Item, bool>)(itm =>
            {
                if (itm.Template != null)
                    return !disabledTemplates.Contains(itm.Template.ID.ToString());
                return false;
            })).ToList<Item>();
            Context.SetActiveSite("website");
            List<Item> list3 = SearchHelper.GetItems(symmons.com._Classes.Shared.Global.Constants.SearchIndex.Products, Context.Language.ToString(), symmons.com._Classes.Shared.Global.Constants.TemplateIds.ProductDetailsTemplateId, symmons.com._Classes.Shared.Global.Constants.FolderIds.ProductsRepository, (string)null, (List<Refinement>)null).Where<Item>((Func<Item, bool>)(x => x != null)).ToList<Item>();
            if (list3.Any<Item>())
                list2.AddRange((IEnumerable<Item>)list3);
            return list2;
        }

        private List<string> BuildListFromString(string str, char separator)
        {
            return ((IEnumerable<string>)str.Split(separator)).Where<string>((Func<string, bool>)(dtp => !string.IsNullOrEmpty(dtp))).ToList<string>();
        }
    }
}

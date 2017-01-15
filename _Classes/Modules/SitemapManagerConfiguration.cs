// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.SitemapXML.SitemapManagerConfiguration
// Assembly: symmons.com, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F07DA85-A335-44EB-BBAB-D70698058439
// Assembly location: D:\Temp\symmons.com.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Xml;
using System.Collections.Specialized;
using System.Xml;

namespace Sitecore.Modules.SitemapXML
{
    public class SitemapManagerConfiguration
    {
        public static string XmlnsTpl
        {
            get
            {
                return SitemapManagerConfiguration.GetValueByName("xmlnsTpl");
            }
        }

        public static string WorkingDatabase
        {
            get
            {
                return SitemapManagerConfiguration.GetValueByName("database");
            }
        }

        public static string SitemapConfigurationItemPath
        {
            get
            {
                return SitemapManagerConfiguration.GetValueByName("sitemapConfigurationItemPath");
            }
        }

        public static string DisabledTemplates
        {
            get
            {
                return SitemapManagerConfiguration.GetValueByNameFromDatabase("Disabled templates");
            }
        }

        public static string ExcludeItems
        {
            get
            {
                return SitemapManagerConfiguration.GetValueByNameFromDatabase("Exclude items");
            }
        }

        public static bool IsProductionEnvironment
        {
            get
            {
                string valueByName = SitemapManagerConfiguration.GetValueByName("productionEnvironment");
                if (string.IsNullOrEmpty(valueByName))
                    return false;
                if (!(valueByName.ToLower() == "true"))
                    return valueByName == "1";
                return true;
            }
        }

        private static string GetValueByName(string name)
        {
            string str = string.Empty;
            foreach (XmlNode configNode in Factory.GetConfigNodes("sitemapVariables/sitemapVariable"))
            {
                if (XmlUtil.GetAttribute("name", configNode) == name)
                {
                    str = XmlUtil.GetAttribute("value", configNode);
                    break;
                }
            }
            return str;
        }

        private static string GetValueByNameFromDatabase(string name)
        {
            string empty = string.Empty;
            Database database = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            if (database != null)
            {
                Item obj = database.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
                if (obj != null)
                    empty = obj[name];
            }
            return empty;
        }

        public static StringDictionary GetSites()
        {
            StringDictionary stringDictionary = new StringDictionary();
            foreach (XmlNode configNode in Factory.GetConfigNodes("sitemapVariables/sites/site"))
            {
                if (!string.IsNullOrEmpty(XmlUtil.GetAttribute("name", configNode)) && !string.IsNullOrEmpty(XmlUtil.GetAttribute("filename", configNode)))
                    stringDictionary.Add(XmlUtil.GetAttribute("name", configNode), XmlUtil.GetAttribute("filename", configNode));
            }
            return stringDictionary;
        }

        public static string GetServerUrlBySite(string name)
        {
            string str = string.Empty;
            foreach (XmlNode configNode in Factory.GetConfigNodes("sitemapVariables/sites/site"))
            {
                if (XmlUtil.GetAttribute("name", configNode) == name)
                {
                    str = XmlUtil.GetAttribute("serverUrl", configNode);
                    break;
                }
            }
            return str;
        }
    }
}

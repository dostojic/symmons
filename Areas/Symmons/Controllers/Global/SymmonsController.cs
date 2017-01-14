using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using symmons.com.Areas.Symmons.Models.Global;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;

namespace symmons.com.Areas.Symmons.Controllers.Global
{
    public class SymmonsController : GlassController
    {
        // *******************************************************************************************************************
        public static SitecoreContext SitecoreCurrentContext
        {
            get
            {
                return new SitecoreContext();
            }
        }

        // *******************************************************************************************************************

        // Return the Datasource Item configured via presentation...
        public static Item DatasourceItem
        {
            get
            {
                return string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource) ? null :
                    SitecoreHelper.ItemMethods.GetItemFromGUID(RenderingContext.Current.Rendering.DataSource);
            }
        }

        // *******************************************************************************************************************

        // Returns site settings Item...
        public static SymmonsSiteSettings SiteSettingsItem
        {
            get { return SitecoreCurrentContext.GetItem<SymmonsSiteSettings>(Constants.PageIds.SiteSettings); }
        }

        // *******************************************************************************************************************

        // Returns context Item...
        public static Item ContextItem
        {
            get
            {
                return Sitecore.Context.Item;
            }
        }

        // *******************************************************************************************************************
        // *******************************************************************************************************************
    }
}
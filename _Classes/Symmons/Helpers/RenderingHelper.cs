using Sitecore.Data.Items;
using Sitecore.Layouts;
using System.Collections.Generic;
using System.Linq;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class RenderingHelper
    {
        public static List<string> GetDataSources(string itemId, string placeHolder)
        {
            RenderingReference[] renderings = GetListOfSublayouts(itemId);

            var placeHolderRenderings = renderings.Where(x => x.Placeholder == placeHolder).ToArray();

            return GetListOfDataSource(placeHolderRenderings);
        }

        public static RenderingReference[] GetListOfSublayouts(string itemId)
        {
            RenderingReference[] renderings = null;

            if (Sitecore.Data.ID.IsID(itemId))
            {
                Item item = Sitecore.Context.Database.GetItem(Sitecore.Data.ID.Parse(itemId));
                if (item != null)
                {
                    renderings = item.Visualization.GetRenderings(Sitecore.Context.Device, true);
                }
            }

            return renderings;
        }

        public static List<string> GetListOfDataSource(RenderingReference[] renderings)
        {
            List<string> ListOfDataSource = new List<string>();

            foreach (RenderingReference rendering in renderings)
            {
                ListOfDataSource.Add(rendering.Settings.DataSource);
            }

            return ListOfDataSource;
        }
    }
}
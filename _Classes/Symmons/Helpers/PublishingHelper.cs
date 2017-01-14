using System.Collections.Generic;
using SC = Sitecore;

namespace symmons.com._Classes.Symmons.Helpers
    {


        public class PublishingTarget : SC.Data.Items.CustomItem
        {
            private SC.Data.Database _targetDatabase;

            public PublishingTarget(SC.Data.Items.Item innerItem)
                : base(innerItem)
            {
            }

            public SC.Data.Database TargetDatabase
            {
                get
                {
                    if (_targetDatabase == null)
                    {
                        string name = InnerItem["Target database"];
                        SC.Diagnostics.Assert.IsNotNullOrEmpty(
                          name,
                        InnerItem.Paths.FullPath);
                        _targetDatabase = SC.Configuration.Factory.GetDatabase(
                          name);
                        SC.Diagnostics.Assert.IsNotNull(
                          _targetDatabase, name);
                    }

                    return _targetDatabase;
                }
            }

            public static PublishingTarget[] GetPublishingTargets(
              SC.Data.Items.Item item)
            {
                SC.Diagnostics.Assert.ArgumentNotNull(item, "item");
                List<PublishingTarget> targets = new List<PublishingTarget>();

                foreach (PublishingTarget target in GetPublishingTargets(item.Database))
                {
                    if (target.Applies(item))
                    {
                        targets.Add(target);
                    }
                }

                return targets.ToArray();
            }

            public static PublishingTarget[] GetPublishingTargets(
              SC.Data.Database database)
            {
                SC.Diagnostics.Assert.ArgumentNotNull(database, "database");
                SC.Data.Items.Item pubTargets = database.GetItem(
                  "/sitecore/system/publishing targets");
                SC.Diagnostics.Assert.IsNotNull(pubTargets, "publishing targets");
                List<PublishingTarget> targets = new List<PublishingTarget>();

                foreach (SC.Data.Items.Item target in pubTargets.Children)
                {
                    targets.Add(new PublishingTarget(target));
                }

                return targets.ToArray();
            }

            public bool Applies(SC.Data.Items.Item item)
            {
                while (item != null)
                {
                    string restricted = item["Publishing targets"];

                    if (!(string.IsNullOrEmpty(restricted)
                      || restricted.Contains(item.ID.ToString())))
                    {
                        return false;
                    }

                    item = item.Parent;
                }

                return true;
            }

            public bool Contains(
              SC.Data.Items.Item item)
            {
                return Contains(
                  item,
                  false, /*checkVersion*/
                  false, /*checkRevision*/
                  false /*checkPath */);
            }

            public bool Contains(
              SC.Data.Items.Item item,
              bool checkVersion)
            {
                return Contains(
                  item,
                  checkVersion,
                  false, /*checkRevision*/
                  false /*checkPath */);
            }

            public bool Contains(
              SC.Data.Items.Item item,
              bool checkVersion,
              bool checkRevision)
            {
                return Contains(
                  item,
                  checkVersion,
                  checkRevision,
                  true /*checkPath */);
            }

            public bool Contains(
              SC.Data.Items.Item item,
              bool checkVersion,
              bool checkRevision,
              bool checkPath)
            {
                SC.Diagnostics.Assert.ArgumentNotNull(item, "item");
                SC.Data.Items.Item targetItem = TargetDatabase.GetItem(
                  item.ID,
                  item.Language,
                  checkVersion ? item.Version : SC.Data.Version.Latest);

                if (targetItem == null
                  || (checkVersion && item.Version.Number != targetItem.Version.Number)
                  || (checkRevision && item.Statistics.Revision != targetItem.Statistics.Revision)
                  || (checkPath && item.Paths.FullPath != targetItem.Paths.FullPath))
                {
                    return false;
                }

                return true;
            }
        }
}
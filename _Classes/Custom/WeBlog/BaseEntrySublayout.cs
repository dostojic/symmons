// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.WeBlog.Layouts.BaseEntrySublayout
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using Sitecore.Modules.WeBlog.Items.WeBlog;

namespace symmons.com._Classes.Symmons.Custom
{
  public class BaseEntrySublayout : BaseSublayout
  {
    public EntryItem CurrentEntry { get; set; }

    public BaseEntrySublayout()
    {
      this.CurrentEntry = new EntryItem(this.DataSourceItem);
    }
  }
}

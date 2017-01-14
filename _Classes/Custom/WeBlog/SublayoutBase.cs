// Decompiled with JetBrains decompiler
// Type: Sitecore.Sharedsource.Web.UI.Sublayouts.SublayoutBase
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using Sitecore;
using Sitecore.Data.Items;
using System;
using System.Web.UI;

namespace symmons.com._Classes.Symmons.Custom
{
  public class SublayoutBase : UserControl
  {
    private Item dataSourceItem;

    public Item DataSourceItem
    {
      get
      {
        if (this.dataSourceItem == null)
          this.dataSourceItem = Sitecore.Context.Item;
        return this.dataSourceItem;
      }
      set
      {
        this.dataSourceItem = value;
      }
    }

    public string DataSource
    {
      get
      {
        if (this.DataSourceItem == null)
          return string.Empty;
        else
          return this.DataSourceItem.Paths.FullPath;
      }
      set
      {
          this.dataSourceItem = Sitecore.Context.Database.GetItem(value);
      }
    }

    protected SublayoutParameterHelper Helper { get; set; }

    public string GetParameter(string name)
    {
      return this.Helper.GetParameter(name);
    }

    protected override void OnInit(EventArgs e)
    {
      this.Helper = new SublayoutParameterHelper((UserControl) this, true);
    }
  }
}

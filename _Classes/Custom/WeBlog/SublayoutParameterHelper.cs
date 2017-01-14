// Decompiled with JetBrains decompiler
// Type: Sitecore.Sharedsource.Web.UI.Sublayouts.SublayoutParameterHelper
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Reflection;
using Sitecore.Web;
using Sitecore.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Web.UI;

namespace symmons.com._Classes.Symmons.Custom
{
  public class SublayoutParameterHelper
  {
    private Item dataSourceItem;

    public string DataSource
    {
      set
      {
        this.DataSourceItem = Context.Database.GetItem(value);
      }
    }

    public Item DataSourceItem
    {
      get
      {
        if (this.dataSourceItem == null)
        {
          if (this.BindingControl == null || string.IsNullOrEmpty(this.BindingControl.DataSource))
            this.dataSourceItem = Context.Item;
          else if (Context.Database != null)
            this.dataSourceItem = Context.Database.GetItem(this.BindingControl.DataSource);
        }
        return this.dataSourceItem;
      }
      set
      {
        this.dataSourceItem = value;
      }
    }

    protected NameValueCollection Parameters { get; set; }

    protected Sublayout BindingControl { get; set; }

    public SublayoutParameterHelper(UserControl control, bool applyProperties)
    {
      this.BindingControl = control.Parent as Sublayout;
      if (this.BindingControl == null)
        return;
      this.Parameters = WebUtil.ParseUrlParameters(this.BindingControl.Parameters);
      if (!applyProperties)
        return;
      this.ApplyProperties(control);
    }

    public string GetParameter(string key)
    {
      if (this.Parameters == null)
        return string.Empty;
      string str = this.Parameters[key];
      if (string.IsNullOrEmpty(str))
        return string.Empty;
      else
        return str;
    }

    protected void ApplyProperties(UserControl control)
    {
      foreach (string name in this.Parameters.Keys)
        ReflectionUtil.SetProperty((object) control, name, (object) this.Parameters[name]);
      if (string.IsNullOrEmpty(this.BindingControl.DataSource))
        return;
      ReflectionUtil.SetProperty((object) control, "datasource", (object) this.DataSourceItem.Paths.FullPath);
      ReflectionUtil.SetProperty((object) control, "datasourceitem", (object) this.DataSourceItem);
    }
  }
}

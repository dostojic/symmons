// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.WeBlog.Layouts.BaseSublayout
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using Sitecore;
using Sitecore.Data;
using Sitecore.Modules.WeBlog.Converters;
using Sitecore.Modules.WeBlog.Extensions;
using Sitecore.Modules.WeBlog.Items.WeBlog;
using Sitecore.Modules.WeBlog.Managers;
using Sitecore.Sharedsource.Web.UI.Sublayouts;
using Sitecore.Sites;
using Sitecore.Web.UI.WebControls;
using System;
using System.ComponentModel;

namespace symmons.com._Classes.Symmons.Custom
{
  public class BaseSublayout : SublayoutBase
  {
    public BlogHomeItem CurrentBlog { get; set; }

    [TypeConverter(typeof (ExtendedBooleanConverter))]
    public bool VaryByBlog { get; set; }

    public BaseSublayout()
    {
      this.CurrentBlog = ManagerFactory.BlogManagerInstance.GetCurrentBlog();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.UpdateParametersForCaching();
    }

    public void UpdateParametersForCaching()
    {
      if (!this.VaryByBlog)
        return;
      Sublayout sublayout1 = this.Parent as Sublayout;
      if (sublayout1 == null)
        return;
      SiteContext site = Sitecore.Context.Site;
      if (!sublayout1.Cacheable || site == null || (!site.CacheHtml || this.CurrentBlog == null))
        return;
      string str1 = "CacheVaryByBlogKey=" + GenericExtensions.SafeGet<string, ShortID>(GenericExtensions.SafeGet<ShortID, ID>(GenericExtensions.SafeGet<ID, BlogHomeItem>(this.CurrentBlog, (Func<BlogHomeItem, ID>) (x => x.ID)), (Func<ID, ShortID>) (x => x.ToShortID())), (Func<ShortID, string>) (x => x.ToString()));
      Sublayout sublayout2 = sublayout1;
      string str2 = sublayout2.Parameters + (string.IsNullOrEmpty(sublayout1.Parameters) ? string.Empty : "&" + str1);
      sublayout2.Parameters = str2;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.WeBlog.Layouts.BlogEntry
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using Sitecore.Modules.WeBlog.Converters;
using Sitecore.Web.UI.WebControls;
using System;
using System.ComponentModel;
using System.Drawing;
using Sitecore;
using Sitecore.Data.Items;

namespace symmons.com._Classes.Symmons.Custom
{
  public class BlogEntry : BaseEntrySublayout
  {
    protected Sitecore.Web.UI.WebControls.Image EntryImage;
    protected Sitecore.Web.UI.WebControls.Text txtTitle;
    protected Sitecore.Web.UI.WebControls.Text txtIntroduction;
    protected Sitecore.Web.UI.WebControls.Text txtContent;

    [TypeConverter(typeof (ExtendedBooleanConverter))]
    public bool ShowEntryTitle { get; set; }

    [TypeConverter(typeof (ExtendedBooleanConverter))]
    public bool ShowEntryImage
    {
      get
      {
        return this.EntryImage.Visible;
      }
      set
      {
        this.EntryImage.Visible = value;
      }
    }

    [TypeConverter(typeof (ExtendedBooleanConverter))]
    public bool ShowEntryMetadata { get; set; }

    [TypeConverter(typeof (ExtendedBooleanConverter))]
    public bool ShowEntryIntroduction { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      //this.Page.Title = this.CurrentEntry.Title.Text + " | " + this.CurrentBlog.Title.Text;
      Size imageSizeDimension = this.CurrentBlog.MaximumEntryImageSizeDimension;
      if (!(imageSizeDimension != Size.Empty))
        return;
      this.EntryImage.MaxWidth = imageSizeDimension.Width;
      this.EntryImage.MaxHeight = imageSizeDimension.Height;
    }

    protected bool DoesFieldRequireWrapping(string fieldName)
    {
      return !this.CurrentEntry[fieldName].StartsWith("<p>");
    }
  }
}

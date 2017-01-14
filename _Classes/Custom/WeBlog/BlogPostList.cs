// Decompiled with JetBrains decompiler
// Type: Sitecore.Modules.WeBlog.Layouts.BlogPostList
// Assembly: Sitecore.Modules.WeBlog, Version=2.3.0.42519, Culture=neutral, PublicKeyToken=null
// MVID: 3BCDA881-5047-4AC9-B82E-9FE3855B0746
// Assembly location: C:\SVN\C01844-CitiPerformingArtsCenter\citicenter.org\trunk\Assemblies\Sitecore.Modules.WeBlog.dll

using MongoDB.Driver.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Modules.WeBlog;
using Sitecore.Modules.WeBlog.Extensions;
using Sitecore.Modules.WeBlog.Items.WeBlog;
using Sitecore.Modules.WeBlog.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace symmons.com._Classes.Symmons.Custom
{
  public class BlogPostList : BaseSublayout
  {
    protected Size m_imageMaxSize = Size.Empty;
    protected const string DEFAULT_POST_TEMPLATE = "/layouts/WeBlog/PostListEntry.ascx";
    protected ListView EntryList;
    protected HtmlAnchor ancViewMore;

    public int TotalToShow { get; set; }

    public int StartIndex { get; set; }

    public string PostTemplate { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.PostTemplate))
        this.PostTemplate = "/layouts/WeBlog/PostListEntry.ascx";
      this.EntryList.ItemTemplate = this.Page.LoadTemplate(this.PostTemplate);
      this.m_imageMaxSize = this.CurrentBlog.MaximumThumbnailImageSizeDimension;
      string s1 = this.Request.QueryString["count"] ?? "0";
      int result1 = 0;
      int.TryParse(s1, out result1);
      this.TotalToShow = result1 > 0 ? result1 : this.CurrentBlog.DisplayItemCountNumeric;
      string s2 = this.Request.QueryString["startIndex"] ?? "0";
      int result2 = 0;
      int.TryParse(s2, out result2);
      this.StartIndex = result2;
      string str1 = this.Request.QueryString["tag"];
      this.BindEntries(str1);
      string itemUrl = LinkManager.GetItemUrl(Sitecore.Context.Item);
      if (this.ancViewMore == null)
        return;

	  if (Request != null && Request["archive"] != null && !string.IsNullOrEmpty(Request["archive"]))
	  {
		  this.ancViewMore.HRef = itemUrl + (object)"?archive=" + Request["archive"].ToString() + (object)"&count=" + (this.TotalToShow + this.CurrentBlog.DisplayCommentSidebarCountNumeric).ToString();
	  }
		else if (Request != null && Request["search"] != null && !string.IsNullOrEmpty(Request["search"]))
		{
			this.ancViewMore.HRef = itemUrl + (object)"?search=" + Request["search"].ToString() + (object)"&count=" + (this.TotalToShow + this.CurrentBlog.DisplayCommentSidebarCountNumeric).ToString();

		}
		else
			this.ancViewMore.HRef = itemUrl + (object) "?count=" + (this.TotalToShow + this.CurrentBlog.DisplayCommentSidebarCountNumeric).ToString();
	    if (str1 == null)
        return;
      HtmlAnchor htmlAnchor = this.ancViewMore;
      string str2 = htmlAnchor.HRef + "&tag=" + this.Server.UrlEncode(str1);
      htmlAnchor.HRef = str2;
    }

    protected virtual void BindEntries(string tag)
    {
	    int yearandmonth;
        TemplateItem template = new TemplateItem(Sitecore.Context.Database.GetItem(Settings.CategoryTemplateID));
      IEnumerable<EntryItem> source;
      if (!string.IsNullOrEmpty(tag))
      {
        source = (IEnumerable<EntryItem>) ManagerFactory.EntryManagerInstance.GetBlogEntries(tag);
        //this.Page.Title = "Posts tagged \"" + tag + "\" | " + this.CurrentBlog.Title.Text;
      }
      else if (ItemExtensions.TemplateIsOrBasedOn(Sitecore.Context.Item, template))
      {
          CategoryItem categoryItem = (CategoryItem)Sitecore.Context.Item;
        source = (IEnumerable<EntryItem>) ManagerFactory.EntryManagerInstance.GetBlogEntryByCategorie(this.CurrentBlog.ID, Sitecore.Context.Item.Name);
        //this.Page.Title = categoryItem.Title.Text + " | " + this.CurrentBlog.Title.Text;
      }
	  else if (Request != null && Request["archive"] != null && int.TryParse(Request["archive"], out yearandmonth) && yearandmonth > 99999)
	  {
		  string year = yearandmonth.ToString().Substring(0, 4);
		  string month = yearandmonth.ToString().Substring(4);

		  int yearint = 0;
		  int monthint = 0;

		  int.TryParse(year, out yearint);
		  int.TryParse(month, out monthint);

		  source = (IEnumerable<EntryItem>)ManagerFactory.EntryManagerInstance.GetBlogEntriesByMonthAndYear(this.CurrentBlog, monthint, yearint);
	  }
	  else if (Request != null && Request["search"] != null && !string.IsNullOrEmpty(Request["search"]))
	  {
		  string searchterms = Request["search"].ToString();
 
		  source = (IEnumerable<EntryItem>)ManagerFactory.EntryManagerInstance.GetBlogEntries(tag);
		  source = source.Where(x => (x.InnerItem.Fields["Title"] != null && !string.IsNullOrEmpty(x.InnerItem.Fields["Title"].ToString()) && x.InnerItem.Fields["Title"].ToString().ToLower().Contains(searchterms))
			  || (x.InnerItem.Fields["Introduction"] != null && !string.IsNullOrEmpty(x.InnerItem.Fields["Introduction"].ToString()) && x.InnerItem.Fields["Introduction"].ToString().ToLower().Contains(searchterms))
			  || (x.InnerItem.Fields["Content"] != null && !string.IsNullOrEmpty(x.InnerItem.Fields["Content"].ToString()) && x.InnerItem.Fields["Content"].ToString().ToLower().Contains(searchterms)));
	  }
      else
        source = (IEnumerable<EntryItem>) ManagerFactory.EntryManagerInstance.GetBlogEntries();
      if (this.TotalToShow == 0)
        this.TotalToShow = Enumerable.Count<EntryItem>(source);
      if (this.StartIndex + this.TotalToShow >= Enumerable.Count<EntryItem>(source) && this.ancViewMore != null)
        this.ancViewMore.Visible = false;
      IEnumerable<EntryItem> enumerable = Enumerable.Take<EntryItem>(Enumerable.Skip<EntryItem>(source, this.StartIndex), this.TotalToShow);
      if (this.EntryList == null)
        return;
      this.EntryList.DataSource = (object) enumerable;
      this.EntryList.DataBind();
    }

    protected void EntryDataBound(object sender, ListViewItemEventArgs args)
    {
      if (args.Item.ItemType != ListViewItemType.DataItem)
        return;
      ListViewDataItem listViewDataItem = args.Item as ListViewDataItem;
      Control control = listViewDataItem.FindControl("EntryImage");
      if (control == null)
        return;
      Sitecore.Web.UI.WebControls.Image image = control as Sitecore.Web.UI.WebControls.Image;
      image.MaxWidth = this.m_imageMaxSize.Width;
      image.MaxHeight = this.m_imageMaxSize.Height;
      //if ((listViewDataItem.DataItem as EntryItem).ThumbnailImage.MediaItem != null)
        return;
      image.Field = "Image";
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Links;
using Sitecore.Modules.WeBlog.Converters;
using Sitecore.Modules.WeBlog.Items.WeBlog;
using Sitecore.Modules.WeBlog.Managers;
using Sitecore.Web;

namespace symmons.com._Classes.Symmons.Custom
{
	public class BlogTagCloud : BaseSublayout
	{
		private int m_min = int.MaxValue;
		private int m_max = int.MinValue;
		protected Panel PanelTagCloud;
		protected Repeater TagList;

		protected DateTime m_startedDate = DateTime.Now;
		public Dictionary<int, List<EntryItem>> m_entriesByMonthAndYear;
		protected Repeater Years;

		[TypeConverter(typeof(ExtendedBooleanConverter))]
		public bool ExpandMonthsOnLoad { get; set; }

		[TypeConverter(typeof(ExtendedBooleanConverter))]
		public bool ExpandPostsOnLoad { get; set; }

		public int[] BlogEntry_Years { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			this.LoadTags();

			this.ExpandMonthsOnLoad = true;
			this.m_entriesByMonthAndYear = new Dictionary<int, List<EntryItem>>();
			if (this.CurrentBlog != null)
				this.m_startedDate = this.CurrentBlog.InnerItem.Statistics.Created;
			this.LoadEntries();
			if (this.Years != null && this.BlogEntry_Years != null && Enumerable.Any<int>((IEnumerable<int>)this.BlogEntry_Years))
			{
				this.Years.DataSource = (object)this.BlogEntry_Years;
				this.Years.DataBind();
			}
			//else
			//	this.Visible = false;
		}

		protected virtual void LoadTags()
		{
			if (ManagerFactory.TagManagerInstance.GetAllTags().Count == 0)
			{
				if (this.PanelTagCloud == null)
					return;
				this.PanelTagCloud.Visible = false;
			}
			else
			{
				Dictionary<string, int> allTags = ManagerFactory.TagManagerInstance.GetAllTags();
				this.m_min = Enumerable.Min(Enumerable.Select<KeyValuePair<string, int>, int>((IEnumerable<KeyValuePair<string, int>>)allTags, (Func<KeyValuePair<string, int>, int>)(tag => tag.Value)));
				this.m_max = Enumerable.Max(Enumerable.Select<KeyValuePair<string, int>, int>((IEnumerable<KeyValuePair<string, int>>)allTags, (Func<KeyValuePair<string, int>, int>)(tag => tag.Value)));
				if (this.TagList == null)
					return;
				this.TagList.DataSource = (object)allTags;
				this.TagList.DataBind();
			}
		}

		protected virtual string GetTagWeightClass(int tagWeight)
		{
			return this.GetWeight((double)tagWeight / (double)this.m_max * 100.0).ToString();
		}

		protected virtual string GetTagUrl(string tag)
		{
			return WebUtil.RemoveQueryString(LinkManager.GetItemUrl(this.CurrentBlog.InnerItem)) + "?tag=" + HttpUtility.UrlEncode(tag);
		}

		protected int GetWeight(double weightPercent)
		{
			return weightPercent >= 99.0 ? 1 : (weightPercent >= 70.0 ? 2 : (weightPercent >= 40.0 ? 3 : (weightPercent >= 20.0 ? 4 : (weightPercent >= 3.0 ? 5 : 0))));
		}

		protected void LoadEntries()
		{
			this.m_entriesByMonthAndYear = new Dictionary<int, List<EntryItem>>();
			EntryItem[] blogEntries = ManagerFactory.EntryManagerInstance.GetBlogEntries();
			foreach (EntryItem entryItem in blogEntries)
			{
				DateTime created = entryItem.Created;
				int key = created.Year * 100 + created.Month;
				if (this.m_entriesByMonthAndYear.ContainsKey(key))
					this.m_entriesByMonthAndYear[key].Add(entryItem);
				else
					this.m_entriesByMonthAndYear.Add(key, new List<EntryItem>()
          {
            entryItem
          });
			}
			this.BlogEntry_Years = this.GetYears(blogEntries);
		}

		protected virtual int[] GetYears(EntryItem[] entries)
		{
			List<int> list = new List<int>();
			if (Enumerable.Any<EntryItem>((IEnumerable<EntryItem>)entries))
			{
				int year1 = Enumerable.First<EntryItem>((IEnumerable<EntryItem>)entries).Created.Year;
				int year2 = Enumerable.Last<EntryItem>((IEnumerable<EntryItem>)entries).Created.Year;
				if (year1 != 0 && year2 != 0)
				{
					for (int year3 = year1; year3 >= year2; --year3)
					{
						if (this.YearHasBlogEntries(year3))
							list.Add(year3);
					}
				}
			}
			return list.ToArray();
		}

		protected virtual int[] GetMonths(int year)
		{
			int num = year * 100;
			List<int> list = new List<int>();
			for (int index = 1; index <= 12; ++index)
			{
				if (this.m_entriesByMonthAndYear.ContainsKey(num + index))
					list.Add(num + index);
			}
			return list.ToArray();
		}

		protected virtual string GetFriendlyMonthName(int yearAndMonth)
		{
			if (yearAndMonth > 99999)
				return new DateTimeFormatInfo().GetMonthName(int.Parse(yearAndMonth.ToString().Substring(4, 2)));
			else
				return "[unknown]";
		}

		protected virtual string IsCurrentSelection(int yearAndMonth)
		{
			int yearandmonthString;

			if (Request != null && Request["archive"] != null && int.TryParse(Request["archive"], out yearandmonthString) && yearandmonthString > 99999 && yearandmonthString == yearAndMonth)
			{
				return "selected=\"selected\"";
			}
			else
			{
				
			}

			return "";
		}

		protected virtual int GetEntryCountForYearAndMonth(int yearAndMonth)
		{
			return this.GetEntriesForYearAndMonth(yearAndMonth).Count;
		}

		protected virtual List<EntryItem> GetEntriesForYearAndMonth(int yearAndMonth)
		{
			if (this.m_entriesByMonthAndYear.ContainsKey(yearAndMonth))
				return this.m_entriesByMonthAndYear[yearAndMonth];
			else
				return new List<EntryItem>();
		}

		protected virtual bool YearHasBlogEntries(int year)
		{
			foreach (int num in this.m_entriesByMonthAndYear.Keys)
			{
				if (num.ToString().Contains(year.ToString()))
					return true;
			}
			return false;
		}

		protected virtual void MonthDataBound(object sender, RepeaterItemEventArgs args)
		{
			if (args.Item.ItemType != ListItemType.AlternatingItem && args.Item.ItemType != ListItemType.Item || this.GetEntryCountForYearAndMonth((int)args.Item.DataItem) > 0)
				return;
			args.Item.Controls.Clear();
		}
	}
}
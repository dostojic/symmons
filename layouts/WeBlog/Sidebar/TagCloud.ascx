<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogTagCloud.ascx.cs" Inherits="symmons.com._Classes.Symmons.Custom.BlogTagCloud" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Verndale.SharedSource.Helpers" %>


<div class="page-header">
                <div class="container">
                    <h1 class="page-header__title blog-title"><sc:FieldRenderer runat="server" FieldName="Title"/></h1>

                        </div>
            </div>

<asp:Panel ID="PanelTagCloud" runat="server" CssClass="wb-tagCloud wb-panel">
    <h3><%= Sitecore.Modules.WeBlog.Globalization.Translator.Render("TAGCLOUD") %></h3>
    <select class="wb-entries">
		<option data-href="<%= SitecoreHelper.ItemRenderMethods.GetItemUrl(Sitecore.Context.Item) %>" value="<%= SitecoreHelper.ItemRenderMethods.GetItemUrl(Sitecore.Context.Item) %>">View All</option>
        <asp:Repeater runat="server" ID="TagList">
            <ItemTemplate>
				<option data-href="<%# GetTagUrl(((KeyValuePair<string, int>)Container.DataItem).Key) %>" value="<%# ((KeyValuePair<string, int>)Container.DataItem).Key %>" <%# (Request["tag"] != null && !string.IsNullOrEmpty(Request["tag"].ToString()) && Request["tag"].ToString() == ((KeyValuePair<string, int>)Container.DataItem).Key ? "selected=\"selected\"" : "") %>><%# ((KeyValuePair<string, int>)Container.DataItem).Key %></option>
            </ItemTemplate>
        </asp:Repeater>
    </select>
</asp:Panel>


<div class="wb-archive wb-panel" id="wb-archive">
    <h3><%=Sitecore.Modules.WeBlog.Globalization.Translator.Render("ARCHIVE")%></h3>
    <asp:Repeater runat="server" ID="Years">
        <HeaderTemplate>
            <select class="wb-entries blog-drop-down">
				<option data-href="<%= SitecoreHelper.ItemRenderMethods.GetItemUrl(Sitecore.Context.Item) %>" value="">View All</option>
        </HeaderTemplate>
        <ItemTemplate>
			<optgroup label="<%# Container.DataItem %>">
                <asp:Repeater runat="server" ID="Months" DataSource="<%# GetMonths((int)Container.DataItem) %>" OnItemDataBound="MonthDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <option data-href="<%= SitecoreHelper.ItemRenderMethods.GetItemUrl(Sitecore.Context.Item) %>?archive=<%# Container.DataItem %>" value="<%# Container.DataItem %>" <%# (Request["archive"] != null && !string.IsNullOrEmpty(Request["archive"].ToString()) && Request["archive"].ToString() == Container.DataItem.ToString() ? "selected=\"selected\"" : "") %> ><%# GetFriendlyMonthName((int)Container.DataItem) %></option>
					   
                            
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
        </ItemTemplate>
        <FooterTemplate>
           </select>
        </FooterTemplate>
    </asp:Repeater>
</div>

<div class="blog-search">
        <h3>Search Blog Posts</h3>

        <div class="blog-search__input">
            <input value="" type="submit">
            <input placeholder="Search" autocomplete="off" type="search" value="<%= (Request["search"] != null && !string.IsNullOrEmpty(Request["search"].ToString()) ? Request["search"].ToString() : "") %>">
        </div>
    </div>
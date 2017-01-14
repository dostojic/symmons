<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogArchive.ascx.cs" Inherits="Sitecore.Modules.WeBlog.Layouts.BlogArchive" %>
<%@ Import Namespace="Sitecore.Modules.WeBlog.Items.WeBlog" %>
<%@ Import Namespace="Verndale.SharedSource.Helpers" %>


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
                        <option data-href="<%= SitecoreHelper.ItemRenderMethods.GetItemUrl(Sitecore.Context.Item) %>?archive=<%# Container.DataItem %>" value="<%# Container.DataItem %>"><%# GetFriendlyMonthName((int)Container.DataItem) %></option>
					   
                            
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

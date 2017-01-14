<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogEntry.ascx.cs" Inherits="symmons.com._Classes.Symmons.Custom.BlogEntry" %>
<%@ Register TagPrefix="gl" Namespace="Sitecore.Modules.WeBlog.Globalization" Assembly="Sitecore.Modules.WeBlog" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>

<div class="wb-entry">
    
    <% if (ShowEntryTitle) { %>
        <h2>
            <% if (!string.IsNullOrEmpty(CurrentEntry["title"]) || Sitecore.Context.PageMode.IsPageEditor) { %>
            <sc:Text ID="txtTitle" Field="Title" runat="server" />
            <% } else { %>
            <%= CurrentEntry.Name %>
            <% } %>
        </h2>
    <% } %>
    <% if (ShowEntryMetadata) { %>
        <div class="wb-details">
            <% if (string.IsNullOrEmpty(CurrentEntry.InnerItem.Fields["Author"].ToString()))
			   { %>
					<%=Sitecore.Modules.WeBlog.Globalization.Translator.Format("ENTRY_DETAILS_NONAME", CurrentEntry.Created.ToString(Sitecore.Modules.WeBlog.Settings.DateFormat)) %>
            <% } else { %>
					<%=Sitecore.Modules.WeBlog.Globalization.Translator.Format("ENTRY_DETAILS", CurrentEntry.Created.ToString(Sitecore.Modules.WeBlog.Settings.DateFormat), CurrentEntry.AuthorName) %>
            <%} %>
        </div>
    <% } %>
    <sc:Image runat="server" ID="EntryImage" Field="Image" CssClass="wb-image" /><% if (ShowEntryIntroduction) { %>
    <sc:Placeholder runat="server" key="weblog-below-entry-title" />
    <% if(DoesFieldRequireWrapping("Introduction")) { %>
    <p>
    <% } %>
        <sc:Text ID="txtIntroduction" Field="Introduction" runat="server" />
    <% if(DoesFieldRequireWrapping("Introduction")) { %>
    </p>
    <% } %>
    <% } %>
    <% if(DoesFieldRequireWrapping("Content")) { %>
    <p>
    <% } %>
    <sc:Text ID="txtContent" Field="Content" runat="server" />
    <% if(DoesFieldRequireWrapping("Content")) { %>
    </p>
    <% } %>
    <sc:Placeholder runat="server" key="weblog-below-entry" /> 
</div>
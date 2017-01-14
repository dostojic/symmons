﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blog.ascx.cs" Inherits="Sitecore.Modules.WeBlog.Layouts.Blog" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>

<div class="wb">
    <div class="wb-header">
        <asp:HyperLink ID="HyperlinkBlog" runat="server"><sc:Text ID="FieldTextItem" Field="Title" runat="server" /></asp:HyperLink>
    </div>

    <div class="wb-wrapper">

        <div class="wb-leftcolumn">
            <sc:placeholder ID="WeBlogMain" key="weblog-main" runat="server" />
        </div>

        <div class="wb-rightcolumn">
            <sc:placeholder ID="WeBlogSidebar" key="weblog-sidebar" runat="server" />
        </div>
        <%-- Feel free to remove the following line from your implementation --%>
        <div class="wb-footer">Powered by <a href="http://github.com/WeTeam/WeBlog">WeBlog</a></div>
    </div>
</div>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogLayout.aspx.cs" Inherits="Sitecore.Modules.WeBlog.Layouts.BlogLayout" %>
<%@ Register TagPrefix="wb" Namespace="Sitecore.Modules.WeBlog.WebControls" Assembly="Sitecore.Modules.WeBlog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en" xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title><%= GetItemTitle() %></title>
    <wb:Syndication runat="server" Cacheable="true" VaryByData="true" />
    <wb:RsdIncludes runat="server" Cacheable="true" VaryByData="true" />
    <link href="/sitecore modules/web/WeBlog/Includes/Common.css" rel="stylesheet" />
    <wb:ThemeIncludes runat="server" Cacheable="true" VaryByData="true" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        if (typeof jQuery == 'undefined') {
            document.write(unescape("%3Cscript src='/sitecore modules/web/WeBlog/Includes/jquery-1.10.1.min.js' type='text/javascript'%3E%3C/script%3E"));
        }
    </script>
    <script src="/sitecore modules/web/WeBlog/Includes/jquery.url.js" type="text/javascript"></script>
    <script src="/sitecore modules/web/WeBlog/Includes/functions.js" type="text/javascript"></script>
  </head>
  <body>
  <form method="post" runat="server" id="mainform">
    <sc:placeholder key="weblog-content" runat="server" />
  
  </form>
  </body>
</html>

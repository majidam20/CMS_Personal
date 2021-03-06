﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/lightBox/js/prototype.js"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/scriptaculous.js?load=effects,builder"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/lightbox.js"></script>
    <link href="Scripts/lightBox/css/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='float: <%# CultureResource.ResourceM.GetString("Align")%>;
        text-align: <%# CultureResource.ResourceM.GetString("Align")%>' dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" valign="top">
                <div>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style='height: 600px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>'
                                dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                                <div style='width: 50%; float: <%# CultureResource.ResourceM.GetString("Align")%>'>
                                    <asp:Literal ID="ltrText" runat="server"></asp:Literal></div>
                                <div style='width: 50%; float: <%# CultureResource.ResourceM.GetString("AlignN")%>'>
                                    <asp:HyperLink ID="hypProdImage" Target="_blank" runat="server">
                                        <asp:Image ID="imgProd" runat="server" /></asp:HyperLink>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td  style='float: <%# CultureResource.ResourceM.GetString("Align")%>;'>
                                <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                    text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>' dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

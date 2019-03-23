<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Free.aspx.cs" Inherits="Free" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
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
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <div>
                    <div class="LargFrame">
                        <div class="LargFrameHeader">
                            <div class="Title" style="text-align: right;">
                                صفحه اصلی</div>
                        </div>
                        asdasdsdf<br />
                        s<br />
                        df<br />
                        sd<br />
                        f<br />
                        sdf<br />
                        s<br />
                        df<br />
                        sd<br />
                        f<br />
                        s<br />
                        fs<br />
                        df<br />
                        sd<br />
                        f<br />
                        sf
                        <div class="LargFrameFooter">
                            <div class="Text" style="text-align: left;">
                                صفحه اصلی</div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
